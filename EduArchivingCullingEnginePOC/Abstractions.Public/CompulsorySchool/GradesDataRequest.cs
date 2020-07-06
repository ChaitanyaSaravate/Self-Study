using System;

namespace Abstractions.Public.CompulsorySchool
{
    public class GradesDataRequest : StudentArchiveDataRequest
    {
        public int GradeLessThan { get; set; }
    }
}
