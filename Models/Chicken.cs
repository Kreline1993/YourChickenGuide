namespace YourChickenGuide.Models
{
    public class Chicken
    {
        public int Id { get; set; }
        public string? Legband_Id { get; set; }
        [Required]
        public DateOnly HatchDate { get; set; }

        [Required]
        public string Breed { get; set; }

        public string? Color { get; set; }
        public string? Notes { get; set; }
        public string? Sex { get; set; }

        public bool Active { get; set; } = true;


    }
}
