using System.Collections.Generic;
using Abstractions.Public.CompulsorySchool.Entities;

namespace Abstractions.Public.CompulsorySchool.ServiceContracts
{
    public interface IArchiveStudent
    {
        IEnumerable<StudentInfo> GetStudents(StudentArchiveDataRequest archiveDataRequest);
    }
}
