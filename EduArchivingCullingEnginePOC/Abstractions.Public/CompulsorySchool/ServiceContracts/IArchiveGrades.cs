using System.Collections.Generic;
using Abstractions.Public.CompulsorySchool.Entities;

namespace Abstractions.Public.CompulsorySchool.ServiceContracts
{
    public interface IArchiveGrades
    {
        IEnumerable<StudentGrades> GetGrades(GradesDataRequest getGradesRequest);
    }
}
