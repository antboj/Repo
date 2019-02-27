using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
        void Start();
        void Commit();
        void Dispose();
    }
}
