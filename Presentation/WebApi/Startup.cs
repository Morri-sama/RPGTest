using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using RPGTest.Core.Domain;
using RPGTest.Core.Infrastructure;
using RPGTest.Core.Infrastructure.Settings;
using RPGTest.Core.Services;
using System;
using WebApi.AutoMapper.Profiles;
using static RPGTest.Core.Domain.Enums;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

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

            services.AddCors(options =>
            {
                options.AddPolicy(name: "xdd",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                  });
            });

            services.AddAutoMapper(new Type[]
            {
                typeof(UnitProfile),
                typeof(UnitClassProfile)
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(policy => policy.WithOrigins(@"http://localhost:32220", @"http://localhost:5000")
                                        .AllowAnyMethod()
                                        .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header")
                                        .AllowCredentials());


            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
