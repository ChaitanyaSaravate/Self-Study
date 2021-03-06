﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Public.CompulsorySchool;
using Abstractions.Public.CompulsorySchool.Entities;
using Abstractions.Public.CompulsorySchool.ServiceContracts;

namespace DataProviderApi
{
    public class GradesService : IArchiveGrades
    {
        public IEnumerable<StudentGrades> GetGrades(GradesDataRequest getGradesRequest)
        {
            //TODO: Filter data as per request
            return FakeData.GetStudentGrades();
        }
    }
}
