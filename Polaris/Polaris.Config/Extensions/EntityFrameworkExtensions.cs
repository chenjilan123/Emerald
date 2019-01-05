using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polaris.Config.EFConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polaris.Config.Extensions
{
    /// <summary>
    /// Extensions about EntityFramework
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        public static IConfigurationBuilder AddEFConfiguration(
            this IConfigurationBuilder builder,
            Action<DbContextOptionsBuilder> optionsAction)
        {
            return builder.Add(new EFConfigurationSource(optionsAction));
        }
    }
}
