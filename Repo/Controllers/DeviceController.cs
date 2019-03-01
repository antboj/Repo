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
    public class DeviceController : BaseController<Device, DeviceDtoGet, DeviceDtoPost, DeviceDtoPut>
    {
        private readonly IDevice _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUsageRepository _usage;

        public DeviceController(IDevice repository, IUnitOfWork unitOfWork, IMapper mapper, IUsageRepository usage) : base(repository, unitOfWork, mapper)
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
            
                    //throw new CustomException("Uredjaj se koristi");
                    _repository.UseDevice(personId, deviceId);
                    _usage.AddUsage(deviceId, personId);
                    //_unitOfWork.Save();
                    //_unitOfWork.Commit();
                    return Ok("Success");
        }

        // PUT api/<controller>/5
        [HttpPut("ChangeDeviceUser/{personId}/{deviceId}")]
        public IActionResult ChangeDeviceUser(int personId, int deviceId)
        {
            //_unitOfWork.Start();
            //if (personId != 0 || deviceId != 0)
            //{
                //try
                //{
                _repository.ChangeDeviceUser(personId, deviceId);
                    _usage.EndUsing(deviceId);
                    _usage.AddUsage(deviceId, personId);
                    //_unitOfWork.Save();
                    //_unitOfWork.Commit();
                    return Ok("Success");
                //}
                //catch (Exception)
                //{
                //    return BadRequest();
                //}
            //}
            //_unitOfWork.Dispose();
            //return NotFound();

            //_repository.ChangeDeviceUser(personId, deviceId);
            //_unitOfWork.Save();
            //return Ok();
        }
    }
}
