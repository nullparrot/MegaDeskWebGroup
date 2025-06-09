namespace MegaDeskWebGroup.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime QuoteDate { get; set; }
        public decimal TotalCost { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public int DrawerCount { get; set; }
        public SurfaceMaterial SurfaceMaterial { get; set; }
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
