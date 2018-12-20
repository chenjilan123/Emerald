using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Models
{
    public class AuroraContext : DbContext
    {
        public AuroraContext (DbContextOptions<AuroraContext> options)
            : base(options)
        {
        }

        public DbSet<Aurora.Models.PolarBear> PolarBear { get; set; }
    }
}
