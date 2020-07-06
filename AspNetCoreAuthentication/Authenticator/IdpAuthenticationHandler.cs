using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authenticator
{
	public class IdpAuthenticationHandler : RemoteAuthenticationHandler<IdpRemoteAuthOptions>
	{
		private IdpRemoteAuthOptions idpAuthOptions;
		private IAuthenticationService authService;

		public IdpAuthenticationHandler(IOptionsMonitor<IdpRemoteAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IAuthenticationService service) : base(options, logger, encoder, clock)
		{
			idpAuthOptions = options.Get("idp");
			authService = service;
		}

		protected override async Task<HandleRequestResult> HandleRemoteAuthenticateAsync()
		{
			return HandleRequestResult.Success(new AuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties(), "idp"));
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var val = this.HandleRemoteAuthenticateAsync();
			if (val.Result.Succeeded)
			{
				return AuthenticateResult.Success(val.Result.Ticket);
			}

			return AuthenticateResult.Fail("failed");
		}
	}

	public class IdpAuthService : IAuthenticationService
	{
		public async Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string scheme)
		{
			return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(), new AuthenticationProperties(), "idp"));
		}

		public Task ChallengeAsync(HttpContext context, string scheme, AuthenticationProperties properties)
		{
			return null;
		}

		public Task ForbidAsync(HttpContext context, string scheme, AuthenticationProperties properties)
		{
			throw new NotImplementedException();
		}

		public Task SignInAsync(HttpContext context, string scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
		{
			throw new NotImplementedException();
		}

		public Task SignOutAsync(HttpContext context, string scheme, AuthenticationProperties properties)
		{
			throw new NotImplementedException();
		}
	}
}
