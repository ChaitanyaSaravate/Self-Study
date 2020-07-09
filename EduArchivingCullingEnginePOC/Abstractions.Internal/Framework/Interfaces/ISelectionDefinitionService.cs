using System.Collections.Generic;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework.Interfaces
{
    // (NOTE: In real Framework, the methods and parameter types would obviously vary!)
    public interface ISelectionDefinitionService
    {
        IList<SelectionDefinition> GetAll();

        SelectionDefinition Get(int id);

        /// <summary>
        /// Execute/run the given selection.
        /// </summary>
        /// <param name="id"><see cref="SelectionDefinition.Id"/></param>
        void Run(int id);
    }
}
