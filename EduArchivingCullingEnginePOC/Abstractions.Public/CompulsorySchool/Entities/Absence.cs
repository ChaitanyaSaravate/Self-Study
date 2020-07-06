using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Public.CompulsorySchool.Entities
{
    public class Absence
    {
        public string Subject { get; set; }

        public bool WasPresent { get; set; }
    }
}
