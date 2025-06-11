using System.ComponentModel.DataAnnotations;

namespace MegaDeskWebGroup.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Customer Name is required")]
        public required string CustomerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime QuoteDate { get; set; }

        public decimal TotalCost { get; set; }

        [RegularExpression(@"2[4-9]|[3-8][0-9]|9[0-6]", ErrorMessage = "Please enter a number between 24 and 96.")]
        [Required(ErrorMessage = "Width is required")]
        public int Width { get; set; }

        [RegularExpression(@"1[2-9]|[23][0-9]|4[0-8]", ErrorMessage = "Please enter a number between 12 and 48.")]
        [Required(ErrorMessage = "Depth is required")]
        public int Depth { get; set; }

        [RegularExpression(@"[0-7]", ErrorMessage = "Please enter a number between 0 and 7.")]
        [Required(ErrorMessage = "Drawer Count is required")]
        public int DrawerCount { get; set; }

        [Required(ErrorMessage = "Surface Material is required")]
        public SurfaceMaterial SurfaceMaterial { get; set; }

        [Required(ErrorMessage = "Rush Option is required")]
        public RushDays RushDays { get; set; }
    }

    public enum SurfaceMaterial
    {
        Oak,
        Laminate,
        Pine,
        Rosewood,
        Veneer
    }

    public enum RushDays
    {
        [Display(Name = "No Rush")]
        NoRush = 0,
        [Display(Name = "3 Days")]
        ThreeDays = 3,
        [Display(Name = "5 Days")]
        FiveDays = 5,
        [Display(Name = "7 Days")]
        SevenDays = 7
    }
}