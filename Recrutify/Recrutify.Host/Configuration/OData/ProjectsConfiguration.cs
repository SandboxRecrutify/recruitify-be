using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration
{
    public class ProjectsConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
            builder.EntityType<ProjectDTO>();
            builder.ComplexType<ProjectPrimarySkillDTO>();
            builder.EntitySet<ShortProjectDTO>("Projects");
            builder.EntityType<ShortProjectDTO>().Collection
                  .Function("GetShortProjects")
                  .ReturnsCollectionFromEntitySet<ShortProjectDTO>("Projects");
        }
    }
}