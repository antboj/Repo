using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class DeviceRepository : Repository<Device>, IDevice
    {
        private readonly RepoContext context;

        public DeviceRepository(RepoContext context) : base(context)
        {
            this.context = context;
        }


        public void UseDevice(int pId, int dId)
        {
            var foundDevice = context.Devices.Find(dId);
            var isCurrentlyUsed = context.Usages.Any(x => x.DeviceId == dId && x.UsedTo == null);

            if (foundDevice != null && !isCurrentlyUsed)
            {
                foundDevice.PersonId = pId;
                context.SaveChanges();

                var newUsageRecord = new Usage
                {
                    PersonId = pId,
                    DeviceId = dId,
                    UsedFrom = DateTime.Now
                };

                context.Usages.Add(newUsageRecord);
                context.SaveChanges();
            }
        }

        public void ChangeDeviceUser(int pId, int dId)
        {
            var foundDevice = context.Devices.Find(dId);

            if (foundDevice == null || foundDevice.PersonId == pId)
            {
                return;
            }

            foundDevice.PersonId = pId;
            context.SaveChanges();

            var usageRecord = context.Usages.FirstOrDefault(u => u.DeviceId == dId && u.UsedTo == null);

            if (usageRecord != null)
            {
                usageRecord.UsedTo = DateTime.Now;
                context.SaveChanges();
            }

            var newUsageRecord = new Usage
            {
                PersonId = pId,
                DeviceId = dId,
                UsedFrom = DateTime.Now
            };

            context.Usages.Add(newUsageRecord);
            context.SaveChanges();
        }
    }
}
