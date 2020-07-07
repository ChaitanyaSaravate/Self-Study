//using System;
//using System.IO;
//using System.Threading.Tasks;
//using System.Xml.Serialization;
//using Abstractions.Internal.Framework;
//using Microsoft.AspNetCore.Hosting;

//namespace Business.Framework
//{
//    public class InputOutputFilesManager
//    {
//        private readonly IHostingEnvironment _hostingEnvironment;

//        public InputOutputFilesManager(IHostingEnvironment hostingEnvironment)
//        {
//            _hostingEnvironment = hostingEnvironment;
//        }
//        public string CreateInputDataFile(string entityName, string dataToSaveInFile)
//        {
//            var filePath = CreateInputDataFilesTempFolder() + @"\" + $"{entityName}.json";
//            var fileStreamWriter = File.CreateText(filePath);
//            fileStreamWriter.WriteLine(dataToSaveInFile);
//            fileStreamWriter.Dispose();

//            return filePath;
//        }

//        public void DeleteInputDataFile(string fileName)
//        {
//            File.Delete(fileName);
//        }

//        public DirectoryInfo CreateInputDataFilesTempFolder()
//        {
//            return Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + @"\Temp");
//        }

//        public void CreateArchiveFile<T>(string entityName, T entityToArchive)
//        {
//            var filePath = CreateOutputArchiveFilesFolder() + @"\" + $"{entityName}_{Guid.NewGuid()}.xml";
//            var fileStreamWriter = File.CreateText(filePath);
//            XmlSerializer serializer = new XmlSerializer(typeof(T));
//            serializer.Serialize(fileStreamWriter, entityToArchive);
//            fileStreamWriter.Dispose();
//        }

//        public DirectoryInfo CreateOutputArchiveFilesFolder()
//        {
//            return Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + @"\OutputArchiveFiles");
//        }

//        public string CreateStatusFile(int selectionId, ArchiveStatuses status)
//        {
//            var filePath = CreateStatusFolder() + @"\" + $"{selectionId}.json";
//            var fileStreamWriter = File.CreateText(filePath);
//            fileStreamWriter.WriteLine(status.ToString());
//            fileStreamWriter.Dispose();

//            return filePath;
//        }

//        public string ReadStatusFile(int selectionId)
//        {
//            using (var reader = File.OpenText(CreateStatusFolder() + @"\" + $"{selectionId}.json"))
//            {
//                return reader.ReadLine();
//            }
//        }

//        public void UpdateStatusFile(int selectionId, ArchiveStatuses status)
//        {
//            File.Delete(CreateStatusFolder() + @"\" + $"{selectionId}.json");
//            CreateStatusFile(selectionId, status);
//        }

//        public DirectoryInfo CreateStatusFolder()
//        {
//            return Directory.CreateDirectory(_hostingEnvironment.ContentRootPath + @"\Status");
//        }
//    }
//}
