using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RazorPg.Models
{
    public class RazorPgContext : DbContext
    {
        public RazorPgContext (DbContextOptions<RazorPgContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPg.Models.SuperSomething> SuperSomething { get; set; }
    }
}
