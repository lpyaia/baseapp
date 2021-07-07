using BaseApp.Domain.DomainObjects;
using System;
using System.Collections.Generic;

namespace BaseApp.Domain.Repositories
{
    public interface IRepository<T> where T : AggregateRoot
    {
        void Delete(params T[] entities);

        T? GetById(Guid id);

        List<T> GetAll();

        void Insert(params T[] entities);

        void Update(params T[] entities);
    }
}