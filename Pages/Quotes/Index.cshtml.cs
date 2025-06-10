using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDeskWebGroup.Data;
using MegaDeskWebGroup.Models;
using Microsoft.Data.SqlClient;

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

        public string CurrentSort { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            CurrentSort = sortOrder;

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Quote> quotes = from q in _context.Quote
                                       select q;

            switch (sortOrder)
            {
                case "name_desc":
                    quotes = quotes.OrderByDescending(q => q.CustomerName);
                    break;
                case "Date":
                    quotes = quotes.OrderBy(q => q.QuoteDate);
                    break;
                case "date_desc":
                    quotes = quotes.OrderByDescending(q => q.QuoteDate);
                    break;
                default:
                    quotes = quotes.OrderBy(q => q.CustomerName);
                    break;
            }

            //search by name

            if (!string.IsNullOrEmpty(SearchString))
            {
                quotes = quotes.Where(n => n.CustomerName.Contains(SearchString));
            }

            Quote = await quotes.ToListAsync();
        }
    }
}
