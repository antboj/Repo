using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Dto.DeviceDto
{
    public class DeviceDtoPost
    {
        [Required]
        public string Name { get; set; }
        public int? PersonId { get; set; }
    }
}
