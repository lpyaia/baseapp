using BaseApp.Domain.DomainObjects;

namespace BaseApp.Domain.Entities.Customers
{
    public class Customer : AggregateRoot
    {
        public string FirstName { get; }

        public string LastName { get; }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            Validate(this, new CustomerValidator());
        }
    }
}