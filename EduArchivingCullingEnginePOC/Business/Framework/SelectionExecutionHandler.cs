using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;

namespace Business.Framework
{
    /// <summary>
    /// A framework level handler which performs operations in stages. Kind of Chain Of Responsibility ... 
    /// NOTE: Since the fake master data does not break down Selection in entity-level selection, operation is performed on the entity directly.
    /// But anyway, after all, its entity for which operation will be performed. So it's OK for now in POC !
    /// </summary>
    public class SelectionExecutionHandler : ISelectionExecutionHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;
        private readonly CleanupHandler _cleanupHandler;
        private readonly InputOutputFilesManager _inputOutputFilesManager;
        Dictionary<SupportedEduEntityTypes, List<string>> _entityDataFilesMapper = new Dictionary<SupportedEduEntityTypes, List<string>>();

        public SelectionExecutionHandler(ArchiveHandlerFactory archiveHandlerFactory,
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
            
            await CreateArchiveFiles(archiveHandler);
            _inputOutputFilesManager.UpdateStatusFile(selection.Id, ArchiveStatuses.ArchiveFilesCreated);
            
            await DeleteTempFiles();
            _inputOutputFilesManager.UpdateStatusFile(selection.Id, ArchiveStatuses.Successful);
        }

        private async Task GetDataFromDataProvider(SelectionDefinition selection, IDomainHandler domainHandlerHandler)
        {
            foreach (var entity in selection.EntitiesToArchiveCull)
            {
                await Task.Run(async () =>
                {
                    //TODO: Better to get the request object from the handler and we call external data reader instead of asking handler to call external data reader?
                    //TODO: But looks like that will be complex as request object can be of any type which framework cannot know in advance.
                    var dataDownloadedFilesList = await domainHandlerHandler.GetDataAsync(entity);
                    _entityDataFilesMapper.Add(entity.EntityType, dataDownloadedFilesList);
                });
            }
        }

        private async Task CreateArchiveFiles(IDomainHandler domainHandlerHandler)
        {
            await domainHandlerHandler.CreateArchiveFilesAsync(_entityDataFilesMapper);
        }

        private async Task DeleteTempFiles()
        {
            foreach (var entity in _entityDataFilesMapper)
            {
                await Task.Run(async () =>
                {
                    _cleanupHandler.CleanupTemporaryData(entity.Value);
                });
            }
        }
    }
}
