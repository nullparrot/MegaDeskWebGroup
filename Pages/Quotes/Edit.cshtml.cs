using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDeskWebGroup.Data;
using MegaDeskWebGroup.Models;

namespace MegaDeskWebGroup.Pages.Quotes
{
    public class EditModel : PageModel
    {
        private readonly MegaDeskWebGroup.Data.MegaDeskWebGroupContext _context;

        public EditModel(MegaDeskWebGroup.Data.MegaDeskWebGroupContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quote Quote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quote =  await _context.Quote.FirstOrDefaultAsync(m => m.Id == id);
            if (quote == null)
            {
                return NotFound();
            }
            Quote = quote;
            return Page();
        }

        private decimal CalculateCost(Quote quote)
        {
            int area = quote.Width * quote.Depth;
            int areaOver = area - 1000;
            int areaCost = (areaOver > 0) ? areaOver * 1 : 0;

            int drawerCost = quote.DrawerCount * 50;

            int materialCost = quote.SurfaceMaterial switch
            {
                SurfaceMaterial.Laminate => 100,
                SurfaceMaterial.Oak => 200,
                SurfaceMaterial.Rosewood => 300,
                SurfaceMaterial.Veneer => 125,
                SurfaceMaterial.Pine => 50,
                _ => throw new ArgumentOutOfRangeException()
            };

            int rushOrderCost = quote.RushDays switch
            {
                RushDays.ThreeDays => (area >= 1000 && area <= 2000) ? 70 : (area < 1000 ? 60 : 80),
                RushDays.FiveDays => (area >= 1000 && area <= 2000) ? 50 : (area < 1000 ? 40 : 60),
                RushDays.SevenDays => (area >= 1000 && area <= 2000) ? 35 : (area < 1000 ? 30 : 40),
                RushDays.NoRush => 0,
                _ => 0
            };

            return 200m + areaCost + drawerCost + materialCost + rushOrderCost;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page();
            }

            Quote.TotalCost = CalculateCost(Quote);
            _context.Attach(Quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(Quote.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool QuoteExists(int id)
        {
            return _context.Quote.Any(e => e.Id == id);
        }
    }
}
