//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;

//namespace IdentityServer
//{
//    public class EduAuthenticationOptions : RemoteAuthenticationOptions
//    {
//        public string DomainID { get; set; }
//        public EduAuthenticationOptions()
//        {
//            DomainID = "TESSA_META";
//            CallbackPath = new PathString("/signin-artifact");
//            //TokenEndpoint = EduAuthenticationDefaults.TokenEndpoint; 
//            //AuthorizationEndpoint = EduAuthenticationDefaults.AuthorizationEndpoint + $"?Domain={DomainID}";
//        }

//        public string AuthenticationEndpoint { get; set; } = EduAuthenticationDefaults.AuthorizationEndpoint;
//        public string TokenEndpoint { get; set; } = EduAuthenticationDefaults.TokenEndpoint;
//    }
//}
