using BlazorApp.AutoMapper.Profiles;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            string api = builder.Configuration["ApiUrl"];

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(api) });
            builder.Services.AddScoped<IHttpService, HttpService>();

            builder.Services.AddAutoMapper(new Type[]
            {
                typeof(UnitClassProfile),
                typeof(UnitProfile)
            });

            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
            });

            await builder.Build().RunAsync();
        }
    }
}
