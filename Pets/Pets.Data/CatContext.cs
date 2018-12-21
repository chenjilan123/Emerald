using Microsoft.EntityFrameworkCore;
using Pets.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Data
{
    public class CatContext : DbContext
    {
        public CatContext(DbContextOptions<CatContext> options) : base(options) { }
        public DbSet<Cat> Cats { get; set; }
    }
}
