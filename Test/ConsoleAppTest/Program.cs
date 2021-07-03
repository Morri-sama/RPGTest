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
using Core.Infrastructure.MongoDBMappings;
using MongoDB.Bson.Serialization.Serializers;

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


            BsonClassMap.RegisterClassMap<Unit>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(u => u.Id);
            });

            BsonClassMap.RegisterClassMap<Class>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id);
                cm.MapMember(c => c.AttackType).SetSerializer(new EnumSerializer<AttackType>(BsonType.String));
                cm.MapMember(c => c.DamageType).SetSerializer(new EnumSerializer<DamageType>(BsonType.String));
            });


            mongoDBSettings.CollectionNames.Add("Unit", "Units");
            mongoDBSettings.CollectionNames.Add("Class", "Classes");

            services.AddSingleton(mongoDBSettings);
            services.AddSingleton<IMongoClient>(new MongoClient(@"mongodb://localhost:27017"));
            services.AddScoped<IMongoDBRepository<Class>, MongoDBRepository<Class>>();
            services.AddScoped<IMongoDBRepository<Unit>, MongoDBRepository<Unit>>();
            services.AddScoped<IClassService, ClassService>();

            var serviceProvider = services.BuildServiceProvider();

            var classService = serviceProvider.GetRequiredService<IClassService>();

            classService.Insert(new Class { Name = "Тест", DamageType = DamageType.Magical, AttackType = AttackType.Melee });

            Console.WriteLine(")))))))");

            Console.ReadLine();
        }
    }
}
