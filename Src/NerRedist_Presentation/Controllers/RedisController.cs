using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NerRedist_Presentation.Extentions;
using NerRedist_Presentation.Models;

namespace NerRedist_Presentation.Controllers
{
    public class RedisController : Controller
    {
        private readonly IDistributedCache cache;
        public RedisController(IDistributedCache cache)
        {
            this.cache = cache;
        }
        [HttpPost("SetRecord")]
        public async Task<ActionResult> SetRecordAsync()
        {
            var sdgdfg = DateTimeOffset.FromUnixTimeSeconds(60);

            await cache.SetRecordAsync<Person>(Guid.NewGuid().ToString(), new Person
            {
                Age = 22,
                Family = "Davoodi",
                FatherName = "AlHo",
                Name = "MSoheil",
            }, TimeSpan.FromSeconds(120), TimeSpan.FromSeconds(120));
            return Ok();
        }
    }
}
