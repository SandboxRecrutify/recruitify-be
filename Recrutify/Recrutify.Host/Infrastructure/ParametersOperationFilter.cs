using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Recrutify.Host.Infrastructure
{
    public class ParametersOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            OpenApiParameter param;

            if ((param = operation.Parameters.FirstOrDefault(p => p.Name == "api-version")) != null)
            {
                operation.Parameters.Remove(param);
            }

            if (apiDescription.RelativePath.Contains("odata/") && (param = operation.Parameters.FirstOrDefault(p => p.Name == "$count")) != null)
            {
                param.Schema.Default = new OpenApiBoolean(default);
            }
        }
    }
}
