using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Dto.UsageDto
{
    public class UsageGetQueryInfoDtoGet
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public string Person { get; set; }
        public string DeviceId { get; set; }
        public string Device { get; set; }
        public DateTime UsedFrom { get; set; }
        public DateTime? UsedTo { get; set; }
    }
}
