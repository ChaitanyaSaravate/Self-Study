using System.Threading.Tasks;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework.Interfaces
{
    //TODO: Turn it into some real interface with methods in it
    public interface ISelectionExecutionHandler
    {
        Task Run(SelectionDefinition selection);
    }
}
