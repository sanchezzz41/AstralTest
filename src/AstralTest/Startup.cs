using System;
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
using AstralTest.Extensions;
using AstralTest.FileStore;
using AstralTest.GeoLocation;
using AstralTest.Identity.JWTModel;
using AstralTest.Sms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;

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
                    , x => x.MigrationsAssembly("AstralTest")));

            //Сервисы для аутификации и валидации пароля
            services.AddScoped<IHashProvider, Md5HashService>();
            services.AddScoped<IPasswordHasher<User>, Md5PasswordHasher>();

            services.AddIdentity<User, Role>()
                .AddRoleStore<RoleStore>()
                .AddUserStore<IdentityStore>()
                .AddPasswordValidator<Md5PasswordValidator>()
                .AddDefaultTokenProviders();

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

            services.AddJWTService(opt =>
            {
                opt.Audince = _tokenOption.Audince;
                opt.Issuer = _tokenOption.Issuer;
                opt.Key = _tokenOption.Key;
                opt.LifeTime = _tokenOption.LifeTime;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            //Обычная аутификация с помощью куки
            //app.UseIdentity();


            //Используем swagger для проверки контроллеров
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "My api");
                });
            }

            //Аутификация на основе токена
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    // укзывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = _tokenOption.Issuer,

                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = _tokenOption.Audince,
                    // будет ли валидироваться время существования
                    ValidateLifetime = true,

                    // установка ключа безопасности
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenOption.Key)),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,

                    //ClockSkew = TimeSpan.Zero
                }
            });
         
            app.UseMvc(route =>
            {
                route.MapRoute("Default", "{controller=Account}/{action=Login}/{id?}");
            });
            //Тут делается миграция бд, если бд не существует
            app.ApplicationServices.GetService<DatabaseContext>().Database.Migrate();
            //Иницилизурем 2 роли и 1го пользователя, если таковых нет
            app.ApplicationServices.GetService<DatabaseContext>().Initialize(app.ApplicationServices).Wait();
        }
    }
}