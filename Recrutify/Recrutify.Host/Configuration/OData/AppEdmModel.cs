
using Microsoft.OData.Edm;
using Recrutify.Services.DTOs;
using Microsoft.OData.ModelBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutify.Host.Configuration
{
    public static class AppEdmModel
    {
        public static IEdmModel GetModel()
        {
            var builder = new ODataConventionModelBuilder();
            var products = builder.EntitySet<ProjectDTO>("Projects");

            products.HasReadRestrictions()
                .HasPermissions(p =>
                    p.HasSchemeName("Scheme").HasScopes(s => s.HasScope("Projects.Read")));
                

            return builder.GetEdmModel();
        }
    }
}
