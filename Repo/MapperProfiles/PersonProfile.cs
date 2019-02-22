using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Repo.Dto.OfficeDto;
using Repo.Dto.PersonDto;
using Repo.Models;

namespace Repo.MapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDtoGet>();
            CreateMap<PersonDtoPost, Person>();
            CreateMap<PersonDtoPut, Person>();

            //CreateMap<Office, GetByOfficeDto>()
            //    .ForMember(d => d.OfficeName, s => s.MapFrom(x => x.Description));
        }
    }
}

