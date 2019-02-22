using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Repo.Dto.DeviceDto;
using Repo.Models;

namespace Repo.MapperProfiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceDtoGet>();
            CreateMap<DeviceDtoPost, Device>();
            CreateMap<DeviceDtoPut, Device>();
        }
    }
}
