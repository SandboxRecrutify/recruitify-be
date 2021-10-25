using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Recrutify.Services.ISRecrutify.Setting
{
    public class IdentityServerSettings
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResource
                {
                    Name = JwtClaimTypes.Role,
                    UserClaims = new List<string>()
                    {
                        JwtClaimTypes.Role,
                    },
                },
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "recruitify_api",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    RequireClientSecret = false,
                    ClientName = "Recruitify API",
                    AllowedScopes =
                    {
                        "recruitify_api",
                    },
                },
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("recruitify_api", "Recruitify API")
                {
                    Scopes =
                    {
                        "recruitify_api",
                    },
                },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("recruitify_api", "Full Access to Recruitify API"),
            };
        }
    }
}
