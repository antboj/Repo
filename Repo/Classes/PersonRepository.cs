using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class PersonRepository : Repository<Person>, IPerson
    {
        private readonly RepoContext context;

        public PersonRepository(RepoContext context) : base(context)
        {
            this.context = context;
        }

        public override void Add(Person entity)
        {
            base.Add(entity);

            context.Persons.Add(entity);
            context.SaveChanges();

            var lastPerson = context.Persons.Last();
            var lastPersonOffice = lastPerson.OfficeId;
            var officeName = context.Offices.FirstOrDefault(o => o.Id == lastPersonOffice);
            var personList = officeName.Persons;
            personList.Add(lastPerson);
        }

        //public override void Update(Person entity)
        //{
           
        //}
    }
}
