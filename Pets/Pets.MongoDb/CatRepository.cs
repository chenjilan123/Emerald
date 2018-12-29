using Pets.Model;
using Pets.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pets.MongoDb
{
    internal class CatRepository : ICatRepository
    {
        public Task AddAsync(Cat cat)
        {
            throw new NotImplementedException();
        }

        public Task<Cat> GetCatAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cat>> GetCatsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
