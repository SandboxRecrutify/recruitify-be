﻿using Microsoft.AspNetCore.Builder;
using Recrutify.Host.Infrastructure;
using Recrutify.Services.Events;

namespace Recrutify.Host.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseHttpStatusExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void RegistrationStatusEvent(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var statusChangeEventProcessor = (StatusChangeEventProcessor)serviceProvider.GetService(typeof(StatusChangeEventProcessor));
            statusChangeEventProcessor.Subscribe();
        }
    }
}
