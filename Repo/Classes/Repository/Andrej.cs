using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Classes.Attributes;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes.Repository
{
    [Service(ServiceEnumAttributeValues.Singleton)]
    public class Andrej : IRepository<Device, decimal>
    {
        public Device GetById(decimal id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Device> Get()
        {
            throw new NotImplementedException();
        }

        public void Add(Device entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Device entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(decimal id)
        {
            throw new NotImplementedException();
        }
    }
}
