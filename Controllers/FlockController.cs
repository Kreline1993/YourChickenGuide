using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YourChickenGuide.Data;
using YourChickenGuide.Models;
using Microsoft.EntityFrameworkCore;          



namespace YourChickenGuide.Controllers
{
    public class FlockController : Controller
    {
        private readonly ApplicationDBContexts _context;

        public FlockController(ApplicationDBContexts context)
        {
            _context = context;
        }

        // Retrieve a chicken by id and show details in a view
        public IActionResult ViewChicken(int id)
        {
            var chicken = _context.Chickens.FirstOrDefault(c => c.Id == id);
            if (chicken == null)
            {
                return NotFound();
            }
            return View(chicken);
        }
        public async Task<IActionResult> Overview()
        {
            var allChickens = await _context.Chickens
                .ToListAsync();
            return View(allChickens);
        }
        public async Task<IActionResult> AddChicken()
        {
            ViewBag.Breeds = YourChickenGuide.Data.BreedList.Breeds ?? new List<string>();
            ViewBag.Statuses = YourChickenGuide.Data.StatusList.Statuses ?? new List<string>();

            // Mothers: active females
            var mothers = await _context.Chickens
                .Where(c => c.Sex == "Female")
                .OrderBy(c => c.Legband_Id) // adjust if your property is LegbandId
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Legband_Id
                })
                .ToListAsync();
            mothers.Insert(0, new SelectListItem { Value = "", Text = "Unknown" });

            // Fathers: active males
            var fathers = await _context.Chickens
                .Where(c => c.Sex == "Male")
                .OrderBy(c => c.Legband_Id)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Legband_Id
                })
                .ToListAsync();
            fathers.Insert(0, new SelectListItem { Value = "", Text = "Unknown" });

            ViewBag.Mothers = mothers;
            ViewBag.Fathers = fathers;

            return View();
        }

        public IActionResult EditChicken(int id)
        {
            var chicken = _context.Chickens.FirstOrDefault(c => c.Id == id);
            if (chicken == null)
            {
                return NotFound();
            }
            return View(chicken);
        }


        public async Task<IActionResult> AddNewChicken(Chicken chicken)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chicken);
                await _context.SaveChangesAsync();
                return RedirectToAction("Overview");
            }
            ViewBag.Breeds = YourChickenGuide.Data.BreedList.Breeds ?? new List<string>();

            ViewBag.Breeds = YourChickenGuide.Data.BreedList.Breeds ?? new List<string>();
            ViewBag.Statuses = YourChickenGuide.Data.StatusList.Statuses ?? new List<string>();

            var mothers = await _context.Chickens
                .Where(c => c.Sex == "Female")
                .OrderBy(c => c.Legband_Id)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Legband_Id })
                .ToListAsync();
            mothers.Insert(0, new SelectListItem { Value = "", Text = "Unknown" });

            var fathers = await _context.Chickens
                .Where(c => c.Sex == "Male")
                .OrderBy(c => c.Legband_Id)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Legband_Id })
                .ToListAsync();
            fathers.Insert(0, new SelectListItem { Value = "", Text = "Unknown" });

            ViewBag.Mothers = mothers;
            ViewBag.Fathers = fathers;

 
            return View("AddChicken", chicken);
        }
        [HttpGet]
        public async Task<IActionResult> EditExistingChicken(int id)
        {
            var chicken = await _context.Chickens.FindAsync(id);
            if (chicken == null)
            {
                               return NotFound();
            }
            return View("EditChicken", chicken);
        }
        [HttpPost]
        public async Task<IActionResult> EditExistingChicken(int id, Chicken chicken)
        {
            if (id != chicken.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var existingChicken = await _context.Chickens.FindAsync(id);
                if (existingChicken == null)
                {
                    return NotFound();
                }

                // Update only the properties you want to allow editing
                existingChicken.Legband_Id = chicken.Legband_Id;
                existingChicken.HatchDate = chicken.HatchDate;
                existingChicken.Breed = chicken.Breed;
                existingChicken.Color = chicken.Color;
                existingChicken.Notes = chicken.Notes;
                existingChicken.Sex = chicken.Sex;
                existingChicken.Status = chicken.Status;

                await _context.SaveChangesAsync();
                return RedirectToAction("ViewChicken", new {id = existingChicken.Id});
            }
            return View("ViewChicken", chicken);
        }

    }
}
