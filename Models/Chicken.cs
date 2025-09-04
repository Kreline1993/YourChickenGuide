namespace YourChickenGuide.Models
{ 
using System.ComponentModel.DataAnnotations.Schema;


    public class Chicken
    {
        public int Id { get; set; }
        public string? Legband_Id { get; set; }
        public DateOnly HatchDate { get; set; }
        [NotMapped]
        public string LegbandPrefix { get; set; } = "EE NF";
        [NotMapped]
        public string LegbandNumber
        {
            get
            {
                if (string.IsNullOrEmpty(Legband_Id)) return "";
                // remove prefix if present
                return Legband_Id.StartsWith(LegbandPrefix)
                    ? Legband_Id.Substring(LegbandPrefix.Length).Trim()
                    : Legband_Id;
            }
            set
            {
                Legband_Id = $"{LegbandPrefix} {value}".Trim();
            }
        }
        public string? Breed { get; set; }
        public string? Color { get; set; }
        public string? Notes { get; set; }
        public string? Sex { get; set; }
        public string? Status { get; set; }
        public int? mother_Id { get; set; }
        public int? father_Id { get; set; }


    }
}
