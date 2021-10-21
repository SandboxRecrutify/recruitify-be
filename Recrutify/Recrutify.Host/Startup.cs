using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Recrutify.Host.UserServices;
using Recrutify.Services.ISRecrutify.Setting;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recrutify.Host
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
            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();           

            services.AddIdentityServer()
                 .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerSettings.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerSettings.GetApiResources())
                .AddInMemoryApiScopes(IdentityServerSettings.GetApiScopes())
                .AddInMemoryClients(IdentityServerSettings.GetClients())
                .AddCustomUserStore();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
            .AddIdentityServerAuthentication(options =>
            {               
                options.Authority = "https://localhost:5001";
                options.ApiName = "recruitify_api";
                options.RequireHttpsMetadata = false;
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recrutify.Host", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),                            
                            Scopes = new Dictionary<string, string>
                            {
                                { "recruitify_api", "Full Access to Recruitify Api" }                               
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                }) ; 
                services.AddAuthorization();
                services.AddControllers();
            });                     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                             
            }
            
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseSwagger();   
            app.UseRouting();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exadel Recritify v.1");
                c.OAuthClientId("recruitify_api");                
                c.OAuthAppName("Recruitify Api");
            });

            
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });           
        }
    }
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            var isAuthorized = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
                              context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();


            if (!isAuthorized) return;

            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });


            var oauth2SecurityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
            };

            operation.Security.Add(new OpenApiSecurityRequirement()
            {
                [oauth2SecurityScheme] = new[] { "recruitify_api" } 

            });

        }
    }
}
