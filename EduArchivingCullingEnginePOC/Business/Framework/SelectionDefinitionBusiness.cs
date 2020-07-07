using System.Collections.Generic;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;

namespace Business.Framework
{
    public class SelectionDefinitionBusiness : ISelectionDefinitionService
    {
        private readonly ISelectionExecutionHandler _selectionExecutionHandler;
        private readonly ISelectionDefinitionDataAccess _selectionDefinitionDataAccess;

        public SelectionDefinitionBusiness(ISelectionDefinitionDataAccess selectionDefinitionDataAccess, 
            ISelectionExecutionHandler selectionExecutionHandler)
        {
            _selectionExecutionHandler = selectionExecutionHandler;
            _selectionDefinitionDataAccess = selectionDefinitionDataAccess;
        }

        public IList<SelectionDefinition> GetAll()
        {
            return _selectionDefinitionDataAccess.GetSelectionDefinitions();
        }

        public SelectionDefinition Get(int id)
        {
            return _selectionDefinitionDataAccess.Get(id);
        }

        public async void Run(int id)
        {
            var selection = _selectionDefinitionDataAccess.Get(id);
            await _selectionExecutionHandler.Run(selection);
        }

    }
}
