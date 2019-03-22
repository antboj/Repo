using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repo.Classes.Expressions;
using Repo.Dto.UsageDto;
using Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/Usage")]
    public class UsageController : Controller
    {
        private readonly IUsageRepository _repository;
        private readonly IMapper _mapper;

        public UsageController(IUsageRepository usage, IMapper mapper)
        {
            _repository = usage;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var data = _repository.Get();

            var otp = _mapper.Map<IEnumerable<UsageDtoGet>>(data);

            return Ok(otp);
        }

        // GET api/values/5
        [HttpGet("AllByDevice/{deviceId}")]
        public IActionResult AllByDevice(int deviceId)
        {
            var data = _repository.AllByDevice(deviceId);

            var otp = _mapper.Map<IEnumerable<UsageDtoGet>>(data);

            return Ok(otp);
        }

        // GET api/values/5
        [HttpGet("AllByPerson/{personId}")]
        public IActionResult AllByPerson(int personId)
        {
            var data = _repository.AllByPerson(personId);


            var otp = _mapper.Map<IEnumerable<UsageAllByPersonDtoGet>>(data);



            return Ok(otp);
        }

        // GET api/values/5
        [HttpGet("TimeUsedByPerson/{personId}")]
        public IActionResult TimeUsedByPerson(int personId)
        {
            var data = _repository.TimeUsedByPerson(personId);

            var otp = _mapper.Map<IEnumerable<TimeUsedByPersonDtoGet>>(data);

            return Ok(otp);
        }

        // GET api/values/5
        [HttpPost("GetQueryInfo")]
        public IActionResult GetQueryInfo([FromBody] QueryInfo input)
        {
            var data = _repository.QueryInfo(input);
            var otp = _mapper.Map<IEnumerable<UsageGetQueryInfoDtoGet>>(data);
            return Ok(otp);
        }
    }
}
