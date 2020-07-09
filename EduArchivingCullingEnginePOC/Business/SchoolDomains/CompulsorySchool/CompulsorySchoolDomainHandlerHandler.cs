using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;
using Abstractions.Public.CompulsorySchool;
using Abstractions.Public.CompulsorySchool.Entities;
using Business.Framework;

namespace Business.SchoolDomains.CompulsorySchool
{
    //TODO: Improve it .. So many switch-cases / conditional blocks !
    public class CompulsorySchoolDomainHandlerHandler : IDomainHandler
    {
        private readonly IExternalDataReader _externalDataReader;
        private readonly InputOutputFilesManager _filesManager;

        public CompulsorySchoolDomainHandlerHandler(IExternalDataReader externalDataReader, InputOutputFilesManager filesManager)
        {
            _externalDataReader = externalDataReader;
            _filesManager = filesManager;
        }

        public async Task<List<string>> GetDataAsync(EduEntity entityToArchive)
        {
            // Main job here is to just prepare selection criterion according to business rule. Actual data is fetched using Framework's external data reader only. 

            object requestObject = null;

            if (entityToArchive.EntityType == SupportedEduEntityTypes.Student)
            {
                requestObject = new StudentArchiveDataRequest { DataOlderThanInYears = 5 };
            }
            else if (entityToArchive.EntityType == SupportedEduEntityTypes.Grades)
            {
                requestObject = new GradesDataRequest { GradeLessThan = 5, DataOlderThanInYears = 5 };
            }
            else if (entityToArchive.EntityType == SupportedEduEntityTypes.Absence)
            {
                requestObject = new AbsenceDataRequest { DataOlderThanInYears = 5, AbsentStudentsDataOnly = true };
            }

            return await _externalDataReader.ReadData(requestObject, entityToArchive);
        }

        public async Task<bool> CreateArchiveFilesAsync(Dictionary<SupportedEduEntityTypes, List<string>> entityDataFileMapper)
        {
            bool result = true;
            foreach (var entityToArchive in entityDataFileMapper.Keys)
            {
                result = await this.CreateArchiveFilesAsync(entityToArchive, entityDataFileMapper[entityToArchive]);
            }

            return result;
        }

        public async Task<bool> CreateArchiveFilesAsync(SupportedEduEntityTypes eduEntityType, List<string> dataDownloadedInFiles)
        {
            var deserializer = new EntityDataDeserializer();

            switch (eduEntityType)
            {
                // Note that in this handler, file is created per entity. So it's simple job when you know which entity to send to the file handler.
                //TODO: Read all files instead of first file.
                case SupportedEduEntityTypes.Grades:
                    await CreateArchiveFileForEachRecord(eduEntityType, await deserializer.GetDeserializedData<StudentGrades>(dataDownloadedInFiles[0]));
                    break;
                case SupportedEduEntityTypes.Student:
                    await CreateArchiveFileForEachRecord(eduEntityType, await deserializer.GetDeserializedData<StudentInfo>(dataDownloadedInFiles[0]));
                    break;
                case SupportedEduEntityTypes.Absence:
                    await CreateArchiveFileForEachRecord(eduEntityType, await deserializer.GetDeserializedData<StudentAbsences>(dataDownloadedInFiles[0]));
                    break;
                default:
                    throw new KeyNotFoundException();
            }

            return true;
        }

        private async Task CreateArchiveFileForEachRecord<T>(SupportedEduEntityTypes eduEntityType, IList<T> entityRecords)
        {
            await Task.WhenAll(entityRecords.Select(record =>
                Task.Run(async () => { _filesManager.CreateArchiveFile(eduEntityType.ToString(), record); })));
        }
    }
}
