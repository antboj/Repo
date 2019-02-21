using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class UsageRepository : Repository<Usage>, IUsageRepository
    {
        private readonly RepoContext _context;

        public UsageRepository(RepoContext context) : base(context)
        {
        }

        public IQueryable AllByDevice(int id)
        {
            return _context.Usages.Where(d => d.DeviceId == id).Select(x => new
            {
                Name = x.Person.FirstName + " " + x.Person.LastName,
                Device = x.Device.Name,
                UsedFrom = x.UsedFrom,
                UsedTo = x.UsedTo
            });
        }

        public IQueryable AllByPerson(int id)
        {
            return _context.Usages.Where(d => d.PersonId == id).Select(x => new
            {
                Name = x.Person.FirstName + " " + x.Person.LastName,
                Device = x.Device.Name,
                UsedFrom = x.UsedFrom,
                UsedTo = x.UsedTo
            });
        }

        public IQueryable TimeUsedByPerson(int id)
        {
            return _context.Usages.Where(p => p.PersonId == id).GroupBy(x => x.Device.Name)
                .Select(y => new
                {
                    Device = y.Key,
                    TimeUsed = new TimeSpan(y.Sum(u => (u.UsedTo.Value != null ? u.UsedTo.Value.Ticks : DateTime.Now.Ticks) - u.UsedFrom.Ticks)).ToString(@"dd\.hh\:mm\:ss")
                });
        }

        public override void Update(Usage entity)
        {
            throw new InvalidOperationException("Za ovaj entitet nije podrzana metoda Update");
        }

        public override void Add(Usage entity)
        {
            throw new InvalidOperationException("Za ovaj entitet nije podrzana metoda Add");
        }

        public override void Remove(int id)
        {
            throw new InvalidOperationException("Za ovaj entitet nije podrzana metoda Remove");
        }
    }
}
