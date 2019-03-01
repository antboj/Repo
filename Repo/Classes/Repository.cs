﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo.Interfaces;
using Repo.Models;

namespace Repo.Classes
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly RepoContext _context;

        public Repository(RepoContext context)
        {
            _context = context;
        }

        public virtual TEntity GetById(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity == null)
            {
                throw new Exception("Not found");
            }

            return entity;
        }

        public virtual IEnumerable<TEntity> Get()
        {
            var entities = _context.Set<TEntity>().ToList();
            if (entities == null)
            {
                throw new Exception("Not Found");
            }

            return entities;
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public virtual void Remove(int id)
        {
            var type = _context.Set<TEntity>().Find(id);
            if (type == null)
            {
                throw new MyException("Not Found");
            }
            _context.Set<TEntity>().Remove(type);
        }
    }
}
