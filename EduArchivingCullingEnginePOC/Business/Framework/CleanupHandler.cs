using System.Collections.Generic;
using Abstractions.Internal.Framework;

namespace Business.Framework
{
    //TODO: Implement it in case of failed operation case too.
    public class CleanupHandler
    {
        private readonly InputOutputFilesManager _filesManager;


        public CleanupHandler(InputOutputFilesManager filesManager)
        {
            _filesManager = filesManager;
            ArchiveFileCreationHandler.ArchiveFilesCreated += ArchiveFileCreationHandler_ArchiveFilesCreated;
        }

        private void ArchiveFileCreationHandler_ArchiveFilesCreated(object sender, Abstractions.Internal.Framework.ArchivingEventArgs eventArgs)
        {
            foreach (var entity in eventArgs.EntityDataFilesMapper.Keys)
            {
                CleanupTemporaryData(eventArgs.EntityDataFilesMapper[entity]);
            }
        }

        public void CleanupTemporaryData(List<string> dataFilesToDelete)
        {
            if (dataFilesToDelete != null && dataFilesToDelete.Count > 0)
            {
                foreach (var file in dataFilesToDelete)
                {
                    _filesManager.DeleteInputDataFile(file);
                }
            }
        }
    }
}
