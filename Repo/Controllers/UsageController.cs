using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/Usage")]
    public class UsageController : Controller
    {
        private readonly IUsageRepository _usage;

        public UsageController(IUsageRepository usage)
        {
            _usage = usage;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usage.Get().ToList());
        }

        // GET api/<controller>/5
        [HttpGet("GetById/{personId}")]
        public IActionResult GetById(int personId)
        {
            return Ok(_usage.GetById(personId));
        }

        // GET api/values/5
        [HttpGet("AllByDevice/{deviceId}")]
        public IActionResult AllByDevice(int deviceId)
        {
            return Ok(_usage.AllByDevice(deviceId));
        }

        // GET api/values/5
        [HttpGet("AllByPerson/{personId}")]
        public IActionResult AllByPerson(int personId)
        {
            return Ok(_usage.AllByPerson(personId));
        }

        // GET api/values/5
        [HttpGet("TimeUsedByPerson/{personId}")]
        public IActionResult TimeUsedByPerson(int personId)
        {
            return Ok(_usage.TimeUsedByPerson(personId));
        }
    }
}
