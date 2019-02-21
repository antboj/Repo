using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Models;

namespace Repo.Interfaces
{
    public interface IUsageRepository : IRepository<Usage>
    {
        IQueryable AllByDevice(int id);
        IQueryable AllByPerson(int id);
        IQueryable TimeUsedByPerson(int id);
    }
}
