using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Interfaces;
using Business.Framework;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InternalApis.BackgroundServices
{
    /// <summary>
    /// Background service which creates archive files if the data is downloaded.
    /// NOTE NOTE NOTE: Background Services are helpful. But note that they are tricky in load balanced environment since we may have data files on the server from where original request was served; not necessarily on which the background service is running.
    /// </summary>
    public class ArchiveFileCreatorService : BackgroundService
    {
        private readonly InputOutputFilesManager _inputOutputFilesManager;
        private readonly ILogger<ArchiveFileCreatorService> _logger;
        private readonly ArchiveHandlerFactory _archiveHandlerFactory;
        private readonly ISelectionDefinitionService _selectionDefinitionService;

        public ArchiveFileCreatorService(InputOutputFilesManager inputOutputFilesManager, ILogger<ArchiveFileCreatorService> logger,
            ArchiveHandlerFactory archiveHandlerFactory, ISelectionDefinitionService selectionDefinitionService)
        {
            _inputOutputFilesManager = inputOutputFilesManager;
            _logger = logger;
            _archiveHandlerFactory = archiveHandlerFactory;
            _selectionDefinitionService = selectionDefinitionService;
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

                    if (status == ArchiveStatuses.DataDownloaded.ToString())
                    {
                        // This tells if the operation is already done or not.
                        if (!statusFiles.Exists(file =>
                            file.EndsWith($"{selectionId}_{ArchiveStatuses.ArchiveFilesCreated}.txt") ||
                            file.EndsWith($"{selectionId}_{ArchiveStatuses.Successful}.txt") ||
                            file.EndsWith($"{selectionId}_{ArchiveStatuses.Failed}.txt")))
                        {
                            var runningSelectionDefinition = _selectionDefinitionService.Get(int.Parse(selectionId));
                            var archiveHandler =
                                _archiveHandlerFactory.GetArchiveHandler(runningSelectionDefinition.SchoolDomain);

                            var dataFiles = _inputOutputFilesManager.GetDataFiles();

                            Dictionary<SupportedEduEntityTypes, List<string>> entityDataFilemapper =
                                new Dictionary<SupportedEduEntityTypes, List<string>>();

                            foreach (var df in dataFiles)
                            {
                                // Expected data file name in the format: <SupportedEduEntityTypes>.json
                                var splittedPath = df.Split('\\');
                                var fileNactualFileNameSplitted = splittedPath[splittedPath.Length - 1].Split('.');
                                var actualFileName = fileNactualFileNameSplitted[0];

                                var entityTypeToArchive =
                                    (SupportedEduEntityTypes) Enum.Parse(typeof(SupportedEduEntityTypes),
                                        actualFileName);

                                entityDataFilemapper.Add(entityTypeToArchive, new List<string> {df});
                            }

                            await archiveHandler.CreateArchiveFilesAsync(entityDataFilemapper);
                            _inputOutputFilesManager.UpdateStatusFile(runningSelectionDefinition.Id,
                                ArchiveStatuses.ArchiveFilesCreated);
                        }
                    }
                }

                await Task.Delay(10000);
            }
        }
    }
}
