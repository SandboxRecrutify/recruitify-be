using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.Host.UserServices
{
    public class CustomProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public CustomProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var user = await _userRepository.GetAsync(Guid.Parse(context.Subject.GetSubjectId()));
            var projectRoles = user.ProjectRoles.Select(pr => new { projectId = pr.Key.ToString(), roles = pr.Value.Select(r => r.ToString()) }).ToArray();
            var globalRoles = user.GlobalRoles.Select(gr => gr.ToString()).ToArray();
            var claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Role, JsonSerializer.Serialize(globalRoles), IdentityServerConstants.ClaimValueTypes.Json),
                new Claim(Constants.Roles.ProjectRoles, JsonSerializer.Serialize(projectRoles, options), IdentityServerConstants.ClaimValueTypes.Json),
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userRepository.GetAsync(Guid.Parse(context.Subject.GetSubjectId()));
            context.IsActive = user != null;
        }
    }
}
