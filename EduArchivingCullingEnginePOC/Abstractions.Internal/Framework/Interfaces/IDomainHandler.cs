using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework.Interfaces
{
    /// <summary>
    /// An interface which every school domain is supposed to implement.
    /// (NOTE: In real Framework, the methods and parameter types would obviously vary!)
    /// 
    /// Implementation for the school domain will be called by the Framework using a factory (//TODO: or try other better DI containers instead of factory?)
    /// </summary>
    public interface IDomainHandler
    {
        //TODO: Send selection definition id, execution id etc so that files can be saved per execution ID of the given selection.
        Task<List<string>> GetDataAsync(EduEntity entityToArchive);

        //TODO: In real framework, data files' list can be read from database instead of handling in memory. So only entityType parameter would be required.
        Task<bool> CreateArchiveFilesAsync(Dictionary<SupportedEduEntityTypes, List<string>> entityDataFileMapper);

        //TODO: This can be removed but kept here to make some code compile.
        Task<bool> CreateArchiveFilesAsync(SupportedEduEntityTypes eduEntityType, List<string> dataDownloadedInFiles);
    }
}
