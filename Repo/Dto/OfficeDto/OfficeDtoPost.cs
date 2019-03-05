using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Dto.OfficeDto
{
    public class OfficeDtoPost
    {
        [Required(ErrorMessage = "Name is required")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Date of birth is required")]
        //public DateTime? DateOfBirth { get; set; }
    }
}
