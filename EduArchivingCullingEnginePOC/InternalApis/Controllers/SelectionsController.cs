using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Entities;
using Abstractions.Internal.Framework.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternalApis.Controllers
{
    [Route("[controller]")]
    [ApiController]
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