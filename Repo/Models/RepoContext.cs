using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repo.Models
{
    public class RepoContext : DbContext

    {
        public RepoContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Usage> Usages { get; set; }
    }
}
