using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using Repo.Classes.Attributes;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    [Service]
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RepoContext _context;
        private IDbContextTransaction transaction;

        public UnitOfWork(RepoContext context)
        {
            _context = context;
        }
        public void Start()
        {
            this.transaction = _context.Database.BeginTransaction();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Dispose()
        {
            transaction?.Dispose();
        }

    }
}
