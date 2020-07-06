using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingNetCore
{
    public static class InMemoryStorage
    {
        public static IList<School> Schools { get; set; }
        static InMemoryStorage()
        {
            Schools = new List<School>
            {
                new School("NMV High School", 1),
                new School("Pune International School", 2)
            };

        }
    }
}
