using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework.Interfaces
{
    /// <summary>
    /// An interface which every school domain is supposed to implement. Implementation for the school domain will be called by the Framework.
    /// </summary>
    public interface IArchive
    {
        Task<bool> GetData(EduEntity entityToArchive);

        //TODO: In real framework, data files can be read from database instead of handling in memory.
        Task<bool> CreateArchiveFiles(EduEntity entityToArchive, List<string> dataDownloadedInFiles);
    }
}
