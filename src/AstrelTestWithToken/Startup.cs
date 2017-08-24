using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using AstralTest.Database;
using AstralTest.Domain;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AstralTest.Identity;
using AstrelTestApi.Extensions;
using AstralTest.FileStore;
using AstralTest.GeoLocation;
using AstralTest.Identity.JWTAuthorization;
using AstralTest.Sms;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace AstrelTestApi
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

            string secretKey = "9661278F-4F9F-4424-A004-ABCA7EDF584E";
            var tokenOption = new TokenOption
            {
                Audince = "LocalServer",
                Issuer = "LocalHost",
                Key = secretKey,
                LifeTime = 30
            };
            _tokenOption = tokenOption;
        }

        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment _env;

        private readonly TokenOption _tokenOption;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")
                    /*, x => x.MigrationsAssembly("AstrelTestWithToken")*/));

            //Сервисы для аутификации и валидации пароля
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();

            if (_env.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"});
                    c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
                });
            }
            services.AddMvc().AddMvcOptions(opt =>
                {
                    opt.Filters.Add(typeof(ErrorFilter));
                    opt.Filters.Add(typeof(LoggerUsersFilter));
                }
            );
            services.AddMemoryCache();

            //services.AddScoped<ErrorFilter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //Тут добавляются наши биндинги интерфейсов
            services.AddServices();
            services.AddFileStoreServices(opt =>
            {
                opt.RootPath = "C:/Users/Alexander/Desktop/AstralRepositoy";
            });
            services.AddGeoService();
            services.AddSmsService();

            services.AddJwtService(opt =>
            {
                opt.Audince = _tokenOption.Audince;
                opt.Issuer = _tokenOption.Issuer;
                opt.Key = _tokenOption.Key;
                opt.LifeTime = _tokenOption.LifeTime;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    // укзывает, будет ли валидироваться издатель при валидации токена
                    x.TokenValidationParameters.ValidateIssuer = true;
                    // строка, представляющая издателя
                    x.TokenValidationParameters.ValidIssuer = _tokenOption.Issuer;

                    // будет ли валидироваться потребитель токена
                    x.TokenValidationParameters.ValidateAudience = true;
                    // установка потребителя токена
                    x.TokenValidationParameters.ValidAudience = _tokenOption.Audince;
                    // будет ли валидироваться время существования
                    x.TokenValidationParameters.ValidateLifetime = true;

                    // установка ключа безопасности
                    x.TokenValidationParameters.IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes((string) _tokenOption.Key));
                    // валидация ключа безопасности
                    x.TokenValidationParameters.ValidateIssuerSigningKey = true;

                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            //Используем swagger для проверки контроллеров
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "My api");
                });
            }

            ////Аутификация на основе токена
            app.UseAuthentication();

            app.UseMvc(route =>
            {
                route.MapRoute("Default", "{controller=Account}/{action=Login}/{id?}");
            });
            ////Тут делается миграция бд, если бд не существует
            //app.ApplicationServices.GetService<DatabaseContext>().Database.Migrate();
            ////Иницилизурем 2 роли и 1го пользователя, если таковых нет
            //app.ApplicationServices.GetService<DatabaseContext>().Initialize(app.ApplicationServices).Wait();
        }
    }
}