using Core.Domain;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.MongoDBMappings
{
    public static class BsonClassMapExtensions
    {
        public static void MapUnits(this BsonClassMap bsonClassMap)
        {
            BsonClassMap.RegisterClassMap<Unit>(cm =>
            {
                cm.MapIdProperty(u => u.Id);
                cm.AutoMap();
            });
        }

        public static void MapClasses(this BsonClassMap bsonClassMap)
        {
            BsonClassMap.RegisterClassMap<Class>(cm =>
            {
                cm.MapIdProperty(c => c.Id);
                cm.AutoMap();
            });
        }
    }
}
