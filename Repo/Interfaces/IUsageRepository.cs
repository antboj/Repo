using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Classes.Expressions;
using Repo.Models;

namespace Repo.Interfaces
{
    public interface IUsageRepository : IRepository<Usage, int>
    {
        IQueryable<Usage> AllByDevice(int id);
        IQueryable<Usage> AllByPerson(int id);
        IQueryable<Usage> TimeUsedByPerson(int id);
        IQueryable<Usage> QueryInfo(QueryInfo input);
        bool IsCurrentlyUsed(int id);
        void AddUsage(int dId, int pId);
        void EndUsing(int id);
    }
}
