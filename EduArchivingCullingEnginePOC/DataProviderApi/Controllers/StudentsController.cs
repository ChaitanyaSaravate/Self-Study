using System.Collections.Generic;
using Abstractions.Public.CompulsorySchool;
using Abstractions.Public.CompulsorySchool.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataProviderApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IArchiveStudent _studentService;
        private readonly IArchiveGrades _gradesService;

        public StudentsController(IArchiveStudent studentService, IArchiveGrades gradesService)
        {
            _studentService = studentService;
            _gradesService = gradesService;
        }

        [HttpPost("studentinfo")]
        public IEnumerable<StudentInfo> Post([FromBody] GetStudentRequest getStudentRequest)
        {
            return _studentService.GetStudents(getStudentRequest);
        }

        [HttpPost("grades")]
        public IEnumerable<StudentGrades> Post([FromBody] GetGradesRequest getGradesRequest)
        {
            return _gradesService.GetGrades(getGradesRequest);
        }
    }
}
