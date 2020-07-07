using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;

namespace Abstractions.Internal.Framework
{
    //NOTE: Changing file naming or folder structure created by this class will cause breaking changes. So, do it carefully.
    public class InputOutputFilesManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public InputOutputFilesManager(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public string CreateInputDataFile(string entityName, string dataToSaveInFile)
        {
            var filePath = CreateInputDataFilesTempFolder() + @"\" + $"{entityName}.json";
            var fileStreamWriter = File.CreateText(filePath);
            fileStreamWriter.WriteLine(dataToSaveInFile);
            fileStreamWriter.Dispose();

            return filePath;

        }

        public void DeleteInputDataFile(string fileName)
        {
            File.Delete(fileName);
        }

        public DirectoryInfo CreateInputDataFilesTempFolder()
        {
            return Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + @"\DemoOutput\Temp");
        }

        public List<string> GetDataFiles()
        {
            CreateInputDataFilesTempFolder();
            return Directory.GetFiles(_hostingEnvironment.ContentRootPath + @"\DemoOutput\Temp").ToList();
        }

        public void CreateArchiveFile<T>(string entityName, T entityToArchive)
        {
            var filePath = CreateOutputArchiveFilesFolder() + @"\" + $"{entityName}_{Guid.NewGuid()}.xml";
            var fileStreamWriter = File.CreateText(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(fileStreamWriter, entityToArchive);
            fileStreamWriter.Dispose();
        }

        public DirectoryInfo CreateOutputArchiveFilesFolder()
        {
            return Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + @"\DemoOutput\OutputArchiveFiles");
        }

        public string CreateStatusFile(int selectionId, ArchiveStatuses status)
        {
            // Status is kept in the file name itself for now.
            var filePath = CreateStatusFolder() + @"\" + $"{selectionId}_{status.ToString()}.txt";
            File.Create(filePath);

            //var fileStreamWriter = File.CreateText(filePath);
            //fileStreamWriter.WriteLine(status.ToString());
            //fileStreamWriter.Dispose();

            return filePath;
        }

        public string GetStatusFromFile(string filePath)
        {
            // Expected status file name in the format: <SelectionId>_<ArchiveStatuses>.txt
            var fullPathSplit = filePath.Split('\\');
            var fileNameWithExtension = fullPathSplit[fullPathSplit.Length - 1];
            var selectionId = fileNameWithExtension.Substring(0, 1);
            return fileNameWithExtension.Split('.')[0].Split('_')[1];

            //using (var reader = File.OpenText(CreateStatusFolder() + @"\" + $"{selectionId}.json"))
            //{
            //    return reader.ReadLine();
            //}
        }

        public string GetSelectionIdFromStatusFile(string filePath)
        {
            // Expected status file name in the format: <SelectionId>_<ArchiveStatuses>.txt
            var fullPathSplit = filePath.Split('\\');
            var fileNameWithExtension = fullPathSplit[fullPathSplit.Length - 1];
            return fileNameWithExtension.Substring(0, 1);
        }

        public void UpdateStatusFile(int selectionId, ArchiveStatuses status)
        {
            CreateStatusFile(selectionId, status);
        }

        public DirectoryInfo CreateStatusFolder()
        {
            return Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + @"\DemoOutput\Status");
        }

        public List<string> GetStatusFiles()
        {
            CreateStatusFolder();
            return Directory.GetFiles(_hostingEnvironment.ContentRootPath + @"\DemoOutput\Status").ToList();
        }


    }
}
