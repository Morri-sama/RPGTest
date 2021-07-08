using RPGTest.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using static RPGTest.Core.Domain.Enums;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization.IdGenerators;
using RPGTest.Core.Domain;
using RPGTest.Core.Infrastructure.Settings;
using RPGTest.Core.Infrastructure;
using System.Text.RegularExpressions;
using System.Data;
using org.mariuszgromada.math.mxparser;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XDDD");

            //var client = new MongoClient();
            //var dbList = client.ListDatabases().ToList();

            //foreach (var db in dbList)
            //{
            //    Console.WriteLine(db);
            //}

            //var services = new ServiceCollection();

            //MongoDBSettings mongoDBSettings = new MongoDBSettings()
            //{
            //    DatabaseName = "RPG"
            //};

            //BsonClassMap.RegisterClassMap<EntityBase>(cm =>
            //{
            //    cm.SetIsRootClass(true);
            //    cm.MapIdProperty(e => e.Id);
            //    cm.IdMemberMap.SetIdGenerator(StringObjectIdGenerator.Instance)
            //                               .SetSerializer(new StringSerializer());
            //});

            //BsonClassMap.RegisterClassMap<Unit>(cm =>
            //{
            //    cm.AutoMap();
            //});

            //BsonClassMap.RegisterClassMap<UnitClass>(cm =>
            //{
            //    cm.AutoMap();
            //    cm.MapMember(c => c.AttackType).SetSerializer(new EnumSerializer<AttackType>(BsonType.String));
            //    cm.MapMember(c => c.DamageType).SetSerializer(new EnumSerializer<DamageType>(BsonType.String));
            //});

            //mongoDBSettings.CollectionNames.Add("Unit", "Units");
            //mongoDBSettings.CollectionNames.Add("UnitClass", "UnitClasses");

            //services.AddSingleton(mongoDBSettings);
            //services.AddSingleton<IMongoClient>(new MongoClient(@"mongodb://localhost:27017"));
            //services.AddScoped<IMongoDBRepository<UnitClass>, MongoDBRepository<UnitClass>>();
            //services.AddScoped<IMongoDBRepository<Unit>, MongoDBRepository<Unit>>();
            //services.AddScoped<IUnitClassService, UnitClassService>();

            //var serviceProvider = services.BuildServiceProvider();

            //var classService = serviceProvider.GetRequiredService<IUnitClassService>();

            //classService.Insert(new UnitClass { Name = "Тест", DamageType = DamageType.Magical, AttackType = AttackType.Melee });

            //Console.WriteLine(")))))))");

            Regex regex = new Regex(@"(?<BaseDamage>БазовыйУрон)|(?<MissingHealth>НедостающееЗдоровье)|(?<MaxHealth>МаксимальноеЗдоровье)|(?<Operation>\+|\-|\*|\/)");
            string formula = "БазовыйУрон + НедостающееЗдоровье / МаксимальноеЗдоровье * БазовыйУрон";

            var matchCollection = regex.Matches(formula);

            string result = string.Empty;

            for (int i = 0; i < matchCollection.Count; i++)
            {
                var match = matchCollection[i];


                if (match.Success)
                {
                    result += match.Value switch
                    {
                        "БазовыйУрон" => 10,
                        "НедостающееЗдоровье" => 20,
                        "МаксимальноеЗдоровье" => 200,
                        _ => match.Value
                    };
                }
            }

            Console.WriteLine(result);

            DataTable dataTable = new DataTable();

            var xdddd = dataTable.Compute(result,"");

            Console.WriteLine(xdddd);

            string mageFormula = "";
            string mageCondition = "ТекущаяМана > 0";
            string postCondition = "ТекущаяМана = ОкруглитьДоЦелого(ТекущаяМана / 2)";

            var pattern = "(?<ChangeableField>.*) = (?<RoundUp>ОкруглитьДоЦелого(?<RoundUpExpression>.*))";

            var x = 0.3 + 0.3 + 0.3;

            Console.WriteLine(x);

            Expression expression = new Expression("100 - ceil(100 / 2)");
            double result2 = expression.calculate();

            Expression expression2 = new Expression("5 < 1");
            double result3 = expression2.calculate();



            Console.WriteLine(result3);


            Console.ReadLine();
        }
    }
}
