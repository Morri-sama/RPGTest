using Core.Domain;
using Core.Infrastructure;
using Core.Infrastructure.Settings;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using static Core.Domain.Enums;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization.IdGenerators;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XDDD");

            var client = new MongoClient();
            var dbList = client.ListDatabases().ToList();

            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }

            var services = new ServiceCollection();

            MongoDBSettings mongoDBSettings = new MongoDBSettings()
            {
                DatabaseName = "RPG"
            };

            BsonClassMap.RegisterClassMap<EntityBase>(cm =>
            {
                cm.SetIsRootClass(true);
                cm.MapIdProperty(e => e.Id);
                cm.IdMemberMap.SetIdGenerator(StringObjectIdGenerator.Instance)
                                           .SetSerializer(new StringSerializer());
            });

            BsonClassMap.RegisterClassMap<Unit>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<UnitClass>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.AttackType).SetSerializer(new EnumSerializer<AttackType>(BsonType.String));
                cm.MapMember(c => c.DamageType).SetSerializer(new EnumSerializer<DamageType>(BsonType.String));
            });

            mongoDBSettings.CollectionNames.Add("Unit", "Units");
            mongoDBSettings.CollectionNames.Add("UnitClass", "UnitClasses");

            services.AddSingleton(mongoDBSettings);
            services.AddSingleton<IMongoClient>(new MongoClient(@"mongodb://localhost:27017"));
            services.AddScoped<IMongoDBRepository<UnitClass>, MongoDBRepository<UnitClass>>();
            services.AddScoped<IMongoDBRepository<Unit>, MongoDBRepository<Unit>>();
            services.AddScoped<IUnitClassService, UnitClassService>();

            var serviceProvider = services.BuildServiceProvider();

            var classService = serviceProvider.GetRequiredService<IUnitClassService>();

            classService.Insert(new UnitClass { Name = "Тест", DamageType = DamageType.Magical, AttackType = AttackType.Melee });

            Console.WriteLine(")))))))");

            Console.ReadLine();
        }
    }
}
