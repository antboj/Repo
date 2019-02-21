﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repo.Interfaces;
using Repo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Repo.Controllers
{
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private readonly IPerson _person;
        private readonly IUnitOfWork _unitOfWork;
        
        
        public PersonController(IPerson person, IUnitOfWork unitOfWork)
        {
            _person = person;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_person.Get());
        }

        // GET api/<controller>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var foundPerson = _person.GetById(id);
            return Ok(foundPerson);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Person entity)
        {
            _person.Add(entity);
            _unitOfWork.Save();
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Person entity)
        {
            var found = _person.GetById(id);
            _person.Update(found);
            found.FirstName = entity.FirstName;
            found.LastName = entity.LastName;

            var key = found.OfficeId;
            found.OfficeId = entity.OfficeId != 0 ? entity.OfficeId : key;

            _unitOfWork.Save();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _person.Remove(id);
            _unitOfWork.Save();
            return Ok();
        }
    }
}