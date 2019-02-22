using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Dto.UsageDto
{
    public class UsageTimeDtoGet
    {
        public DateTime UsedFrom { get; set; }
        public DateTime? UsedTo { get; set; }
    }
}
