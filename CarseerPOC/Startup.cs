using CarseerPOC.Helper;
using CarseerPOC.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CarseerPOC
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarseerPOC", Version = "v1" });
            });
            services.AddScoped<ICarsDetailsRepo, CarsDetailsRepo>();
            #region Localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddSingleton<IDictionary<string, string>>(provider =>
            {
                var resources = new Dictionary<string, string>();
                var supportedCultures = new[]
                {
            new CultureInfo("en"),
            // Add more cultures as needed
        };

                foreach (var culture in supportedCultures)
                {
                    var fileName = $"Resources/Resources.{culture.Name}.json";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                    if (File.Exists(filePath))
                    {
                        var jsonContent = File.ReadAllText(filePath);
                        var localizedStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);

                        foreach (var localizedString in localizedStrings)
                        {
                            resources[$"{localizedString.Key}:{culture.Name}"] = localizedString.Value;
                        }
                    }
                }

                return resources;
            });

            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarseerPOC v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
