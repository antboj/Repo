﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class DeviceRepository : Repository<Device>, IDevice
    {
        private readonly RepoContext _context;

        public DeviceRepository(RepoContext context) : base(context)
        {
            _context = context;
        }


        public void UseDevice(int pId, int dId)
        {
            var foundDevice = _context.Devices.Find(dId);
            //var isCurrentlyUsed = _context.Usages.Any(x => x.DeviceId == dId && x.UsedTo == null);

            //if (!isCurrentlyUsed)
            if (foundDevice == null)
            {
                throw new MyException("Uredjaj ne postoji");
            }

            if (foundDevice.PersonId == pId && foundDevice.Id == dId)
            {
                throw new MyException("Korisnik vec koristi trazeni uredjaj");
            }
            
                foundDevice.PersonId = pId;
                _context.SaveChanges();
        }

        public void ChangeDeviceUser(int pId, int dId)
        {
            var foundDevice = _context.Devices.Find(dId);

            if (foundDevice == null)
            {
                throw new MyException("Uredjaj ne postoji");
            }

            if (foundDevice.PersonId == pId && foundDevice.Id == dId)
            {
                throw new MyException("Korisnik vec koristi trazeni uredjaj");
            }

            foundDevice.PersonId = pId;
            _context.SaveChanges();
        }
    }
}
