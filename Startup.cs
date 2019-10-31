using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Helpers;
using WebApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Resource.Contract;
using WebApi.Manager.Contract;
using WebApi.Engine.Contract;
using WebApi.DataBase;
using Microsoft.EntityFrameworkCore;
using System;

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
            var connection = @"Server=LAPTOP-GF9N3PC5\SQLEXPRESS;Database=OnlineStore;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<DatabaseContext>
                (options => Console.WriteLine("aici"+options.UseSqlServer(connection)));
            Console.WriteLine("asdasd"+services);

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
               //Resource
            services.AddScoped<IProdusResource, ProdusResource>();
            services.AddScoped<IPozaResource, PozaResource>();
            services.AddScoped<IContactResource, ContactResource>();
            //Manager
            services.AddScoped<IProdusManager, ProdusManager>();
            services.AddScoped<IPozaManager, PozaManager>();
            services.AddScoped<IContactManager,ContactManager>();
            //validation
            services.AddScoped<IProdusValidationEngine, ProdusValidationEngine>();
            services.AddScoped<IPozaValidationEngine, PozaValidationEngine>();
            services.AddScoped<IContactValidationEngine, ContactValidationEngine>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc(
                           routes =>
                           {
                               routes.MapRoute(
                                   name: "default",
                                   template: "{controller}/{action=Index}/{id?}");
                           });
        }
    }
}
