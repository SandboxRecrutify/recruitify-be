using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration
{
    public class ProjectsConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
            var p = builder.EntitySet<ProjectDTO>("Projects");


        }
    }
}