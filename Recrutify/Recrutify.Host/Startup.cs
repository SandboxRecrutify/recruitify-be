using System;
using System.Collections.Generic;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Repositories;
using Recrutify.DataAccess.Repositories.Abstract;
using Recrutify.Host.Configuration;
using Recrutify.Host.Settings;
using Recrutify.Host.UserServices;
using Recrutify.Services.Extensions;
using Recrutify.Services.Services;
using Recrutify.Services.Services.Abstract;

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
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            services.Configure<MongoSettings>(
                Configuration.GetSection(nameof(MongoSettings)));
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<IProjectService, ProjectService>();
            services.AddSingleton<ICandidateRepository, CandidateRepository>();
            services.AddSingleton<ICandidateService, CandidateService>();

            services.AddRepositories();
            services.AddServices();

            var mapper = MapperConfig.GetConfiguration()
                .CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers()
                .AddFluentValidation();
            services.AddValidators();

            services.AddIdentityServer()
                 .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerSettings.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerSettings.GetApiResources())
                .AddInMemoryApiScopes(IdentityServerSettings.GetApiScopes())
                .AddInMemoryClients(IdentityServerSettings.GetClients())
                .AddCustomUserStore();

            var authority = Configuration["Authority"];
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = authority;
                options.ApiName = "recruitify_api";
            });

            var origins = Configuration.GetSection(nameof(CorsOriginsSettings)).Get<CorsOriginsSettings>().Origins;
            services.AddCors(cors =>
            {
                cors.AddPolicy(
                    "CorsForUI",
                    builder =>
                    builder.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recrutify.Host" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{authority}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "recruitify_api", "Full Access to Recruitify Api" },
                            },
                        },
                    },
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exadel Recritify");
                c.OAuthClientId("recruitify_api");
                c.OAuthAppName("Recruitify Api");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
