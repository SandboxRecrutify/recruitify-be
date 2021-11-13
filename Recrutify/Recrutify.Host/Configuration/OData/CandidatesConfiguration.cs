using System;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration
{
    public class CandidatesConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
            builder.EntitySet<CandidateDTO>("Candidates");
            builder.EntityType<CandidateDTO>().Collection
                   .Function("ByProject")
                   .ReturnsCollectionFromEntitySet<CandidateDTO>("Candidates");
        }
    }
    
}
