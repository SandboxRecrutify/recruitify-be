using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.MemoryStorage;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using OData.Swagger.Services;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.Host.Configuration;
using Recrutify.Host.Extensions;
using Recrutify.Host.Identity;
using Recrutify.Host.Infrastructure;
using Recrutify.Host.Infrastructure.Authorization;
using Recrutify.Host.Settings;
using Recrutify.Services.Extensions;
using Recrutify.Services.Settings;

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Policies.AllAccessPolicy, policy => policy.RequireProjectRole(nameof(Role.Admin), nameof(Role.Recruiter), nameof(Role.Mentor), nameof(Role.Manager), nameof(Role.Interviewer)));
                options.AddPolicy(Constants.Policies.FeedbackPolicy, policy => policy.RequireProjectRole(nameof(Role.Recruiter), nameof(Role.Mentor), nameof(Role.Interviewer)));
                options.AddPolicy(Constants.Policies.AdminPolicy, policy => policy.RequireProjectRole(nameof(Role.Admin)));
                options.AddPolicy(Constants.Policies.HighAccessPolicy, policy => policy.RequireProjectRole(nameof(Role.Admin), nameof(Role.Manager)));
                options.AddPolicy(Constants.Policies.ManagerPolicy, policy => policy.RequireProjectRole(nameof(Role.Manager)));
            });

            services.AddHttpContextAccessor();
            services.AddRepositories();
            services.AddServices();

            var mapper = MapperConfig.GetConfiguration()
                .CreateMapper();
            services.AddSingleton(mapper);

            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

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
            services.AddSingleton<IAuthorizationHandler, RolesPolicyHandler>();
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, VersionedODataModelBuilder modelBuilder, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpStatusExceptionHandler();
            }

            app.UseForwardedHeaders(ForwardedHeadersSettings.Get());
            app.UseCors(Constants.Cors.CorsForUI);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Filter().Count().OrderBy().MaxTop(100);
                endpoints.MapVersionedODataRoute("odata", "odata", modelBuilder.GetEdmModels());
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exadel Recruitify");
                c.OAuthClientId("recruitify_api");
                c.OAuthAppName("Recruitify Api");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
