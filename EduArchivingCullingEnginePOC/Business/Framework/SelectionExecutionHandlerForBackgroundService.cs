using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;

namespace Business.Framework
{
    public class SelectionExecutionHandlerForBackgroundService : ISelectionExecutionHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;
        private readonly CleanupHandler _cleanupHandler;
        private readonly InputOutputFilesManager _inputOutputFilesManager;
        Dictionary<SupportedEduEntityTypes, List<string>> _entityDataFilesMapper = new Dictionary<SupportedEduEntityTypes, List<string>>();

        public SelectionExecutionHandlerForBackgroundService(ArchiveHandlerFactory archiveHandlerFactory,
            CleanupHandler cleanupHandler, InputOutputFilesManager inputOutputFilesManager)
        {
            _archiveHandlerFactory = archiveHandlerFactory;
            _cleanupHandler = cleanupHandler;
            _inputOutputFilesManager = inputOutputFilesManager;
        }

        public async Task Run(SelectionDefinition selection)
        {
            var archiveHandler = _archiveHandlerFactory.GetArchiveHandler(selection.SchoolDomain);

            await GetDataFromDataProvider(selection, archiveHandler);
            _inputOutputFilesManager.CreateStatusFile(selection.Id, ArchiveStatuses.DataDownloaded);
        }

        private async Task GetDataFromDataProvider(SelectionDefinition selection, IArchive archiveHandler)
        {
            foreach (var entity in selection.EntitiesToArchiveCull)
            {
                await Task.Run(async () =>
                {
                    var dataDownloadedFilesList = await archiveHandler.GetDataAsync(entity);
                    _entityDataFilesMapper.Add(entity.EntityType, dataDownloadedFilesList);
                });
            }
        }
    }
}
