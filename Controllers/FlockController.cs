using Microsoft.AspNetCore.Mvc;
using YourChickenGuide.Data;
using YourChickenGuide.Models;


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
        public IActionResult AddChicken()
        {
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
            return View(chicken);
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
            return View(chicken);
        }

    }
}
