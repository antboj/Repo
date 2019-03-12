using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repo.Dto.PersonDto;
using Repo.Interfaces;
using Repo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/Person")]
    public class PersonController : BaseController<Person, PersonDtoGet, PersonDtoPost, PersonDtoPut, int>
    {
        private readonly IPersonRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PersonController(IPersonRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
