using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class OfficeRepository : Repository<Office>, IOffice
    {
        private readonly RepoContext context;

        public OfficeRepository(RepoContext context) : base(context)
        {
            this.context = context;
        }


        public IQueryable GetByOffice(string name)
        {
            return context.Offices.Where(o => o.Description == name).GroupBy(g => g.Description)
                .Select(y => new {Office = y.Key, Persons = y.Select(c => c.Persons)});
        }
    }
}
