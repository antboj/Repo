using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RepoContext _context;
        private TransactionScope transaction;

        public UnitOfWork(RepoContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Start()
        {
            this.transaction = new TransactionScope();
        }

        public void Commit()
        {
            this.transaction.Complete();
        }

        public void Dispose()
        {
            transaction?.Dispose();
        }

    }
}
