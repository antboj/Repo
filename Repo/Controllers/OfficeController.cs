using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repo.Dto.OfficeDto;
using Repo.Interfaces;
using Repo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/Office")]
    public class OfficeController : BaseController<Office, OfficeDtoGet, OfficeDtoPost, OfficeDtoPut>
    {
        private readonly IOffice _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfficeController(IOffice repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET api/values/5
        [HttpGet("GetByOffice/{officeName}")]
        public IActionResult GetByOffice(string officeName)
        {
            var data = _repository.GetByOffice(officeName);

            var otp = _mapper.Map<IEnumerable<GetByOfficeDto>>(data);
                return Ok(otp);
        }
    }
}
