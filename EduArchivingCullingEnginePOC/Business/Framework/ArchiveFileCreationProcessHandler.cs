using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Framework
{
    public class ArchiveFileCreationProcessHandler
    {
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;

        public ArchiveFileCreationProcessHandler(ArchiveHandlerFactory archiveHandlerFactory)
        {
            _archiveHandlerFactory = archiveHandlerFactory;
            SelectionExecutionHandler.DataDownloaded += SelectionExecutionHandler_DataDownloaded;
        }

        private void SelectionExecutionHandler_DataDownloaded(object sender, Abstractions.Internal.Framework.DataDownloadedEventArgs eventArgs)
        {
            var archiveHandler = _archiveHandlerFactory.GetArchiveHandler(eventArgs.SelectionDefinition.SchoolDomain);
            archiveHandler.CreateArchiveFiles()
        }
    }
}
