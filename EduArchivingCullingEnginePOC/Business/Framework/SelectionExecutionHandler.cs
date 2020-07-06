using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Microsoft.AspNetCore.Hosting;

namespace Business.Framework
{
    public class SelectionExecutionHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;
        public static event FrameworkDelegates.DataDownloaded DataDownloaded;
        public static event FrameworkDelegates.DataArchived FilesCreated;

        public SelectionExecutionHandler(ArchiveHandlerFactory archiveHandlerFactory)
        {
            _archiveHandlerFactory = archiveHandlerFactory;
        }

        public async Task Run(SelectionDefinition selection)
        {
            var archiveHandler = _archiveHandlerFactory.GetArchiveHandler(selection.SchoolDomain);

            Dictionary<SupportedEduEntityTypes, bool> statusForEachObject = new Dictionary<SupportedEduEntityTypes, bool>();

            foreach (var entity in selection.EntitiesToArchiveCull)
            {
                await Task.Run(async () =>
                {
                    var isDataDownloadSuccessful = await archiveHandler.GetData(entity);
                    statusForEachObject.Add(entity.EntityType, isDataDownloadSuccessful);
                });
            }

            foreach (var entity in selection.EntitiesToArchiveCull)
            {
                DataDownloaded?.Invoke(this, new DataDownloadedEventArgs
                {
                    SelectionDefinition = selection,
                    RunId = 1, //TODO: Implement it
                    EntityToArchive = entity
                });
            }

        }
    }
}
