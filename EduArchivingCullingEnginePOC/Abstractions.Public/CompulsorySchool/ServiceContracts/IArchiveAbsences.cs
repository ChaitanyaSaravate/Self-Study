using System.Collections.Generic;
using Abstractions.Public.CompulsorySchool.Entities;

namespace Abstractions.Public.CompulsorySchool.ServiceContracts
{
    public interface IArchiveAbsences
    {
        IEnumerable<StudentAbsences> GetAbsenceses(AbsenceDataRequest absenceDataRequest);
    }
}
