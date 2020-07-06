using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
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

        public async Task<bool> CreateArchiveFiles(EduEntity entityToArchive, List<string> dataDownloadedInFiles)
        {
            var deserializer = new EntityDataDeserializer();
            List<object> datasetToArchive = null;

            if (entityToArchive.EntityType == SupportedEduEntityTypes.Grade)
            {
                var studentGradesData = await deserializer.GetDeserializedData<StudentGrades>(dataDownloadedInFiles[0]);
                await Task.WhenAll(studentGradesData.Select(record =>
                    Task.Run(async () =>
                    {
                        _filesManager.CreateArchiveFile(entityToArchive.Name, record);
                    })));
            }
            else if (entityToArchive.EntityType == SupportedEduEntityTypes.Student)
            {
                var studentInfoRecords = await deserializer.GetDeserializedData<StudentInfo>(dataDownloadedInFiles[0]);
                await Task.WhenAll(studentInfoRecords.Select(record =>
                    Task.Run(async () =>
                    {
                        _filesManager.CreateArchiveFile(entityToArchive.Name, record);
                    })));
            }
            else if (entityToArchive.EntityType == SupportedEduEntityTypes.Absence)
            {
                var absenceRecords = await deserializer.GetDeserializedData<StudentAbsences>(dataDownloadedInFiles[0]);
                await Task.WhenAll(absenceRecords.Select(record =>
                    Task.Run(async () =>
                    {
                        _filesManager.CreateArchiveFile(entityToArchive.Name, record);
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
