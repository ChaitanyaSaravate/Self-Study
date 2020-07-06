//using System.Text.Encodings.Web;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

//namespace IdentityServer
//{
//    public class EduAuthenticationHandler : RemoteAuthenticationHandler<EduAuthenticationOptions>
//    {
//        public EduAuthenticationHandler(IOptionsMonitor<EduAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
//        {

//        }
        
//        protected override Task<HandleRequestResult> HandleRemoteAuthenticateAsync()
//        {
//            return new Task<HandleRequestResult>(() =>
//            {
//                return HandleRequestResult.Success(new AuthenticationTicket(null, EduAuthenticationDefaults.AuthenticationScheme));
//            });
//        }

//        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
//        {
//            return base.HandleChallengeAsync(properties);
//        }
//    }
//}
