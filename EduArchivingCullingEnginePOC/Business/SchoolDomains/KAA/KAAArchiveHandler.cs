using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;
using Abstractions.Public.KAA;
using Abstractions.Public.KAA.Entities;
using Business.Framework;

namespace Business.SchoolDomains.KAA
{
    public class KAAArchiveHandler : IArchive
    {
        private readonly IDataReader _externalDataReader;
        private readonly InputOutputFilesManager _filesManager;

        public KAAArchiveHandler(IDataReader dataReader, InputOutputFilesManager filesManager)
        {
            _externalDataReader = dataReader;
            _filesManager = filesManager;
        }

        public async Task<List<string>> GetData(EduEntity entityToArchive)
        {
            var requestObject = new GetKAARequest
            {
                NumOfMonthsFromPreviousMonth = 6,
                YouthAgeGreaterThan = 21
            };

            return await _externalDataReader.ReadData(requestObject, entityToArchive);
        }

        public async Task<bool> CreateArchiveFiles(Dictionary<SupportedEduEntityTypes, List<string>> entityDataFileMapper)
        {
            bool result = true;
            var deserializer = new EntityDataDeserializer();

            List<Youth> youths = new List<Youth>();

            foreach (var entityToArchive in entityDataFileMapper.Keys)
            {
                if (entityToArchive == SupportedEduEntityTypes.Measures)
                {
                    foreach (var file in entityDataFileMapper[entityToArchive])
                    {
                        var measures = await deserializer.GetDeserializedData<Measures>(file);

                        foreach (var measure in measures)
                        {
                            Youth youth = youths.Find(y => y.Id == measure.Youth.Id);

                            if (youth == null)
                            {
                                youth = new Youth
                                {
                                    Age = measure.Youth.Age,
                                    Id = measure.Youth.Id,
                                    Name = measure.Youth.Name,
                                    Measures = new List<Measures>(),
                                    Reminders = new List<Reminders>()
                                };

                                youths.Add(youth);
                            }

                            youth.Measures.Add(measure);
                        }
                    }
                }

                if (entityToArchive == SupportedEduEntityTypes.Reminders)
                {
                    foreach (var file in entityDataFileMapper[entityToArchive])
                    {
                        var reminders = await deserializer.GetDeserializedData<Reminders>(file);

                        foreach (var reminder in reminders)
                        {
                            Youth youth = youths.Find(y => y.Id == reminder.Youth.Id);

                            if (youth == null)
                            {
                                youth = new Youth
                                {
                                    Age = reminder.Youth.Age,
                                    Id = reminder.Youth.Id,
                                    Name = reminder.Youth.Name,
                                    Measures = new List<Measures>(),
                                    Reminders = new List<Reminders>()
                                };

                                youths.Add(youth);
                            }

                            youth.Reminders.Add(reminder);
                        }
                    }
                }
            }

            foreach (var youth in youths)
            {
                _filesManager.CreateArchiveFile(SupportedEduEntityTypes.Youth.ToString(), youth);
            }

            return result;
        }

        public async Task<bool> CreateArchiveFiles(SupportedEduEntityTypes eduEntityType, List<string> dataDownloadedInFiles)
        {
            return true;
        }
    }
}
