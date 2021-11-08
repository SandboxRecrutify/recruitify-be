using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Authorization;
using Microsoft.AspNetCore.OData.Authorization.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using OData.Swagger.Services;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.Host.Configuration;
using Recrutify.Host.Extensions;
using Recrutify.Host.Infrastructure;
using Recrutify.Host.Settings;
using Recrutify.Host.UserServices;
using Recrutify.Services.Extensions;

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

            var corsOrigins = Configuration["CorsOrigins"].Split(',');
            services.AddCors(cors =>
            {
                cors.AddPolicy(
                    Constants.Cors.CorsForUI,
                    builder =>
                    builder.WithOrigins(corsOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.AllAccessPolicy, policy => policy.RequireRole(nameof(Role.Admin), nameof(Role.Recruiter), nameof(Role.Mentor), nameof(Role.Manager), nameof(Role.Interviewer)));
                options.AddPolicy(Constants.Policies.FeedbackPolicy, policy => policy.RequireRole(nameof(Role.Recruiter), nameof(Role.Manager), nameof(Role.Interviewer)));
                options.AddPolicy(Constants.Policies.AdminPolicy, policy => policy.RequireRole(nameof(Role.Admin)));
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddOData().EnableApiVersioning();
            services.AddODataApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recrutify.Host" });
                c.OperationFilter<DescriptionsOperationFilter>();
                c.OperationFilter<ParametersOperationFilter>();
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
            services.AddOdataSwaggerSupport();
        }

        public void Configure(IApplicationBuilder app, VersionedODataModelBuilder modelBuilder, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpStatusExceptionHandler();
            }

            app.UseCors(Constants.Cors.CorsForUI);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Filter().Count().OrderBy().MaxTop(100);
                endpoints.MapVersionedODataRoute("odata", "odata", modelBuilder.GetEdmModels());
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
