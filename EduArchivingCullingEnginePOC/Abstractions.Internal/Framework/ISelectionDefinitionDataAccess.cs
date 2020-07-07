using System;
using System.Collections.Generic;
using System.Text;
using Abstractions.Internal.Framework.Entities;

namespace Abstractions.Internal.Framework
{
    public interface ISelectionDefinitionDataAccess
    {
        IList<SelectionDefinition> GetSelectionDefinitions();

        SelectionDefinition Get(int id);

        EduEntityGroup GetEduEntityGroup(SupportedEduEntityGroups eduEntityGroup);
    }
}
