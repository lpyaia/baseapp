using BaseApp.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApp.Infra.Data.DbMappings
{
    public class CustomerMapping : EntityMapping<Customer>
    {
        public override void ConfigureEntityProperties(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(p => p.FirstName)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}