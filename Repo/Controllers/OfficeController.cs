using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repo.Interfaces;
using Repo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/Office")]
    public class OfficeController : Controller
    {
        private readonly IOffice _office;
        private readonly IUnitOfWork _unitOfWork;

        public OfficeController(IOffice office, IUnitOfWork unitOfWork)
        {
            _office = office;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var allOffices = _office.Get();
            return Ok(allOffices);
        }

        // GET api/<controller>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var foundOffice = _office.GetById(id);
            return Ok(foundOffice);
        }

        // GET api/values/5
        [HttpGet("GetByOffice/{officeName}")]
        public IActionResult GetByOffice(string officeName)
        {
            return Ok(_office.GetByOffice(officeName));
           
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Office entity)
        {
            _office.Add(entity);
            _unitOfWork.Save();
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Office entity)
        {
            var found = _office.GetById(id);
            found.Description = entity.Description;


            _unitOfWork.Save();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _office.Remove(id);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
