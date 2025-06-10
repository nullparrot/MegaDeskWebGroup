using System.ComponentModel.DataAnnotations;

namespace MegaDeskWebGroup.Models
{
    public class Quote
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string CustomerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime QuoteDate { get; set; }
        public decimal TotalCost { get; set; }

        [RegularExpression(@"2[4-9]|[3-8][0-9]|9[0-6]",
            ErrorMessage = "Please enter a number between 24 and 96.")]
        [Required]
        public int Width { get; set; }

        [RegularExpression(@"1[2-9]|[23][0-9]|4[0-8]",
            ErrorMessage = "Please enter a number between 12 and 48.")]
        [Required]
        public int Depth { get; set; }

        [RegularExpression(@"[0-7]",
            ErrorMessage = "Please enter a number between 0 and 6.")]
        [Required]
        public int DrawerCount { get; set; }

        [Required]
        public SurfaceMaterial SurfaceMaterial { get; set; }

        [RegularExpression(@"\b(0|3|5|7)\b",
            ErrorMessage = "Please enter a 0, 3, 5, or 7.")]
        [Required]
        public int RushDays { get; set; }
    }

    public enum SurfaceMaterial
    {
        Oak,
        Laminate,
        Pine,
        Rosewood,
        Veneer
    }
}
