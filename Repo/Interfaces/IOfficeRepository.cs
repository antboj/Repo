using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Models;

namespace Repo.Interfaces
{
    public interface IOfficeRepository : IRepository<Office, int>
    {
        IQueryable GetByOffice(string name, string prop);
        IQueryable GetOffice(string name);
        void Unos(Office input);
    }
}
