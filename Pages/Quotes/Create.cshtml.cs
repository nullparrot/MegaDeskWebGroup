using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MegaDeskWebGroup.Data;
using MegaDeskWebGroup.Models;

namespace MegaDeskWebGroup.Pages.Quotes
{
    public class CreateModel : PageModel
    {
        private readonly MegaDeskWebGroup.Data.MegaDeskWebGroupContext _context;

        public CreateModel(MegaDeskWebGroup.Data.MegaDeskWebGroupContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Quote Quote { get; set; } = default!;

        private int CalculateCost(Quote quote)
        {
            // Calculate Area over 1000 in^2
            int area = Quote.Width * Quote.Depth;
            int areaOver = area - 1000;
            int areaCost = (areaOver > 0) ? areaOver * 1 : 0;

            //Calculate Drawer Cost
            int drawerCost = Quote.DrawerCount * 50;

            // Determine Material Cost
            int materialCost = Quote.SurfaceMaterial switch
            {
                SurfaceMaterial.Laminate => 100,
                SurfaceMaterial.Oak => 200,
                SurfaceMaterial.Rosewood => 300,
                SurfaceMaterial.Veneer => 125,
                SurfaceMaterial.Pine => 50,
                _ => throw new ArgumentOutOfRangeException()
            };

            // Calculate Rush Order Cost
            int rushOrderCost = Quote.RushDays switch
            {
                3 => (area >= 1000 && area <= 2000) ? 70 : ((area < 1000) ? 60 : 80),
                5 => (area >= 1000 && area <= 2000) ? 50 : ((area < 1000) ? 40 : 60),
                7 => (area >= 1000 && area <= 2000) ? 35 : ((area < 1000) ? 30 : 40),
                _ => 0 // Nothing for no rush
            };

            // Return: Base Price + Extra Surface Area Cost + Drawer Cost + Surface Cost + Rush Order Cost
            return (200 + areaCost + drawerCost + materialCost + rushOrderCost);
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Set the QuoteDate to now
            Quote.QuoteDate = DateTime.Now;

            //Set Quote Cost
            Quote.TotalCost = CalculateCost(Quote);

            _context.Quote.Add(Quote);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
