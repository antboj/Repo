using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Models;

namespace Repo.Interfaces
{
    public interface IDevice : IRepository<Device>
    {
        void UseDevice(int pId, int dId);
        void ChangeDeviceUser(int pId, int dId);
    }
}
