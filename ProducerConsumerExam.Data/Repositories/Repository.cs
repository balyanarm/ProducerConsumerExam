using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProducerConsumerExam.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
            Query = Context.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't find entity: {ex.Message}");
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return Context.Set<TEntity>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't get  entities: {ex.Message}");
            }

        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Add)} entity must not be null");
            }
            Context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(Add)} entity must not be null");
            }
            Context.Set<TEntity>().Remove(entity);
        }

        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Context.Set<TEntity>().Update(entity);
            }
        }

        public IQueryable<TEntity> Query { get; }
    }
}
