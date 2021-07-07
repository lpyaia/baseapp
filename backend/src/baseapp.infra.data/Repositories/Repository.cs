using BaseApp.Domain.DomainObjects;
using BaseApp.Domain.Repositories;
using BaseApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseApp.Infra.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        protected readonly BaseAppDbContext DbContext;
        protected readonly DbSet<T> DbSet;

        protected Repository(BaseAppDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public void Delete(params T[] entities)
        {
            DbSet.RemoveRange(entities);
        }

        public List<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T? GetById(Guid id)
        {
            return DbSet.Where(w => w.Id == id).FirstOrDefault();
        }

        public void Insert(params T[] entities)
        {
            DbSet.AddRange(entities);
        }

        public void Update(params T[] entities)
        {
            DbSet.UpdateRange(entities);
        }
    }
}