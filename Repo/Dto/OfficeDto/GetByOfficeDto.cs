using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Dto.PersonDto;

namespace Repo.Dto.OfficeDto
{
    public class GetByOfficeDto
    {
        public string OfficeName { get; set; }
        public List<PersonDtoGet> Persons { get; set; }
    }
}
