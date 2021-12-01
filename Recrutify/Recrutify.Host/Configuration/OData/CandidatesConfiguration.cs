using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration
{
    public class CandidatesConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
           
            builder.EntitySet<CandidateDTO>("Candidates").HasManyBinding(x => x.ProjectResults, "ProjectResults");
            
            //builder.ComplexType<ProjectResultDTO>();

            //builder.ComplexType<CandidateDTO>().HasMany(a=>a.ProjectResults);

            //builder.EntitySet<CandidateDTO>("Candidates").EntityType.HasKey(t => t.Id);
            builder.EntityType<ProjectResultDTO>().HasKey(e => e.ProjectId);

            builder.ComplexType<CandidatePrimarySkillDTO>();
            builder.EntityType<CandidateDTO>().Collection
                   .Function("GetByProject")
                   .ReturnsCollectionFromEntitySet<CandidateDTO>("Candidates");
        }
    }
}
