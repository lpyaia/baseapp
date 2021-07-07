using BaseApp.Domain.IoC;
using BaseApp.Infra.Data.Context;

namespace BaseApp.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BaseAppDbContext _dbContext;

        public UnitOfWork(BaseAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}