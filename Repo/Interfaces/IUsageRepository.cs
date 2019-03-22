using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Classes.Expressions;
using Repo.Dto.UsageDto;
using Repo.Models;

namespace Repo.Interfaces
{
    public interface IUsageRepository : IRepository<Usage, int>
    {
        IQueryable<Usage> AllByDevice(int id);
        // izmijeni
        IQueryable<UsageAllByPersonDtoGet> AllByPerson(int id);
        IQueryable<TimeUsedByPersonDtoGet> TimeUsedByPerson(int id);
        // ove dvije
        IQueryable<Usage> QueryInfo(QueryInfo input);
        bool IsCurrentlyUsed(int id);
        void AddUsage(int dId, int pId);
        void EndUsing(int id);
    }
}
