using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration
{
    public class ScheduleConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
            builder.EntitySet<AssignedCandidateDTO>("Schedule");
            builder.EntityType<CandidateDTO>();
            builder.ComplexType<ProjectResultDTO>();
            builder.ComplexType<CandidatePrimarySkillDTO>();
            builder.EntityType<AssignedCandidateDTO>().Collection
                  .Function("GetNewCandidateByProject")
                  .ReturnsCollectionFromEntitySet<AssignedCandidateDTO>("Schedule");
            builder.EntityType<AssignedCandidateDTO>().Collection
                  .Function("GetAssignedCandidateByProject")
                  .ReturnsCollectionFromEntitySet<AssignedCandidateDTO>("Schedule");
        }
    }
}
