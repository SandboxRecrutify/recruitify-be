using Microsoft.AspNetCore.Mvc.Filters;

namespace Recrutify.Host.Infrastructure.CustomsAuthorizationFilter
{
    public class ODataAuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
