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
    public class CompulsorySchoolArchiveHandler : IArchive
    {
        private readonly IDataReader _externalDataReader;
        private readonly InputOutputFilesManager _filesManager;

        public CompulsorySchoolArchiveHandler(IDataReader externalDataReader, InputOutputFilesManager filesManager)
        {
            _externalDataReader = externalDataReader;
            _filesManager = filesManager;
        }

        public async Task<List<string>> GetData(EduEntity entityToArchive)
        {
            object requestObject = null;

            if (entityToArchive.EntityType == SupportedEduEntityTypes.Student)
            {
                requestObject = new StudentArchiveDataRequest { DataOlderThanInYears = 5 };
            }
            else if (entityToArchive.EntityType == SupportedEduEntityTypes.Grade)
            {
                requestObject = new GradesDataRequest { GradeLessThan = 5, DataOlderThanInYears = 5 };
            }
            else if (entityToArchive.EntityType == SupportedEduEntityTypes.Absence)
            {
                requestObject = new AbsenceDataRequest { DataOlderThanInYears = 5, AbsentStudentsDataOnly = true };
            }

            return await _externalDataReader.ReadData(requestObject, entityToArchive);
        }

        public async Task<bool> CreateArchiveFiles(Dictionary<SupportedEduEntityTypes, List<string>> entityDataFileMapper)
        {
            bool result = true;
            foreach (var entityToArchive in entityDataFileMapper.Keys)
            {
                result = await this.CreateArchiveFiles(entityToArchive, entityDataFileMapper[entityToArchive]);
            }

            return result;
        }

        public async Task<bool> CreateArchiveFiles(SupportedEduEntityTypes eduEntityType, List<string> dataDownloadedInFiles)
        {
            var deserializer = new EntityDataDeserializer();

            if (eduEntityType == SupportedEduEntityTypes.Grade)
            {
                var studentGradesData = await deserializer.GetDeserializedData<StudentGrades>(dataDownloadedInFiles[0]);
                await Task.WhenAll(studentGradesData.Select(record =>
                    Task.Run(async () =>
                    {
                        _filesManager.CreateArchiveFile(eduEntityType.ToString(), record);
                    })));
            }
            else if (eduEntityType == SupportedEduEntityTypes.Student)
            {
                var studentInfoRecords = await deserializer.GetDeserializedData<StudentInfo>(dataDownloadedInFiles[0]);
                await Task.WhenAll(studentInfoRecords.Select(record =>
                    Task.Run(async () =>
                    {
                        _filesManager.CreateArchiveFile(eduEntityType.ToString(), record);
                    })));
            }
            else if (eduEntityType == SupportedEduEntityTypes.Absence)
            {
                var absenceRecords = await deserializer.GetDeserializedData<StudentAbsences>(dataDownloadedInFiles[0]);
                await Task.WhenAll(absenceRecords.Select(record =>
                    Task.Run(async () =>
                    {
                        _filesManager.CreateArchiveFile(eduEntityType.ToString(), record);
                    })));
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return true;
        }
    }
}
