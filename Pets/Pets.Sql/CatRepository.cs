using Microsoft.EntityFrameworkCore;
using Pets.Model;
using Pets.Repository;
using Pets.Sql.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Sql
{
    internal class CatRepository : ICatRepository
    {
        private readonly CatContext _context;
        public CatRepository(CatContext context)
        {
            this._context = context;

            if (_context.Cats.Count() == 0)
            {
                _context.Cats.AddRange(
                    new Cat
                    {
                        Name = "Nifee Pedo",
                        Gender = Gender.Female,
                        Weight = 5.7F,
                    },
                    new Cat
                    {
                        Name = "Catee",
                        Gender = Gender.Female,
                        Weight = 5.5F,
                    });
                _context.SaveChanges();
            }
        }

        public async Task<Cat> GetCatAsync(int id)
        {
            return await _context.Cats.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Cat>> GetCatsAsync()
        {
            return await _context.Cats.ToListAsync();
        }
        public async Task AddAsync(Cat cat)
        {
            _context.Cats.Add(cat);
            await _context.SaveChangesAsync();
        }
    }
}
