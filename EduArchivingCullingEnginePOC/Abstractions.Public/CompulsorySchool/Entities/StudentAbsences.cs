using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Public.CompulsorySchool.Entities
{
    /// <summary>
    /// Student Info With Grades
    /// </summary>
    public class StudentAbsences : StudentInfo
    {
        public List<Absence> Absences { get; set; }
    }
}
