using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repo.Classes;
using Repo.Interfaces;
using Repo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/Device")]
    public class DeviceController : BaseController<Device>
    {
        private readonly IDevice _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeviceController(IDevice repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        /*
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_device.Get());
        }

        // GET api/<controller>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var foundDevice = _device.GetById(id);
            return Ok(foundDevice);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Device entity)
        {
            _device.Add(entity);
            _unitOfWork.Save();
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Device entity)
        {
            var found = _device.GetById(id);
            found.Name = entity.Name;
            _unitOfWork.Save();
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("UseDevice/{personId}/{deviceId}")]
        public IActionResult UseDevice(int personId, int deviceId)
        {
            _device.UseDevice(personId, deviceId);
            _unitOfWork.Save();
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("ChangeDeviceUser/{personId}/{deviceId}")]
        public IActionResult ChangeDeviceUser(int personId, int deviceId)
        {
            _device.ChangeDeviceUser(personId, deviceId);
            _unitOfWork.Save();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _device.Remove(id);
            _unitOfWork.Save();
            return Ok();
        }*/
    }
}
