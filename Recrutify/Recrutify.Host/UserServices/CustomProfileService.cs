using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
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
            var user = await _userRepository.GetAsync(Guid.Parse(context.Subject.GetSubjectId()));

            List<string> roles = new List<string>();
            var claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.Email, user.Email),
            };

            foreach (var role in user.Roles)
            {
                roles.Add(role.ToString());
            }

            claims.Add(new Claim(JwtClaimTypes.Role, JsonSerializer.Serialize(roles), IdentityServerConstants.ClaimValueTypes.Json));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userRepository.GetAsync(Guid.Parse(context.Subject.GetSubjectId()));
            context.IsActive = user != null;
        }
    }
}
