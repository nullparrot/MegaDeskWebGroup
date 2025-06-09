using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaDeskWebGroup.Models;

namespace MegaDeskWebGroup.Data
{
    public class MegaDeskWebGroupContext : DbContext
    {
        public MegaDeskWebGroupContext (DbContextOptions<MegaDeskWebGroupContext> options)
            : base(options)
        {
        }

        public DbSet<MegaDeskWebGroup.Models.Quote> Quote { get; set; } = default!;
    }
}
