using System;

namespace Abstractions.Internal.Framework
{
    public class FrameworkDelegates
    {
        public delegate void DataDownloaded(Object sender, DataDownloadedEventArgs eventArgs);

        public delegate void DataArchived(Object sender, EntityArchivedEventArgs eventArgs);
    }
}
