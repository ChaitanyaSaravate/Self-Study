using System;
using System.Collections.Generic;
using System.Text;
using Abstractions.Public.CompulsorySchool.Entities;

namespace Abstractions.Public.CompulsorySchool
{
    public interface IArchiveGrades
    {
        IEnumerable<StudentGrades> GetGrades(GetGradesRequest getGradesRequest);
    }
}
