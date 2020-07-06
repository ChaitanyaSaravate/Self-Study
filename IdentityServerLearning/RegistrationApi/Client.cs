using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace RegistrationApi
{
    public class Client : IdentityServer4.Models.Client
    {
        public IList<ApiResource> ApiResources { get; set; }
    }
}
