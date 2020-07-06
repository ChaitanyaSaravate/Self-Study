using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;
using Abstractions.Public.CompulsorySchool;
using Abstractions.Public.KAA;
using Business.Framework;

namespace Business.SchoolDomains.KAA
{
    public class KAAArchiveHandler : IArchive
    {
        private readonly IDataReader _externalDataReader;

        public KAAArchiveHandler(IDataReader dataReader)
        {
            _externalDataReader = dataReader;
        }

        public async Task<bool> GetData(EduEntity entityToArchive)
        {
            var requestObject = new GetKAARequest
            {
                NumOfMonthsFromPreviousMonth = 6,
                YouthAgeGreaterThan = 21
            };

            return await _externalDataReader.ReadData(requestObject, entityToArchive);
        }

        public Task<bool> CreateArchiveFiles(EduEntity entityToArchive, List<string> dataDownloadedInFiles)
        {
            throw new NotImplementedException();
        }
    }
}
