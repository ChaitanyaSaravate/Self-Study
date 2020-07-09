using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Microsoft.Extensions.Hosting;

namespace InternalApis.BackgroundServices
{
    /// <summary>
    /// Background service which cleans up temp data files.
    /// NOTE NOTE NOTE: Background Services are helpful. But note that they are tricky in load balanced environment since we may have data files on the server from where original request was served; not necessarily on which the background service is running. 
    /// </summary>
    public class CleanupService : BackgroundService
    {
        private readonly InputOutputFilesManager _inputOutputFilesManager;

        public CleanupService(InputOutputFilesManager inputOutputFilesManager)
        {
            _inputOutputFilesManager = inputOutputFilesManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var statusFiles = _inputOutputFilesManager.GetStatusFiles();

                foreach (var filePath in statusFiles)
                {
                    var selectionId = _inputOutputFilesManager.GetSelectionIdFromStatusFile(filePath);
                    var status = _inputOutputFilesManager.GetStatusFromFile(filePath);

                    // This tells if the operation is already done or not.
                    if (!statusFiles.Exists(file =>
                        file.EndsWith($"{selectionId}_{ArchiveStatuses.Successful}.txt") ||
                        file.EndsWith($"{selectionId}_{ArchiveStatuses.Failed}.txt")))
                    {
                        if (status == ArchiveStatuses.ArchiveFilesCreated.ToString())
                        {
                            //TODO: It should actually return only those data files which belong to selection.
                            var dataFiles = _inputOutputFilesManager.GetDataFiles();
                            foreach (var df in dataFiles)
                            {
                                File.Delete(df);
                            }

                            _inputOutputFilesManager.UpdateStatusFile(int.Parse(selectionId),
                                ArchiveStatuses.Successful);
                        }
                    }
                }

                await Task.Delay(10000);
            }
        }
    }
}
