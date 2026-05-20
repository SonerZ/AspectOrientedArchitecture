using Application.Business.Extensions;
using Application.Core.Configuration.Context;
using Application.Core.Configuration.Environment;
using Application.Core.Extensions;
using Application.Packages.Hashing.MD5.Extensions;
using Application.Packages.JWT.Entities;
using Application.Packages.JWT.Extensions;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Application.Core.Utilities.DependencyServiceTool;
using Application.Packages.Caching.InMemory.Extension;
using Application.Packages.HttpClient.Extension;
using Application.Packages.RabbitMQ.Extensions;
using Application.WebAPI.MQTT.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace Application.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigurationContext = new ApplicationConfigurationContext(new EnvironmentService());
        }
        public IConfiguration Configuration { get; }
        private IApplicationConfigurationContext ConfigurationContext { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to `UseServiceProviderFactory(new AutofacServiceProviderFactory())`
            // when building the host or this won't be called.
            
            builder.AddBusinessModule();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });

            services.AddAuthorization();
            
            services.AddControllers();
            
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(config => 
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWTToken kullanılarak oluşturulan Yetkilendirme tokenını buraya giriniz.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:5074")  
                        .AllowCredentials()  
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddSubscribers();
            services.AddHttpClientService();
            // Register core module. 🎉
            services.AddCoreModule();
            
           
            // Register business module. 🎉
            services.AddBusinessModule(ConfigurationContext);
          

            // Register MD5 module. 🎉
            services.AddMD5();

            services.AddJWT(configuration =>
            {
                configuration.SecretKey = ConfigurationContext.JWTKey;
                configuration.Issuer = ConfigurationContext.JWTIssuer;
                configuration.Audience = ConfigurationContext.JWTAudience;
                configuration.ExpiryHour = ConfigurationContext.JWTExpiryHour;
                configuration.TokenSecurityAlgorithms = EnumTokenSecurityAlgorithms.HmacSha256;
            });

            services.AddRabbitMQ(configuration =>
            {
                configuration.Host = ConfigurationContext.RabbitMQHost;
                configuration.Port = ConfigurationContext.RabbitMQPort;
                configuration.Username = ConfigurationContext.RabbitMQUsername;
                configuration.Password = ConfigurationContext.RabbitMQPassword;
            });

            services.AddInMemoryCache();
            
            //Register depencency Services
            DependencyServiceTool.CreateServiceProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                {
                    if (!httpRequest.Headers.ContainsKey("X-Forwarded-Host")) return;
                    var serverUrl = $"{httpRequest.Headers["X-Forwarded-Host"]}";
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = serverUrl } };
                });
            });

            app.UseSwaggerUI();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAPIExceptionMiddleware();

            app.UseHttpsRedirection();

            
            app.UseCors("AllowSpecificOrigin");

           // app.RegisterSubscribers();
            
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            
           // app.UseSubscribers();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
