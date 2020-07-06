using System;
using System.Collections.Generic;
using System.Text;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework.Interfaces
{
    public interface ISelectionDefinitionService
    {
        IList<SelectionDefinition> GetAll();

        SelectionDefinition Get(int id);

        void Run(int id);
    }
}
