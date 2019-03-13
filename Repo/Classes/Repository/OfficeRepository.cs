using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Classes.Attributes;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    //[Service]
    public class OfficeRepository : Repository<Office, int>, IOfficeRepository
    {
        private readonly RepoContext _context;

        public OfficeRepository(RepoContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetByOffice(string name)
        {
            var office =  _context.Offices.Where(o => o.Description == name).Include(p => p.Persons);
            if (!office.Any())
            {
                throw new MyException("Not Found");
            }

            return office;
        }

        public void Unos(Office input)
        {
            _context.Add(input);
            //_context.SaveChanges();
            //throw new MyException("nije sacuvano");
        }
    }
}
