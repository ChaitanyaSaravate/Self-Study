using System;
using System.Collections.Generic;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework
{
    public class DataDownloadedEventArgs : EventArgs
    {
        public SelectionDefinition SelectionDefinition { get; set; }

        public int RunId { get; set; }

        public List<string> ArchiveFiles { get; set; }

        public EduEntity EntityToArchive { get; set; }
    }

    public class ArchiveFilesCreatedEventArgs : EventArgs
    {
        public SelectionDefinition SelectionDefinition { get; set; }

        public EduEntity ArchivedEntity { get; set; }

        public int RunId { get; set; }
    }
}
