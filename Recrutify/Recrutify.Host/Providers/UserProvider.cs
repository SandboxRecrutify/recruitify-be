using System;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Recrutify.Services.Providers;

namespace Recrutify.Host.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            return new Guid(_httpContextAccessor.HttpContext.User.GetSubjectId());
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.GetDisplayName();
        }
    }
}
