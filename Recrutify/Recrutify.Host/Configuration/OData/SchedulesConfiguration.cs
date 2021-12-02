using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration
{
    public class SchedulesConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
            builder.EntitySet<ScheduleCandidateInfoDTO>("Schedules");
            builder.EntityType<ScheduleCandidateInfoDTO>().Collection
                  .Function("GetCandidatesPassedTest")
                  .ReturnsCollectionFromEntitySet<ScheduleCandidateInfoDTO>("Schedules");
            builder.EntityType<ScheduleCandidateInfoDTO>().Collection
                  .Function("GetUnassignedCandidates")
                  .ReturnsCollectionFromEntitySet<ScheduleCandidateInfoDTO>("Schedules");
        }
    }
}
