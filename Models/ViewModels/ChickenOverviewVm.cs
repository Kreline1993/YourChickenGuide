using Microsoft.AspNetCore.Mvc.Rendering;

namespace YourChickenGuide.Models.ViewModels
{
    public class ChickenOverviewVm
    {
        // Filters
        public string? Breed { get; set; }
        public string? Sex { get; set; }
        public string? Status { get; set; }
        public int? MotherId { get; set; }
        public int? FatherId { get; set; }
        public string? Search { get; set; }

        // Sorting
        public string? SortBy { get; set; }
        public string? SortDir { get; set; }

        // Data
        public IEnumerable<Chicken> Chickens { get; set; } = Enumerable.Empty<Chicken>();

        // Dropdown sources
        public IEnumerable<SelectListItem> Breeds { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Mothers { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Fathers { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
    }
}
