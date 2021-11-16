using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration
{
    public class ProjectsConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
            builder.EntitySet<ProjectDTO>("Projects");
            builder.ComplexType<ProjectPrimarySkillDTO>();
            builder.EntityType<ProjectDTO>().Collection
                 .Function("GetAllProject")
                 .ReturnsCollectionFromEntitySet<ProjectDTO>("Projects");
           /* builder.EntitySet<ShortProjectDTO>("AllProjects");
            builder.EntityType<ShortProjectDTO>().Collection
                  .Function("GetAllProject")
                  .ReturnsCollectionFromEntitySet<ShortProjectDTO>("AllProjects");*/
        }
    }
}