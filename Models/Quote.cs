using System.ComponentModel.DataAnnotations;

namespace MegaDeskWebGroup.Models
{
    public class Quote
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [Display(Name = "Customer Name")]
        public required string CustomerName { get; set; }

        [Required(ErrorMessage = "Quote Date is required")]
        public DateTime QuoteDate { get; set; }

        [Required(ErrorMessage = "Total Cost is required")]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Width is required")]
        [Range(24, 96, ErrorMessage = "Width must be between 24 and 96 inches")]
        public int Width { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Depth is required")]
        [Range(12, 48, ErrorMessage = "Depth must be between 12 and 48 inches")]
        public int Depth { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Drawer Count is required")]
        [Range(0, 7, ErrorMessage = "Drawer Count must be between 0 and 7")]
        public int DrawerCount { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "Surface Material is required")]
        public SurfaceMaterial SurfaceMaterial { get; set; }

        [ScaffoldColumn(false)]
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
        [Display(Name = "3 Days")]
        ThreeDays = 3,
        [Display(Name = "5 Days")]
        FiveDays = 5,
        [Display(Name = "7 Days")]
        SevenDays = 7,
        [Display(Name = "14 Days")]
        FourteenDays = 14,
        [Display(Name = "No Rush")]
        NoRush = 0
    }
}
