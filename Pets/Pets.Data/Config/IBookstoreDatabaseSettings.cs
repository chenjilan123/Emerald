using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pets.Data.Config
{
    public interface IBookstoreDatabaseSettings
    {
        string ConnectionString { get; set; }
        string BookstoreCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}
