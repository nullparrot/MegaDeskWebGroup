using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskWebGroup.Data;
using MegaDeskWebGroup.Models;

namespace MegaDeskWebGroup.Pages.Quotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDeskWebGroup.Data.MegaDeskWebGroupContext _context;

        public IndexModel(MegaDeskWebGroup.Data.MegaDeskWebGroupContext context)
        {
            _context = context;
        }

        public IList<Quote> Quote { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Quote = await _context.Quote.ToListAsync();
        }
    }
}
