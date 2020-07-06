//using System;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.DependencyInjection;

//namespace IdentityServer
//{
//    public static class EduAuthenticationExtension
//    {
//        public static AuthenticationBuilder AddEduAuthentication(this AuthenticationBuilder builder,
//            Action<EduAuthenticationOptions> eduAuthenticationOptions)
//        {
//            return builder.AddRemoteScheme<EduAuthenticationOptions, EduAuthenticationHandler>(
//                EduAuthenticationDefaults.AuthenticationScheme, EduAuthenticationDefaults.DisplayName,
//                eduAuthenticationOptions);
//        }
//    }
//}
