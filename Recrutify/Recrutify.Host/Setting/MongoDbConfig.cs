using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services.ISRecrutify.Setting
{
    class MongoDbConfig
    {
        public string Name { get; init; }
        public string Host { get; init; }
        public int Port { get; init; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
