using System.Collections.Generic;
using Abstractions.Public.KAA;
using Abstractions.Public.KAA.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataProviderApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KAAController : ControllerBase
    {
        public KAAController()
        { 
        }

        [HttpPost("youths")]
        public IEnumerable<YouthBase> PostGetYouths([FromBody] GetKAARequest requestedYouth)
        {
            return FakeData.GetYouths();
        }

        [HttpPost("measures")]
        public IEnumerable<Measures> PostGetMeasures([FromBody] GetKAARequest requestedMeasures)
        {
            return FakeData.GetMeasures();
        }

        [HttpPost("reminders")]
        public IEnumerable<Reminders> PostGetReminders([FromBody] GetKAARequest requestedReminders)
        {
            return FakeData.GetReminders();
        }
    }
}
