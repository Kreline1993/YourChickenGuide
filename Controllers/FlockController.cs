using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourChickenGuide.Data;
using YourChickenGuide.Models;
using YourChickenGuide.Models.ViewModels;

namespace YourChickenGuide.Controllers
{
    public class FlockController : Controller
    {
        private readonly ApplicationDBContexts _context;

        public FlockController(ApplicationDBContexts context)
        {
            _context = context;
        }

        // Shared: populate dropdowns (breeds, statuses, parents)
        private async Task PopulateEditListsAsync(Chicken? model = null)
        {
            ViewBag.Breeds = BreedList.Breeds ?? new List<string>();
            ViewBag.Statuses = StatusList.Statuses ?? new List<string>();

            var mothers = await _context.Chickens.AsNoTracking()
                .Where(c =>
                    (c.Status == null || c.Status != "Inactive") &&
                    (c.Sex == "Female" || c.Sex == "Hen"))
                .OrderBy(c => c.Legband_Id)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Legband_Id })
                .ToListAsync();
            mothers.Insert(0, new SelectListItem { Value = "", Text = "Unknown" });

            var fathers = await _context.Chickens.AsNoTracking()
                .Where(c =>
                    (c.Status == null || c.Status != "Inactive") &&
                    (c.Sex == "Male" || c.Sex == "Rooster"))
                .OrderBy(c => c.Legband_Id)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Legband_Id })
                .ToListAsync();
            fathers.Insert(0, new SelectListItem { Value = "", Text = "Unknown" });

            ViewBag.Mothers = mothers;
            ViewBag.Fathers = fathers;
        }

        public IActionResult ViewChicken(int id)
        {
            var chicken = _context.Chickens
                .Include(c => c.Mother)
                .Include(c => c.Father)
                .Include(c => c.ChildrenAsMother)
                .Include(c => c.ChildrenAsFather)
                .FirstOrDefault(c => c.Id == id);

            if (chicken == null) return NotFound();
            return View(chicken);
        }

        public async Task<IActionResult> Overview(
            string? breed, string? sex, string? status, int? motherId, int? fatherId,
            string? search, string? sortBy, string? sortDir)
        {
            var q = _context.Chickens.AsNoTracking().AsQueryable();

            // Status rule: exclude Inactive by default, include only chosen status if provided
            if (string.IsNullOrWhiteSpace(status))
                q = q.Where(c => c.Status == null || c.Status != "Inactive");
            else
                q = q.Where(c => c.Status == status);

            if (!string.IsNullOrWhiteSpace(breed)) q = q.Where(c => c.Breed == breed);
            if (!string.IsNullOrWhiteSpace(sex)) q = q.Where(c => c.Sex == sex);
            if (motherId.HasValue) q = q.Where(c => c.mother_Id == motherId.Value);
            if (fatherId.HasValue) q = q.Where(c => c.father_Id == fatherId.Value);
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(c =>
                    (c.Legband_Id != null && c.Legband_Id.Contains(search)) ||
                    (c.Notes != null && c.Notes.Contains(search)) ||
                    (c.Color != null && c.Color.Contains(search)));

            var dir = (sortDir ?? "asc").ToLowerInvariant();
            switch ((sortBy ?? "legband").ToLowerInvariant())
            {
                case "hatch": q = dir == "desc" ? q.OrderByDescending(c => c.HatchDate) : q.OrderBy(c => c.HatchDate); break;
                case "breed": q = dir == "desc" ? q.OrderByDescending(c => c.Breed) : q.OrderBy(c => c.Breed); break;
                case "sex": q = dir == "desc" ? q.OrderByDescending(c => c.Sex) : q.OrderBy(c => c.Sex); break;
                case "status": q = dir == "desc" ? q.OrderByDescending(c => c.Status) : q.OrderBy(c => c.Status); break;
                default: q = dir == "desc" ? q.OrderByDescending(c => c.Legband_Id) : q.OrderBy(c => c.Legband_Id); break;
            }

            var chickens = await q.ToListAsync();

            var breeds = (BreedList.Breeds ?? new List<string>())
                .Select(b => new SelectListItem { Value = b, Text = b, Selected = b == breed })
                .Prepend(new SelectListItem { Value = "", Text = "All breeds", Selected = string.IsNullOrEmpty(breed) })
                .ToList();

            var statuses = (StatusList.Statuses ?? new List<string>())
                .Select(s => new SelectListItem { Value = s, Text = s, Selected = s == status })
                .Prepend(new SelectListItem { Value = "", Text = "Any status", Selected = string.IsNullOrEmpty(status) })
                .ToList();

            // Parent filters in the filter bar — also exclude Inactive
            var mothers = await _context.Chickens.AsNoTracking()
                .Where(c => (c.Status == null || c.Status != "Inactive") && (c.Sex == "Female" || c.Sex == "Hen"))
                .OrderBy(c => c.Legband_Id)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Legband_Id, Selected = motherId == c.Id })
                .ToListAsync();
            mothers.Insert(0, new SelectListItem { Value = "", Text = "Any mother", Selected = !motherId.HasValue });

            var fathers = await _context.Chickens.AsNoTracking()
                .Where(c => (c.Status == null || c.Status != "Inactive") && (c.Sex == "Male" || c.Sex == "Rooster"))
                .OrderBy(c => c.Legband_Id)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Legband_Id, Selected = fatherId == c.Id })
                .ToListAsync();
            fathers.Insert(0, new SelectListItem { Value = "", Text = "Any father", Selected = !fatherId.HasValue });

            var vm = new ChickenOverviewVm
            {
                Breed = breed,
                Sex = sex,
                Status = status,
                MotherId = motherId,
                FatherId = fatherId,
                Search = search,
                SortBy = sortBy,
                SortDir = dir,
                Chickens = chickens,
                Breeds = breeds,
                Statuses = statuses,
                Mothers = mothers,
                Fathers = fathers
            };

            return View(vm);
        }

        public async Task<IActionResult> AddChicken()
        {
            await PopulateEditListsAsync();
            return View();
        }

        public async Task<IActionResult> EditChicken(int id)
        {
            var chicken = await _context.Chickens.FindAsync(id);
            if (chicken == null) return NotFound();

            // ✅ This was missing — populate ViewBags for the edit view
            await PopulateEditListsAsync(chicken);
            return View(chicken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewChicken(Chicken chicken)
        {
            if (!ModelState.IsValid)
            {
                await PopulateEditListsAsync(chicken);
                return View("AddChicken", chicken);
            }

            _context.Add(chicken);
            await _context.SaveChangesAsync();
            return RedirectToAction("Overview");
        }

        [HttpGet]
        public async Task<IActionResult> EditExistingChicken(int id)
        {
            var chicken = await _context.Chickens.FindAsync(id);
            if (chicken == null) return NotFound();

            await PopulateEditListsAsync(chicken);
            return View("EditChicken", chicken);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExistingChicken(int id, Chicken chicken)
        {
            if (id != chicken.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopulateEditListsAsync(chicken);
                return View("EditChicken", chicken);
            }

            var existingChicken = await _context.Chickens.FindAsync(id);
            if (existingChicken == null) return NotFound();

            // Update allowed fields
            existingChicken.Legband_Id = chicken.Legband_Id;
            existingChicken.HatchDate = chicken.HatchDate;
            existingChicken.Breed = chicken.Breed;
            existingChicken.Color = chicken.Color;
            existingChicken.Notes = chicken.Notes;
            existingChicken.Sex = chicken.Sex;
            existingChicken.Status = chicken.Status;
            existingChicken.mother_Id = chicken.mother_Id;
            existingChicken.father_Id = chicken.father_Id;

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewChicken", new { id = existingChicken.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetParentsByBreed(string breed, string sex)
        {
            var chickens = await _context.Chickens.AsNoTracking()
                .Where(c =>
                    c.Breed == breed &&
                    c.Sex == sex &&
                    (c.Status == null || c.Status != "Inactive"))
                .OrderBy(c => c.Legband_Id)
                .Select(c => new { id = c.Id, legband_Id = c.Legband_Id }) // keys match your JS
                .ToListAsync();

            return Json(chickens);
        }
    }
}
