using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Public.CompulsorySchool;
using Abstractions.Public.CompulsorySchool.Entities;

namespace DataProviderApi
{
    public class GradesService : IArchiveGrades
    {
        public IEnumerable<StudentGrades> GetGrades(GetGradesRequest getGradesRequest)
        {
            var students = FakeData.GetStudentGrades();
            return students;            
        }
    }
}
