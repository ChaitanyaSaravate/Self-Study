using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Authenticator
{
	public static class IdpServiceExtension
	{
		public static AuthenticationBuilder AddIdpService(this AuthenticationBuilder builder, Action<IdpRemoteAuthOptions> options)
		{
			builder.Services.Add(ServiceDescriptor.Singleton<IPostConfigureOptions<IdpRemoteAuthOptions>, IdpPostConfigurationOptions>());
			builder.Services.Add(ServiceDescriptor.Transient<IAuthenticationService, IdpAuthService>());
			builder.Services.AddAuthentication()
				.AddRemoteScheme<IdpRemoteAuthOptions, IdpAuthenticationHandler>("idp", "idp", options);

			return builder;
		}
	}
}
