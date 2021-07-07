using BaseApp.Domain.Entities.Customers;
using BaseApp.Infra.Data.Context;

namespace BaseApp.Infra.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(BaseAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}