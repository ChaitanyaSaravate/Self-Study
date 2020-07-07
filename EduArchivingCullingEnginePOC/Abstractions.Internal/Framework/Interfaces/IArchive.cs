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
        //TODO: Send selection definition id, run id etc so that files can be saved in separate folders.
        Task<List<string>> GetDataAsync(EduEntity entityToArchive);

        //TODO: In real framework, data files can be read from database instead of handling in memory.
        Task<bool> CreateArchiveFilesAsync(Dictionary<SupportedEduEntityTypes, List<string>> entityDataFileMapper);

        //TODO: This can be removed but kept here to make some code compile.
        Task<bool> CreateArchiveFilesAsync(SupportedEduEntityTypes eduEntityType, List<string> dataDownloadedInFiles);
    }
}
