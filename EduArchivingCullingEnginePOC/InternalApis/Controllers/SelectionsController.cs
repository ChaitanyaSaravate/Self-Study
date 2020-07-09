using System.Collections.Generic;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternalApis.Controllers
{
    [Route("[controller]")]
    [ApiController]
    // (NOTE: In real Framework, the controller actions would obviously vary!)
    // NOTE: Call API using some tool like Postman.
    public class SelectionsController : ControllerBase
    {
        private readonly ISelectionDefinitionService _selectionDefinitionService;

        public SelectionsController(ISelectionDefinitionService selectionDefinitionService)
        {
            _selectionDefinitionService = selectionDefinitionService;
        }

        [HttpGet]
        public IEnumerable<SelectionDefinition> Get()
        {
            return _selectionDefinitionService.GetAll();
        }

        [HttpGet("{id}")]
        public SelectionDefinition Get(int id)
        {
            return _selectionDefinitionService.Get(id);
        }

        [HttpPost("run/{id}")]
        public void Run(int id)
        {
            _selectionDefinitionService.Run(id);
        }
    }
}