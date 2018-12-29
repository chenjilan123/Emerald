using Microsoft.Extensions.DependencyInjection;
using Pets.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pets.MongoDb
{
    public class MongoRepositoriesRegister : AbstractRegister
    {
        public override void RegisterBookRepository(IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
        }

        public override void RegisterCatRepository(IServiceCollection services)
        {
            services.AddScoped<ICatRepository, CatRepository>();
        }
    }
}
