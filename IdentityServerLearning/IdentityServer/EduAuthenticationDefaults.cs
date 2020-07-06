using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class EduAuthenticationDefaults
    {
        public const string AuthenticationScheme = "IdpWeb";

        public const string DomainId = "TESSA_META";

        public const string CallbackPath = "/signin-idpWeb";

        public static readonly string DisplayName = "IDP Web";

        public static readonly string AuthorizationEndpoint = "https://tecedupune.ap.tieto.com/HCW.Welfare.Common.IdentityPortalWeb/Start.aspx";

        public static readonly string TokenEndpoint = "http://tecedupune.ap.tieto.com:23610/HCW.Welfare.Common.IdentityProvider/REST/V1/IdentityProvider.aspx/ResolveArtifact/";
    }
}
