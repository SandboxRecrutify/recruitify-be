﻿using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.DTOs;

namespace Recrutify.Host.Configuration
{
    public class ScheduleConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
        {
            builder.EntitySet<ScheduleCandidateInfoDTO>("Schedule");
            builder.EntityType<ScheduleCandidateInfoDTO>().Collection
                  .Function("GetNewCandidatesSlots")
                  .ReturnsCollectionFromEntitySet<ScheduleCandidateInfoDTO>("Schedule");
            builder.EntityType<ScheduleCandidateInfoDTO>().Collection
                  .Function("GetUnassignedCandidatesSlots")
                  .ReturnsCollectionFromEntitySet<ScheduleCandidateInfoDTO>("Schedule");
        }
    }
}
