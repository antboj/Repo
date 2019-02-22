using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Dto.UsageDto
{
    public class UsageAllByPersonDtoGet
    {
        public string Device { get; set; }
        public IEnumerable<UsageTimeDtoGet> Usages { get; set; }
    }
}
