using Microsoft.AspNetCore.Mvc;
using YourChickenGuide.Models.Genetics;

namespace YourChickenGuide.Controllers
{
    public class GeneticsController : Controller
    {
        public IActionResult ColorCalculatorIndex()
        {
            ViewBag.BaseColorDominanceOrder = ColorCalculator.BaseColorDominanceOrder;
            ViewBag.ColumbianPatternDominanceOrder = ColorCalculator.ColumbianPatternDominanceOrder;
            ViewBag.MottlingPatternDominanceOrder = ColorCalculator.MottlingPatternDominanceOrder;
            ViewBag.MelanoticPatternDominanceOrder = ColorCalculator.MelanoticPatternDominanceOrder;
            ViewBag.PatternGeneDominanceOrder = ColorCalculator.PatternGeneDominanceOrder;
            return View("ColorCalculator");
        }

        [HttpPost]
        public IActionResult CalculateColor(string E1, string E2, string Co1, string Co2, string Mo1, string Mo2, string Ml1, string Ml2, string Pg1, string Pg2)
        {
            var calculator = new ColorCalculator("Chicken", E1, E2, Co1, Co2, Mo1, Mo2, Ml1, Ml2, Pg1, Pg2);
            var baseColor = calculator.GetBaseColor();
            var columbianPattern = calculator.GetColumbianPattern();
            var mottlingPattern = calculator.GetMottlingPattern();
            var melanoticPattern = calculator.GetMelanoticPattern();
            var patternGene = calculator.GetPatternGenePattern();  
            ViewBag.BaseColor = baseColor;
            ViewBag.BaseColorDominanceOrder = ColorCalculator.BaseColorDominanceOrder;
            ViewBag.ColumbianPattern = columbianPattern;
            ViewBag.ColumbianPatternDominanceOrder = ColorCalculator.ColumbianPatternDominanceOrder;
            ViewBag.MottlingPattern = mottlingPattern;
            ViewBag.MottlingPatternDominanceOrder = ColorCalculator.MottlingPatternDominanceOrder;
            ViewBag.MelanoticPattern = melanoticPattern;
            ViewBag.MelanoticPatternDominanceOrder = ColorCalculator.MelanoticPatternDominanceOrder;
            ViewBag.PatternGene = patternGene;
            ViewBag.PatternGeneDominanceOrder = ColorCalculator.PatternGeneDominanceOrder;
            return View("ColorCalculator");
        }
    }
}
