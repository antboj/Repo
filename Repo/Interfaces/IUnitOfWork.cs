using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Interfaces
{
    public interface IUnitOfWork
    {
        //IOffice Office { get; }
        //IPerson Person { get; }
        void Save();
    }
}
