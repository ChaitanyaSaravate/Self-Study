using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Authenticator
{
	public class IdpRemoteAuthOptions : RemoteAuthenticationOptions
	{
		//public IdpRemoteAuthOptions()
		//{

		//}

		public string IdpUrl { get; set; }

		public string DomainId { get; set; }
	}

	public class IdpPostConfigurationOptions : IPostConfigureOptions<IdpRemoteAuthOptions>
	{
		//public IdpPostConfigurationOptions()
		//{

		//}

		public void PostConfigure(string name, IdpRemoteAuthOptions options)
		{
			if (string.IsNullOrWhiteSpace(options.DomainId) || string.IsNullOrWhiteSpace(options.IdpUrl))
			{
				throw new Exception("Required auth options not specified.");
			}
		}
	}
}
