using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Models;

namespace Repo.Interfaces
{
    public interface IUsageRepository : IRepository<Usage, int>
    {
        IQueryable AllByDevice(int id);
        IQueryable AllByPerson(int id);
        IQueryable TimeUsedByPerson(int id);
        IQueryable Queryinfo(string op, string prop, string src, string ob);
        bool IsCurrentlyUsed(int id);
        void AddUsage(int dId, int pId);
        void EndUsing(int id);
    }
}
