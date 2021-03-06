﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repo.Classes.Attributes;
using Repo.Classes.Expressions;
using Repo.Dto.UsageDto;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    //[Service]
    public class UsageRepository : Repository<Usage, int>, IUsageRepository
    {
        private readonly RepoContext _context;

        public UsageRepository(RepoContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Usage> AllByDevice(int id)
        {
            return _context.Usages.Where(x => x.DeviceId == id).Include(p => p.Person).Include(d => d.Device);
        }

        public IQueryable<UsageAllByPersonDtoGet> AllByPerson(int id)
        {
            return _context.Usages.Where(d => d.PersonId == id).GroupBy(d => d.Device.Name)
                .Select(x => new UsageAllByPersonDtoGet { Device = x.Key, Usages = x.Select(y => new UsageTimeDtoGet { UsedFrom = y.UsedFrom, UsedTo = y.UsedTo }) });
        }

        public IQueryable<TimeUsedByPersonDtoGet> TimeUsedByPerson(int id)
        {
            return _context.Usages.Where(p => p.PersonId == id).GroupBy(x => x.Device.Name)
                .Select(y => new TimeUsedByPersonDtoGet
                {
                    Device = y.Key,
                    TimeUsed = new TimeSpan(y.Sum(u => (u.UsedTo.Value != null ? u.UsedTo.Value.Ticks : DateTime.Now.Ticks) - u.UsedFrom.Ticks)).ToString(@"dd\.hh\:mm\:ss")
                }); ;
        }

        // -------EXPRESSION SAMPLE------- //
        public IQueryable<Usage> QueryInfo(QueryInfo input)
        {
            var obj = new QueryInfo();

            var query = _context.Usages.Include(p => p.Person).Include(d => d.Device).AsQueryable();

            var rules = input.Filter.Rules;
            var sorters = input.Sorters;
            var condition = input.Filter.Condition;
            var skipNum = input.Skip;
            var takeNum = input.Take > 0 ? input.Take : 1;

            ParameterExpression parameterEx = Expression.Parameter(typeof(Usage), "x");
            Expression result;
            if (condition == "and")
            {
                Expression<Func<Usage, bool>> trueExp = x => true;
                result = trueExp.Body;
            }
            else
            {
                Expression<Func<Usage, bool>> falseExp = x => false;
                result = falseExp.Body;
            }

            foreach (var rule in rules)
            {
                var property = rule.Property;
                var binOperator = rule.Operator;
                var value = rule.Value;
                var binaryExpression = obj.GetBinaryExpression(parameterEx, binOperator, property, value);

               switch (condition)
                {
                    case "and":
                        result = Expression.AndAlso(result, binaryExpression);
                        break;
                    case "or":
                        result = Expression.OrElse(result, binaryExpression);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            var whereEx = obj.GetWhere<Usage>(result, parameterEx);

            query = query.Where(whereEx);

            bool sortedFirst = false;
            foreach (var sort in sorters)
            {
                var sortProperty = sort.Property;
                var sortDirection = sort.SortDirection;
                
                switch (sortDirection)
                {
                    case "asc":
                        if (!sortedFirst)
                        {
                            query = query.OrderBy(obj.GetOrderByExpression<Usage>(sortProperty));
                            sortedFirst = true;
                        }
                        else
                        {
                            query = ((IOrderedQueryable<Usage>)query).ThenBy(obj.GetOrderByExpression<Usage>(sortProperty));
                        }
                        break;
                    case "desc":
                        if (!sortedFirst)
                        {
                            query = query.OrderByDescending(obj.GetOrderByExpression<Usage>(sortProperty));
                            sortedFirst = true;
                        }
                        else
                        {
                            query = ((IOrderedQueryable<Usage>)query).ThenByDescending(obj.GetOrderByExpression<Usage>(sortProperty));
                        }
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            
            query = query.Skip(skipNum).Take(takeNum);
            return query;
            
            //var op = input.Filter.Rules[0].Operator;
            //var prop = input.Filter.Rules[0].Property;
            //var src = input.Filter.Rules[0].Value;
            //var ob = input.Sorters[0].Property;

            //var qu = new QueryInfo();

            //var whereExFilter = qu.GetWhereExpression<Usage>(op, prop, src);

            //var orderByFilter = qu.GetOrderByExpression<Usage>(ob);

            //query = query.OrderBy(x => x.UsedFrom);

            //query = ((IOrderedQueryable<Usage>) query).ThenBy(x => x.Id);


            //return query.Where(whereExFilter).OrderBy(orderByFilter);
        }
        // -------------------------------- //
        
        public bool IsCurrentlyUsed(int id)
        {
            var isCurrentlyUsed = _context.Usages.Any(x => x.DeviceId == id && x.UsedTo == null);

            return isCurrentlyUsed;
        }

        public void EndUsing(int id)
        {
            var usageRecord = _context.Usages.FirstOrDefault(u => u.DeviceId == id && u.UsedTo == null);

            if (usageRecord != null)
            {
                usageRecord.UsedTo = DateTime.Now;
                //_context.SaveChanges();
            }
        }

        public void AddUsage(int dId, int pId)
        {
            var usage = new Usage
            {
                PersonId = pId,
                DeviceId = dId,
                UsedFrom = DateTime.Now
            };

            _context.Usages.Add(usage);
           // _context.SaveChanges();
        }

        public override IEnumerable<Usage> Get()
        {
            return _context.Usages.Include(p => p.Person).Include(d => d.Device);
        }

        public override Usage GetById(int id)
        {
            throw new InvalidOperationException("Za ovaj entitet nije podrzana metoda Update");
        }

        public override void Update(Usage entity)
        {
            throw new InvalidOperationException("Za ovaj entitet nije podrzana metoda Update");
        }

        public override void Add(Usage entity)
        {
            throw new InvalidOperationException("Za ovaj entitet nije podrzana metoda Add");
            
        }

        public override void Remove(int id)
        {
            throw new InvalidOperationException("Za ovaj entitet nije podrzana metoda Remove");
        }
    }
}
