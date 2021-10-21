using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Recrutify.Host
{
    class CustomProfileService : IProfileService
    {
        protected readonly ILogger Logger;
        protected readonly IUserRepository userRepository;

        public CustomProfileService(IUserRepository userRepository, ILogger<CustomProfileService> logger)
        {
            this.userRepository = userRepository;
            Logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {          
            Logger.LogDebug("Get profile called for subject {subject} from client {client} with claim types {claimTypes} via {caller}",
                context.Subject.GetSubjectId(),
                context.Client.ClientName ?? context.Client.ClientId,
                context.RequestedClaimTypes,
                context.Caller);

            var user = userRepository.FindBySubjectId(context.Subject.GetSubjectId());
            
            var claims = new List<Claim>//foreach
            {
                new Claim("role", user.),
                new Claim("role", "user"),
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = userRepository.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}
