using Core.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Domain.Enums;

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
                cm.MapMember(c => c.AttackType).SetSerializer(new EnumSerializer<AttackType>(BsonType.String));
                cm.MapMember(c => c.DamageType).SetSerializer(new EnumSerializer<DamageType>(BsonType.String));
                cm.AutoMap();
            });
        }
    }
}
