using System;
using System.Collections.Generic;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework
{
    public class ArchivingEventArgs : EventArgs
    {
        public SelectionDefinition SelectionDefinition { get; set; }

        public int RunId { get; set; }
        public Dictionary<SupportedEduEntityTypes, List<string>> EntityDataFilesMapper { get; set; }
    }
}
