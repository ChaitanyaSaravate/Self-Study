using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;

namespace Business.Framework
{
    /// <summary>
    /// One more execution handler which works on C# events.
    /// NOTE: This gives unpredictable results because C# events getting messed up in async/await pattern.. 
    /// </summary>
    public class SelectionExecutionHandlerWithEvents : ISelectionExecutionHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;
        Dictionary<SupportedEduEntityTypes, List<string>> _entityDataFilesMapper = new Dictionary<SupportedEduEntityTypes, List<string>>();

        public static event FrameworkDelegates.ArchivingDelegate DataDownloaded;

        public SelectionExecutionHandlerWithEvents(ArchiveHandlerFactory archiveHandlerFactory,
            ArchiveFileCreationHandler archiveFileCreationProcessHandler,
            CleanupHandler cleanupHandler, ISelectionDefinitionDataAccess selectionDefinitionDataAccess)
        {
            _archiveHandlerFactory = archiveHandlerFactory;
        }

        public async Task Run(SelectionDefinition selection)
        {
            var archiveHandler = _archiveHandlerFactory.GetArchiveHandler(selection.SchoolDomain);

            GetDataFromDataProvider(selection, archiveHandler);

            DataDownloaded?.Invoke(this, new ArchivingEventArgs
            {
                EntityDataFilesMapper = _entityDataFilesMapper,
                SelectionDefinition = selection
            });

        }

        private void GetDataFromDataProvider(SelectionDefinition selection, IDomainHandler domainHandlerHandler)
        {
            foreach (var entity in selection.EntitiesToArchiveCull)
            {
                var dataDownloadedFilesList = domainHandlerHandler.GetDataAsync(entity).Result;
                _entityDataFilesMapper.Add(entity.EntityType, dataDownloadedFilesList);
            }
        }
    }
}
