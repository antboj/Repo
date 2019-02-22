using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Repo.Dto.OfficeDto;
using Repo.Models;

namespace Repo.MapperProfiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<Office, OfficeDtoGet>();
            CreateMap<OfficeDtoPost, Office>();
            CreateMap<OfficeDtoPut, Office>();

            CreateMap<Office, GetByOfficeDto>()
                .ForMember(d => d.OfficeName, s => s.MapFrom(x => x.Description));
        }
    }
}
