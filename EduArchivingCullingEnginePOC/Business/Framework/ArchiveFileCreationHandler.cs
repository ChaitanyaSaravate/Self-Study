using Abstractions.Internal.Framework;

namespace Business.Framework
{
    public class ArchiveFileCreationHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;
        public static event FrameworkDelegates.ArchivingDelegate ArchiveFilesCreated;

        public ArchiveFileCreationHandler(ArchiveHandlerFactory archiveHandlerFactory)
        {
            _archiveHandlerFactory = archiveHandlerFactory;
            SelectionExecutionHandlerWithEvents.DataDownloaded += SelectionExecutionHandler_DataDownloaded;
        }

        private async void SelectionExecutionHandler_DataDownloaded(object sender, Abstractions.Internal.Framework.ArchivingEventArgs eventArgs)
        {
            var archiveHandler = _archiveHandlerFactory.GetArchiveHandler(eventArgs.SelectionDefinition.SchoolDomain);
            await archiveHandler.CreateArchiveFilesAsync(eventArgs.EntityDataFilesMapper);

            ArchiveFilesCreated?.Invoke(this, eventArgs);
        }
    }
}
