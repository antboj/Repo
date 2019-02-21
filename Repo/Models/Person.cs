using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public Office Office { get; set; }

        public IList<Device> Devices { get; set; }
    }
}
