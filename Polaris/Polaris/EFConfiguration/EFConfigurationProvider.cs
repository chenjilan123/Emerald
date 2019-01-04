using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polaris.EFConfiguration
{
    public class EFConfigurationProvider : ConfigurationProvider//IConfigurationProvider
    {
        Action<DbContextOptionsBuilder> OptionsAction { get; }
        public EFConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        /// <summary>
        /// (Understanding)
        /// </summary>
        public override void Load()
        {
            //Intialize builder
            var builder = new DbContextOptionsBuilder<EFConfigurationContext>();
            //Database config
            OptionsAction(builder);
            //Get config sections.
            //Data: Key-Value pairs that store all configurations.
            using (var dbContext = new EFConfigurationContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();

                Data = !dbContext.Values.Any()
                    ? CreateAndSaveDefaultValues(dbContext)
                    : dbContext.Values.ToDictionary(c => c.Id, c => c.Value);
            }
        }
        private static IDictionary<string, string> CreateAndSaveDefaultValues(
            EFConfigurationContext dbContext)
        {
            // Quotes (c)2005 Universal Pictures: Serenity
            // https://www.uphe.com/movies/serenity
            var configValues = new Dictionary<string, string>
            {
                { "quote1", "I aim to misbehave." },
                { "quote2", "I swallowed a bug." },
                { "quote3", "You can't stop the signal, Mal." }
            };

            dbContext.Values.AddRange(configValues
                .Select(kvp => new EFConfigurationValue
                {
                    Id = kvp.Key,
                    Value = kvp.Value
                })
                .ToArray());

            dbContext.SaveChanges();

            return configValues;
        }
    }
}
