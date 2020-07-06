using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Public.CompulsorySchool;
using Abstractions.Public.CompulsorySchool.Entities;

namespace DataProviderApi
{
    public class StudentService : IArchiveStudent
    {
        public IEnumerable<StudentInfo> GetStudents(GetStudentRequest getStudentRequest)
        {
            var students = FakeData.GetStudentsInfo(); 
            return students.FindAll(s => s.Age > getStudentRequest.AgeGreaterThan);
        }
    }
}
