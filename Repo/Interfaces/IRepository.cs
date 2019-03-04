using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo.Interfaces
{
    public interface IRepository<TEntity, IdType> where TEntity : class
    {
        TEntity GetById(IdType id);

        IEnumerable<TEntity> Get();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(IdType id);
    }
}
