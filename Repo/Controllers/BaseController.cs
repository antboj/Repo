using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repo.Classes;
using Repo.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<TEntity, TDtoGet, TDtoPost, TDtoPut> : Controller where TEntity : class where TDtoGet : class where TDtoPost : class where TDtoPut : class
    {
        private IRepository<TEntity> _repository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BaseController(IRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var data = _repository.Get();

            if (data != null)
            {
                var otp = _mapper.Map<IEnumerable<TDtoGet>>(data);

                return Ok(otp);
            }

            return NotFound();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _repository.GetById(id);

            if (data != null)
            {
                var otp = _mapper.Map<TDtoGet>(data);

                return Ok(otp);
            }

            return NotFound();
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(TDtoPost input)
        {
            var otp = _mapper.Map<TEntity>(input);
            _repository.Add(otp);
            return Ok("Success");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, TDtoPut input)
        {
            var entity = _repository.GetById(id);
            _mapper.Map(input, entity);
            return Ok("Success");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Remove(id);
            return Ok("Success");
        }
    }
}
