﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class PersonRepository : Repository<Person>, IPerson
    {
        private readonly RepoContext _context;

        public PersonRepository(RepoContext context) : base(context)
        {
            _context = context;
        }

        public override void Add(Person entity)
        {
            _context.Persons.Add(entity);
            _context.SaveChanges();

            //var lastPerson = _context.Persons.Last();
            //var lastPersonOffice = lastPerson.OfficeId;
            //var officeName = _context.Offices.FirstOrDefault(o => o.Id == lastPersonOffice);
            //var personList = officeName.Persons;
            //personList.Add(lastPerson);
        }

        //public override void Update(Person entity)
        //{
        //}
    }
}
