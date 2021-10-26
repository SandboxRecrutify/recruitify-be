using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using Recrutify.DataAccess.Models;

namespace Recrutify.Host
{
    internal class CustomProfileService : IProfileService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public CustomProfileService(IUserRepository userRepository, ILogger<CustomProfileService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(context.Subject.GetSubjectId()));

            //var roles = user.Roles.Select(r => r.ToString()).ToList();
            var roles = new List<string>();
            foreach(var role in user.Roles)
            {
                if(role == Role.Recruiter)
                {
                    roles.Add(nameof(Role.Recruiter));
                }
            }

            var a = JsonSerializer.Serialize(roles);
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Role, a, IdentityServerConstants.ClaimValueTypes.Json),
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(context.Subject.GetSubjectId()));
            context.IsActive = user != null;
        }
    }
}
