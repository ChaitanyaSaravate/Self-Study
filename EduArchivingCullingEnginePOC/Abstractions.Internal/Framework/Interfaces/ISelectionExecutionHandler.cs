using System.Threading.Tasks;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework.Interfaces
{
    // (NOTE: In real Framework, the methods and parameter types would obviously vary!)
    public interface ISelectionExecutionHandler
    {
        /// <summary>
        /// Execute/run the given selection.
        /// </summary>
        /// <param name="selection"></param>
        /// <returns></returns>
        Task Run(SelectionDefinition selection);
    }
}
