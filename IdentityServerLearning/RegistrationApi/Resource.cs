using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationApi
{
    public class Resource
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public Owner Owner { get; set; }

    }

    public class Owner
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
