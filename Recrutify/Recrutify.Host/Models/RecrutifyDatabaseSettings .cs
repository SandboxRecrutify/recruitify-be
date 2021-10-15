using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutify.Host.Models
{
    public class RecrutifyDatabaseSettings
    {
        public string RecrutifyCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
