using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Classes.Attributes;
using Repo.Interfaces;

namespace Repo.Classes.Repository
{
    [Service(ServiceEnumAttributeValues.Transient)]
    public class Andrej2<TA, TB> : IRepository<TA, TB> where TA : class where TB : class
    {
        public void Add(TA entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TA> Get()
        {
            throw new NotImplementedException();
        }

        public TA GetById(TB id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TB id)
        {
            throw new NotImplementedException();
        }

        public void Update(TA entity)
        {
            throw new NotImplementedException();
        }
    }
}
