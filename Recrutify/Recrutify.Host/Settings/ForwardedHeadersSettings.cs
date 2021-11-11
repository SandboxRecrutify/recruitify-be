using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace Recrutify.Host.Settings
{
    public class ForwardedHeadersSettings
    {
        public static ForwardedHeadersOptions Get()
        {
            var forwardOptions = new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost,
                RequireHeaderSymmetry = false,
            };
            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();
            return forwardOptions;
        }
    }
}
