using System;
using System.IO;
using System.Reflection;
using GlaucomaWay.Models;
using GlaucomaWay.Repositories;
using GlaucomaWay.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace GlaucomaWay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GlaucomaWay", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var controllersXmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(controllersXmlPath, true);
            });

            services.AddDbContext<GlaucomaDbContext>(options => options.UseSqlServer(Configuration.GetSection("DbSettings")["ConnectionString"]));

            services.AddScoped<IVf14Repository, Vf14Repository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GlaucomaWay v1"));
            }

            app.UseSerilogRequestLogging();

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
