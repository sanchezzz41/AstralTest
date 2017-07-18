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
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AstralTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")
                , x => x.MigrationsAssembly("AstralTest")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            if (_env.IsDevelopment())
            {                
                //Временные настройки для авторизации
                services.Configure<IdentityOptions>(opt =>
                {
                    opt.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                    opt.Cookies.ApplicationCookie.LogoutPath = "/Account/Logout";

                //Pass settings
                opt.Password.RequiredLength = 5;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                });

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                });
            }
            services.AddMvc();

            //Тут добавляются наши биндинги интерфейсов
            services.AddServices();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIdentity();

            //Используем swagger для проверки контроллеров
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "My api");
                });
            }

            app.UseMvc(route =>
            {
                route.MapRoute("Default", "{controller=Account}/{action=Login}/{id?}");
            });
            //Тут делается миграция бд, если бд не существует
            app.ApplicationServices.GetService<DatabaseContext>().Database.Migrate();
            //Иницилизурем 2 роли и 1го пользователя, если таковых нет
            DatabaseInitialize(app.ApplicationServices).Wait();
        }

        public async Task DatabaseInitialize(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@gmail.com";
            string userName = "admin";
            string password = "admin";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(userName) == null)
            {
                User admin = new User { Email = adminEmail, UserName = userName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }

    }
}

