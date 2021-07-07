using BaseApp.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseApp.Infra.Data.DbMappings
{
    public abstract class EntityMapping<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public abstract void ConfigureEntityProperties(EntityTypeBuilder<T> builder);

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Ignore(p => p.IsValid);

            builder.Ignore(p => p.ValidationResult);

            ConfigureEntityProperties(builder);
        }
    }
}