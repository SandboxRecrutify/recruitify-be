using Microsoft.AspNetCore.Http;
using Recrutify.Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;

namespace Recrutify.Host.UserServices
{
    public class HttpContextProvider : IHttpContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            return new Guid(_httpContextAccessor.HttpContext.User.GetSubjectId());
        }
    }
}
