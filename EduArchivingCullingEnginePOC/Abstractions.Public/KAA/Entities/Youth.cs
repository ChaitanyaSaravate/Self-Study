using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Public.KAA.Entities
{
    public class Youth : YouthBase
    {
        public List<Measures> Measures { get; set; }
        public List<Reminders> Reminders { get; set; }
    }
}
