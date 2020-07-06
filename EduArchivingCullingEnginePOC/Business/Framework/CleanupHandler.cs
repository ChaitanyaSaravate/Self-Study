using System.Collections.Generic;

namespace Business.Framework
{
    //TODO: Implement it in case of failed operation case too.
    public class CleanupHandler
    {
        private readonly InputOutputFilesManager _filesManager;

        public CleanupHandler(InputOutputFilesManager filesManager)
        {
            _filesManager = filesManager;
        }

        public void CleanupTemporaryData(List<string> dataFilesToDelete)
        {
            foreach (var file in dataFilesToDelete)
            {
                _filesManager.DeleteInputDataFile(file);
            }
        }
    }
}
