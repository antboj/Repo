using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repo.Classes;
using Repo.Dto.DeviceDto;
using Repo.Interfaces;
using Repo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/Device")]
    public class DeviceController : BaseController<Device, DeviceDtoGet, DeviceDtoPost, DeviceDtoPut, int>
    {
        private readonly IDeviceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUsageRepository _usage;

        public DeviceController(IDeviceRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IUsageRepository usage) : base(repository, unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
            _usage = usage;

        }
        // PUT api/<controller>/5
        [HttpPut("UseDevice/{personId}/{deviceId}")]

        public IActionResult UseDevice(int personId, int deviceId)
        {
            //_unitOfWork.Start();
            var isCurrentlyUsed = _usage.IsCurrentlyUsed(deviceId);
                    _repository.UseDevice(personId, deviceId);
                    _usage.AddUsage(deviceId, personId);
                    return Ok("Success");
        }

        // PUT api/<controller>/5
        [HttpPut("ChangeDeviceUser/{personId}/{deviceId}")]
        public IActionResult ChangeDeviceUser(int personId, int deviceId)
        {
                _repository.ChangeDeviceUser(personId, deviceId);
                    _usage.EndUsing(deviceId);
                    _usage.AddUsage(deviceId, personId);
                    return Ok("Success");
        }
    }
}
