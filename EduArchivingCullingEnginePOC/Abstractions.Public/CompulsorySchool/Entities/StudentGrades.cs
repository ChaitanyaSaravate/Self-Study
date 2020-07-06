using System;
using System.Collections.Generic;
using System.Text;

namespace Abstractions.Public.CompulsorySchool.Entities
{
    public class StudentGrades : StudentInfo
    {
        public IEnumerable<Grade> Grades { get; set; }
    }
}
