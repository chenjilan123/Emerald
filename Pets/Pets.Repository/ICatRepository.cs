using Pets.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Repository
{
    public interface ICatRepository
    {
        Task<Cat> GetCatAsync(int id);
        Task<List<Cat>> GetCatsAsync();
        Task AddAsync(Cat cat);
    }
}
