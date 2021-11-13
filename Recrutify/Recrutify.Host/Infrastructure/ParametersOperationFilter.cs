using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Recrutify.Host.Infrastructure
{
    public class ParametersOperationFilter : IOperationFilter
    {
        private const string ParamVersion = "api-version";
        private const string ParamCount = "$count";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            var param = operation.Parameters.FirstOrDefault(p => p.Name == ParamVersion);
            if (param != null)
            {
                operation.Parameters.Remove(param);
            }

            param = operation.Parameters.FirstOrDefault(p => p.Name == ParamCount);
            if (param != null)
            {
                param.Schema.Default = null;
            }
        }
    }
}
