using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Dto.UsageDto;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class UsageRepository : Repository<Usage, int>, IUsageRepository
    {
        private readonly RepoContext _context;

        public UsageRepository(RepoContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable AllByDevice(int id)
        {
            return _context.Usages.Where(x => x.DeviceId == id).Include(p => p.Person).Include(d => d.Device);
        }

        public IQueryable AllByPerson(int id)
        {
            return _context.Usages.Where(d => d.PersonId == id).Include(p => p.Person).Include(d => d.Device).GroupBy(d => d.Device.Name).Select(x =>
                 new { Device = x.Key, Usages = x.Select(y => new { UsedFrom = y.UsedFrom, UsedTo = y.UsedTo }).OrderBy(d => d.UsedFrom) });
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

        public bool IsCurrentlyUsed(int id)
        {
            var isCurrentlyUsed = _context.Usages.Any(x => x.DeviceId == id && x.UsedTo == null);

            return isCurrentlyUsed;
        }

        public void EndUsing(int id)
        {
            var usageRecord = _context.Usages.FirstOrDefault(u => u.DeviceId == id && u.UsedTo == null);

            if (usageRecord != null)
            {
                usageRecord.UsedTo = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void AddUsage(int dId, int pId)
        {
            var usage = new Usage
            {
                PersonId = pId,
                DeviceId = dId,
                UsedFrom = DateTime.Now
            };

            _context.Usages.Add(usage);
            _context.SaveChanges();
        }

        public override IEnumerable<Usage> Get()
        {
            return _context.Usages.Include(p => p.Person).Include(d => d.Device);
        }

        public override Usage GetById(int id)
        {
            throw new InvalidOperationException("Za ovaj entitet nije podrzana metoda Update");
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
