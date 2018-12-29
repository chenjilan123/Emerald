using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pets.Repository;
using Pets.Sql;
using Pets.Sql.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pets.Sql
{
    public class SqlRepositoriesRegister : AbstractRegister
    {
        public override void AddServices(IServiceCollection services)
        {
            services.AddDbContext<CatContext>(op => op.UseInMemoryDatabase("CatInventory"));
            base.AddServices(services);
        }
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
