using System.Collections.Generic;
using System.Linq;
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
        private readonly ArchiveFileCreationHandler _archiveFileCreationProcessHandler;
        private readonly CleanupHandler _cleanupHandler;

        //TODO: Try with events too.
        public static event FrameworkDelegates.DataDownloaded DataDownloaded;

        public SelectionExecutionHandler(ArchiveHandlerFactory archiveHandlerFactory,
            ArchiveFileCreationHandler archiveFileCreationProcessHandler,
            CleanupHandler cleanupHandler)
        {
            _archiveHandlerFactory = archiveHandlerFactory;
            _archiveFileCreationProcessHandler = archiveFileCreationProcessHandler;
            _cleanupHandler = cleanupHandler;
        }

        public async Task Run(SelectionDefinition selection)
        {
            var archiveHandler = _archiveHandlerFactory.GetArchiveHandler(selection.SchoolDomain);

            Dictionary<SupportedEduEntityTypes, List<string>> entityDataFilesMapper = new Dictionary<SupportedEduEntityTypes, List<string>>();

            foreach (var entity in selection.EntitiesToArchiveCull)
            {
                await Task.Run(async () =>
                {
                    var dataDownloadedFilesList = await archiveHandler.GetData(entity);
                    entityDataFilesMapper.Add(entity.EntityType, dataDownloadedFilesList);
                });
            }

            //var tasks = selection.EntitiesToArchiveCull.Select(entity =>
            //      Task.Run(async () =>
            //      {
            //          var dataDownloadedFilesList = await archiveHandler.GetData(entity);
            //          entityDataFilesMapper.Add(entity.EntityType, dataDownloadedFilesList);
            //      })).ToList();

            //await Task.WhenAll(tasks);

            foreach (var entity in entityDataFilesMapper)
            {
                await Task.Run(async () =>
                {
                    await archiveHandler.CreateArchiveFiles(
                        selection.EntitiesToArchiveCull.First(e => e.EntityType == entity.Key),
                        entity.Value);
                });

                //TODO: Try with events too.
                //DataDownloaded?.Invoke(this, new DataDownloadedEventArgs
                //    {
                //        SelectionDefinition = selection,
                //        RunId = 1, //TODO: Implement it
                //        EntityToArchive = selection.EntitiesToArchiveCull.First(e => e.EntityType == entity.Key),
                //        DataFilesToReadDataFrom = entity.Value
                //    });
                //}
            }

            foreach (var entity in entityDataFilesMapper)
            {
                await Task.Run(async () =>
                {
                    _cleanupHandler.CleanupTemporaryData(entity.Value);
                });
            }
        }
    }
}
