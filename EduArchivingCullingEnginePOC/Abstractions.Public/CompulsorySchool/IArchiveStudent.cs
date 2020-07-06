using System.Collections.Generic;
using Abstractions.Public.CompulsorySchool.Entities;

namespace Abstractions.Public.CompulsorySchool
{
    public interface IArchiveStudent
    {
        IEnumerable<StudentInfo> GetStudents(GetStudentRequest getStudentRequest);
    }
}
