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

            OpenApiParameter param = operation.Parameters.FirstOrDefault(p => p.Name == "api-version");

            if (param != null)
            {
                operation.Parameters.Remove(param);
            }

            param = operation.Parameters.FirstOrDefault(p => p.Name == "$count");
            if (apiDescription.RelativePath.Contains("odata/") && param != null)
            {
                param.Schema.Default = new OpenApiBoolean(default);
            }
        }
    }
}
