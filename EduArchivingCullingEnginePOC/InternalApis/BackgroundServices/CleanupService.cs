using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Microsoft.Extensions.Hosting;

namespace InternalApis.BackgroundServices
{
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
