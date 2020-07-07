using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;

namespace Business.Framework
{
    public class SelectionExecutionHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;
        private readonly ArchiveFileCreationHandler _archiveFileCreationProcessHandler;
        private readonly CleanupHandler _cleanupHandler;
        private readonly ISelectionDefinitionDataAccess _selectionDefinitionDataAccess;
        Dictionary<SupportedEduEntityTypes, List<string>> entityDataFilesMapper = new Dictionary<SupportedEduEntityTypes, List<string>>();

        //TODO: Try with events too.
        public static event FrameworkDelegates.DataDownloaded DataDownloaded;

        public SelectionExecutionHandler(ArchiveHandlerFactory archiveHandlerFactory,
            ArchiveFileCreationHandler archiveFileCreationProcessHandler,
            CleanupHandler cleanupHandler, ISelectionDefinitionDataAccess selectionDefinitionDataAccess)
        {
            _archiveHandlerFactory = archiveHandlerFactory;
            _archiveFileCreationProcessHandler = archiveFileCreationProcessHandler;
            _cleanupHandler = cleanupHandler;
            _selectionDefinitionDataAccess = selectionDefinitionDataAccess;
        }

        public async Task Run(SelectionDefinition selection)
        {
            var archiveHandler = _archiveHandlerFactory.GetArchiveHandler(selection.SchoolDomain);

            await GetDataFromDataProvider(selection, archiveHandler);
            await CreateArchiveFiles(archiveHandler);
            await DeleteTempFiles();
        }

        private async Task GetDataFromDataProvider(SelectionDefinition selection, IArchive archiveHandler)
        {
            foreach (var entity in selection.EntitiesToArchiveCull)
            {
                await Task.Run(async () =>
                {
                    var dataDownloadedFilesList = await archiveHandler.GetData(entity);
                    entityDataFilesMapper.Add(entity.EntityType, dataDownloadedFilesList);
                });
            }
        }

        private async Task CreateArchiveFiles(IArchive archiveHandler)
        {
            await archiveHandler.CreateArchiveFiles(entityDataFilesMapper);
        }

        private async Task DeleteTempFiles()
        {
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
