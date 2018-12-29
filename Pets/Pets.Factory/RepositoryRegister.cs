using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pets.Factory
{
    public static class RepositoryRegister
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            Repository.AbstractRegister register = new MongoDb.MongoRepositoriesRegister();
            //Repository.AbstractRegister register = new Sql.SqlRepositoriesRegister();
            register.AddServices(service);
            return service;
        }
    }
}
