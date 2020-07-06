//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Logging.Abstractions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Serilog;

//namespace LoggingNetCore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentsController : ControllerBase
//    {
//        private readonly IAuditLogger<SchoolsController> _auditLogger;
//        private readonly Serilog.ILogger _seriLogger = Log.ForContext<SchoolsController>();

//        public StudentsController(IAuditLogger<SchoolsController> auditLogger)
//        {
//            _auditLogger = auditLogger;
//        }

//        // GET: api/Students
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            _seriLogger.Verbose("Querying the database.");
//            var schools = InMemoryStorage.Schools;
//            _seriLogger.Verbose("Database call successful");

//            _auditLogger.LogBusinessEvent(ApplicationLogEvents.AuditEventTypes.SchoolsLookUp, "Read details of the {SchoolCount} schools.", schools.Count());
//            return schools;
//        }

//        // GET: api/Students/5
//        [HttpGet("{id}", Name = "Get")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST: api/Students
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT: api/Students/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE: api/ApiWithActions/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
