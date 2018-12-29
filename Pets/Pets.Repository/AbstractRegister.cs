using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pets.Repository
{
    public abstract class AbstractRegister
    {
        public virtual void AddServices(IServiceCollection services)
        {
            RegisterBookRepository(services);
            RegisterCatRepository(services);
        }
        public abstract void RegisterBookRepository(IServiceCollection services);
        public abstract void RegisterCatRepository(IServiceCollection services);
    }
}
