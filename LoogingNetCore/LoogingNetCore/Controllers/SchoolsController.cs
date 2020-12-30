using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Logging.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LoggingNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IAuditLogger<SchoolsController> _auditLogger;
        private readonly Serilog.ILogger _seriLogger = Log.ForContext<SchoolsController>();

        public SchoolsController(IAuditLogger<SchoolsController> auditLogger, IServiceProvider serviceProvider)
        {
            _auditLogger = auditLogger;

            _auditLogger = serviceProvider.GetRequiredService<IAuditLogger<SchoolsController>>();
        }

        // GET: api/School
        [HttpGet]
        public IEnumerable<School> Get()
        {
            _seriLogger.Verbose("Querying the database.");
            var schools = InMemoryStorage.Schools;
            _seriLogger.Verbose("Database call successful");
            
            _auditLogger.LogBusinessEvent(ApplicationLogEvents.AuditEventTypes.SchoolsLookUp, "Read details of the {SchoolCount} schools.", schools.Count());
            HttpClient client = new HttpClient();
            var result = client.GetAsync("https://localhost:44327/weatherforecast");
            return schools;
        }

        // GET: api/School/5
        [HttpGet("{id}", Name = "Get")]
        public School Get(int id)
        {
            var school = InMemoryStorage.Schools.First(s => s.Id == id);
            _auditLogger.LogBusinessEvent(ApplicationLogEvents.AuditEventTypes.SchoolsLookUp, "Read information about the school having ID = {SchoolID}.", school.Id);
            return school;
        }

        // GET: api/School/ByName/NMV School
        [HttpGet("ByName/{name}", Name = "GetByName")]
        public School Get(string name)
        {
            var school = InMemoryStorage.Schools.First(s => s.Name.ToLowerInvariant().StartsWith(name.ToLowerInvariant(), StringComparison.InvariantCulture));
            _auditLogger.LogBusinessEvent(ApplicationLogEvents.AuditEventTypes.SchoolsLookUp, "Read information about the school named {SchoolName}.", school.Name);
            return school;
        }

        [HttpGet("{schoolId:int}/students", Name = "Students")]
        public IEnumerable<Student> GetStudents(int schoolId)
        {
            var school = InMemoryStorage.Schools.First(s => s.Id == schoolId);
            var students = school?.Students; ;
            _auditLogger.LogBusinessEvent(ApplicationLogEvents.AuditEventTypes.StudentsLookUp, "Read information about the Students in the School having ID = {SchoolID}.", schoolId);
            return students;
        }

        // POST: api/schools
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post([FromBody]School school)
        {
            if (school != null && school.Id > 0 && !string.IsNullOrWhiteSpace(school.Name))
            {
                _seriLogger.Verbose("Attempting to create school with {SchoolID} = ", school.Id);
                if (InMemoryStorage.Schools.FirstOrDefault(s => s.Id == school.Id) == null)
                {
                    InMemoryStorage.Schools.Add(school);
                    _auditLogger.LogBusinessEvent(ApplicationLogEvents.AuditEventTypes.SchoolAdded, "Created the new School with {SchoolID}.", school.Id);
                    return Created(new Uri(this.Request.GetEncodedUrl() + "/" + school.Id), null);
                }
                else
                {
                    return Conflict($"The school with {school.Id} already exists.");
                }
            }

            _seriLogger.Verbose("Either School object or its mandatory fields are null.");
            return BadRequest("Either School object or its mandatory fields are null.");
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] School school)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
