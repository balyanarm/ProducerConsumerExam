using System.Collections.Generic;
using System.Linq;

namespace ProducerConsumerExam.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);

        void Remove(TEntity entity);

        IQueryable<TEntity> Query { get; }

        void UpdateAll(IEnumerable<TEntity> entities);
    }
}
