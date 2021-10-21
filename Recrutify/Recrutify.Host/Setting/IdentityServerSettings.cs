using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Recrutify.Services.ISRecrutify.Setting
{
    public class IdentityServerSettings
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
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
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 120, //86400,
                    IdentityTokenLifetime = 120, //86400,
                    UpdateAccessTokenClaimsOnRefresh = false,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RequireClientSecret = false,
                    AlwaysSendClientClaims = true,
                    Enabled = true,                  
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "recruitify_api"
                    },               
                    
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("recruitify_api","Data Event Records API", new List<string> {"role", "admin", "manager"})
                {
                    Scopes =
                    {
                        "recruitify_api",
                    }
                }
            };
        }

         public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope
                {
                    Name = "recruitify_api",
                    DisplayName = "Scope for the dataEventRecords ApiResource",
                    UserClaims = {"role", "admin", "manager" }
                }
            };
        }

    }
}
