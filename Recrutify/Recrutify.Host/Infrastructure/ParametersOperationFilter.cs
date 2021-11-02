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

            if (operation.Parameters.Any(p => p.Name == "api-version"))
            {
                operation.Parameters.Remove(operation.Parameters.First(p => p.Name == "api-version"));
            }

            if (apiDescription.RelativePath.Contains("odata/"))
            {
                OpenApiParameter param;

                if ((param = operation.Parameters.FirstOrDefault(p => p.Name == "$expand")) != null)
                {
                    operation.Parameters.Remove(param);
                }

                if ((param = operation.Parameters.FirstOrDefault(p => p.Name == "$select")) != null)
                {
                    operation.Parameters.Remove(param);
                }

                if ((param = operation.Parameters.FirstOrDefault(p => p.Name == "$count")) != null)
                {
                    param.Schema.Default = new OpenApiBoolean(default);
                }
            }
        }
    }
}
