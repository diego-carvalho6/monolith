using BGD.User.Repository.Contracts;
using BGD.User.Repository.Dapper.Postgres;
using BGD.User.Repository.Dapper.Postgres.Contracts;
using BGD.User.Repository.Dapper.Postgres.Database;
using BGD.User.Repository.Postgres;
using BGD.User.Repository.Postgres.Connection;
using BGD.User.Services;
using BGD.User.Services.Contracts;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text;
using BGD.User.Services.Exceptions;
using BGD.User.Services.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BGD.Common.Enviroment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BGD.User.API
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
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
                
            services.AddTransient<IPostgresConnectionFactory, PostgresConnectionFactory>();
            services.AddTransient(typeof(IPostgresRepository<>), typeof(PostgresRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<IBuyValueServices, BuyValueServices>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IClientServices, ClientServices>();
            services.AddTransient<IBuyValueRepository, BuyValueRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderUserRepository, OrderUserRepository>();
            services.AddTransient<IOrderUserServices, OrderUserServices>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();
            services.AddTransient<IOrderItemServices, OrderItemServices>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemServices, ItemServices>();
            services.AddTransient<IPayOutRepository, PayOutRepository>();
            services.AddTransient<IPayOutServices, PayOutServices>();
            services.AddTransient<IToDoListServices, ToDoListServices>();
            services.AddTransient<IToDoListRepository, ToDoListRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IQRRepository, QRRepository>();
            services.AddSingleton<IQRServices, QRServices>();
            services.AddSingleton<IRedirectRepository, RedirectRepository>();
            services.AddSingleton<IRedirectServices, RedirectServices>();
            services.AddSingleton<PostgresDatabase>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<JWTServices>();
            // services.AddLogging(c => c.AddFluentMigratorConsole())
            // .AddFluentMigratorCore()
            // .ConfigureRunner(c => c.AddPostgres11_0()
            // .WithGlobalConnectionString(Configuration.GetConnectionString("PostgresConnection"))
            // .ScanIn(typeof(ReferenceClass).Assembly).For.All());
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "BGD.User.API", Version = "v1" });
            // });
            // services.AddMvc(x => x.EnableEndpointRouting = false);
            
            services.AddCors(x =>
            {
                x.AddDefaultPolicy(builder => builder.AllowCredentials());
                x.AddPolicy("origins", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            });

            services.AddControllers()
                .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                
                opt.SerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new SnakeCaseNamingStrategy() });
            });
            // services.AddSwaggerGen(c =>
            // services.Configure<IdentityOptions>(options =>
            // {
            //     // Password settings.
            //     options.Password.RequireDigit = true;
            //     options.Password.RequireLowercase = true;
            //     options.Password.RequireNonAlphanumeric = true;
            //     options.Password.RequireUppercase = true;
            //     options.Password.RequiredLength = 6;
            //     options.Password.RequiredUniqueChars = 1;
            //
            //     // Lockout settings.
            //     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            //     options.Lockout.MaxFailedAccessAttempts = 5;
            //     options.Lockout.AllowedForNewUsers = true;
            //
            //     // User settings.
            //     options.User.AllowedUserNameCharacters =
            //         "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //     options.User.RequireUniqueEmail = false;
            // });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseSwagger();
            //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BGD.User.API v1"));
            // }
            app.UseGlobalExceptionHandler(loggerFactory);

            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseCors("origins");

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
