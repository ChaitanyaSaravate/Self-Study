using System;
using System.Collections.Generic;
using System.Text;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;

namespace DAL
{
    public class SelectionDefinitionDataAccess : ISelectionDefinitionDataAccess
    {
        public IList<SelectionDefinition> GetSelectionDefinitions()
        {
            return MasterData.GetSelectionDefinitions();
        }

        public SelectionDefinition Get(int id)
        {
            return MasterData.GetSelectionDefinition(id);
        }

        public EduEntityGroup GetEduEntityGroup(SupportedEduEntityGroups eduEntityGroup)
        {
            return MasterData.GetEduEntityGroup(eduEntityGroup);
        }
    }
}
