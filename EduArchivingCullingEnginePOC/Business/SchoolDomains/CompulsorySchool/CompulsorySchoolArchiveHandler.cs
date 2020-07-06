using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;
using Abstractions.Public.CompulsorySchool;

namespace Business.SchoolDomains.CompulsorySchool
{
    public class CompulsorySchoolArchiveHandler : IArchive
    {
        private readonly IDataReader _externalDataReader;

        public CompulsorySchoolArchiveHandler(IDataReader externalDataReader)
        {
            _externalDataReader = externalDataReader;
        }

        public async Task<bool> GetData(EduEntity entityToArchive)
        {
            object requestObject = null;

            if (entityToArchive.EntityType == SupportedEduEntityTypes.Student)
            {
                requestObject = new GetStudentRequest { AgeGreaterThan = 21 };
            }
            else if (entityToArchive.EntityType == SupportedEduEntityTypes.Grade)
            {
                requestObject = new GetGradesRequest { GradeLessThan = 5 };
            }

            return await _externalDataReader.ReadData(requestObject, entityToArchive);
        }

        public Task<bool> CreateArchiveFiles(EduEntity entityToArchive, List<string> dataDownloadedInFiles)
        {
            throw new NotImplementedException();
        }
    }
}
