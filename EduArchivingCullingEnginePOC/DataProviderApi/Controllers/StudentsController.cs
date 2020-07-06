using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abstractions.Public.CompulsorySchool;
using Abstractions.Public.CompulsorySchool.Entities;
using Abstractions.Public.CompulsorySchool.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace DataProviderApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IArchiveStudent _studentService;
        private readonly IArchiveGrades _gradesService;
        private readonly IArchiveAbsences _absenceService;

        public StudentsController(IArchiveStudent studentService, IArchiveGrades gradesService, IArchiveAbsences absenceService)
        {
            _studentService = studentService;
            _gradesService = gradesService;
            _absenceService = absenceService;
        }

        [HttpPost("studentinfo")]
        public IEnumerable<StudentInfo> Post([FromBody] StudentArchiveDataRequest archiveDataRequest)
        {
            return _studentService.GetStudents(archiveDataRequest);
        }

        [HttpPost("grades")]
        public async Task<IEnumerable<StudentGrades>> Post([FromBody] GradesDataRequest getGradesRequest)
        {
            //await Task.Delay(10000);
            return _gradesService.GetGrades(getGradesRequest);
        }

        [HttpPost("absences")]
        public async Task<IEnumerable<StudentAbsences>> Post([FromBody] AbsenceDataRequest absenceDataRequest)
        {
            //await Task.Delay(10000);
            return _absenceService.GetAbsenceses(absenceDataRequest);
        }
    }
}
