using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;

namespace Business.Framework
{
    public class SelectionExecutionHandlerWithEvents : ISelectionExecutionHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;
        Dictionary<SupportedEduEntityTypes, List<string>> _entityDataFilesMapper = new Dictionary<SupportedEduEntityTypes, List<string>>();

        //TODO: Try with events too.
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

        private void GetDataFromDataProvider(SelectionDefinition selection, IArchive archiveHandler)
        {
            foreach (var entity in selection.EntitiesToArchiveCull)
            {
                var dataDownloadedFilesList = archiveHandler.GetDataAsync(entity).Result;
                _entityDataFilesMapper.Add(entity.EntityType, dataDownloadedFilesList);
            }
        }
    }
}
