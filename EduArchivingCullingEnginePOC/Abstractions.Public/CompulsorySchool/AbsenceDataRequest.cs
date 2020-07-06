using System;

namespace Abstractions.Public.CompulsorySchool
{
    public class AbsenceDataRequest : StudentArchiveDataRequest
    {
        public bool AbsentStudentsDataOnly { get; set; }
    }
}
