using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        private static Expression<Func<T, bool>> GetWhere<T>(string propertyName, string value)
        {
            // o =>
            ParameterExpression param = Expression.Parameter(typeof(T), "o");
            // o.Description
            var propertyEx = Expression.Property(param, propertyName);
            // name
            var constantEx = Expression.Constant(value);
            // ==
            BinaryExpression binaryEx = Expression.Equal(propertyEx, constantEx);

            return Expression.Lambda<Func<T, bool>>(binaryEx, param);
        }

        public IQueryable GetByOffice(string name, string prop)
        {
            var ex = GetWhere<Office>(prop, name);

            var office =  _context.Offices.Where(ex).Include(p => p.Persons); 
            if (!office.Any())
            {
                throw new MyException("Not Found");
            }

            return office;
        }

        public IQueryable GetOffice(string name)
        {
            var office = _context.Offices.Where(o => o.Description == name).Include(p => p.Persons);
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
