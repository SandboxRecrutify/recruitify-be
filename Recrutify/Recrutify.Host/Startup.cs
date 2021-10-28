using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using Recrutify.DataAccess.Configuration;
using Recrutify.Host.Configuration;
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

            services.AddCors(cors =>
            {
                cors.AddPolicy(
                    "GetPolicy",
                    builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .WithMethods("GET"));

                cors.AddPolicy(
                    "PostPolice",
                    builder =>
                    builder.AllowAnyOrigin()
                    .WithMethods("POST"));

                cors.AddPolicy(
                    "PutPolice",
                    builder =>
                    builder.AllowAnyOrigin()
                    .WithMethods("PUT"));

                cors.AddPolicy(
                   "DeletePolice",
                   builder =>
                   builder.AllowAnyOrigin()
                   .WithMethods("DELETE"));
            });

            services.Configure<MongoSettings>(
                Configuration.GetSection(nameof(MongoSettings)));

            services.AddRepositories();
            services.AddServices();

            var mapper = MapperConfig.GetConfiguration()
                .CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers()
                .AddFluentValidation();
            services.AddValidators();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Recrutify.Host", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exadel Recritify v.1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
