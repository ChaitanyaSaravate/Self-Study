using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Public.CompulsorySchool;
using Abstractions.Public.CompulsorySchool.Entities;
using Abstractions.Public.CompulsorySchool.ServiceContracts;

namespace DataProviderApi
{
    public class StudentService : IArchiveStudent
    {
        public IEnumerable<StudentInfo> GetStudents(StudentArchiveDataRequest archiveDataRequest)
        {
            //TODO: Filter data as per request
            return FakeData.GetStudentsInfo(); 
           // return students.FindAll(s => s.Age > archiveDataRequest.DataOlderThanInYears);
        }
    }
}
