using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Settings
{
    public class MongoDBSettings
    {
        public Dictionary<string, string> CollectionNames { get; set; } = new Dictionary<string, string>();
        public string DatabaseName { get; set; }
    }
}
