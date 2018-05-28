using DAFA.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAFA.Infra.Data.EntityConfig
{
    public class PeriodicityConfiguration : EntityTypeConfiguration<Periodicity>
    {
        public PeriodicityConfiguration()
        {
            // For more information google Fluent API

            HasKey(p => p.PeriodicityId);

            Property(p => p.Description)
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.Quantity)
                .IsRequired();

            Property(p => p.Unit)
                .IsRequired();

            // boolean values are required by nature
            Property(p => p.Active)
                .IsRequired();

            Ignore(p => p.ValidationResult);
        }
    }
}
