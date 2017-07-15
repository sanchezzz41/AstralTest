using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.ContextDb;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Http;
using AstralTest.Database;

namespace AstralTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")
                , x => x.MigrationsAssembly("AstralTest")));

            services.AddMvc();

            //Тут добавляются наши биндинги интерфейсов
            services.AddServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(route =>
            {
                route.MapRoute("Default", "{controller=Home}/{action=GetUsers}/{id?}");
            });

            //Используем swagger для проверки контроллеров


            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "My api");
                });
            }
            //Тут делается миграция бд, если бд не существует
            app.ApplicationServices.GetService<DatabaseContext>().Database.Migrate();
        }

    }

}

