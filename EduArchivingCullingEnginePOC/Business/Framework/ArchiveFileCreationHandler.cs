namespace Business.Framework
{
    public class ArchiveFileCreationHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;

        public ArchiveFileCreationHandler(ArchiveHandlerFactory archiveHandlerFactory)
        {
            _archiveHandlerFactory = archiveHandlerFactory;
            SelectionExecutionHandler.DataDownloaded += SelectionExecutionHandler_DataDownloaded;
        }

        private void SelectionExecutionHandler_DataDownloaded(object sender, Abstractions.Internal.Framework.DataDownloadedEventArgs eventArgs)
        {
            var archiveHandler = _archiveHandlerFactory.GetArchiveHandler(eventArgs.SelectionDefinition.SchoolDomain);
            archiveHandler.CreateArchiveFiles(eventArgs.EntityToArchive.EntityType, eventArgs.DataFilesToReadDataFrom);
        }
    }
}
