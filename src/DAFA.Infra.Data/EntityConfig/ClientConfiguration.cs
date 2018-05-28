using DAFA.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAFA.Infra.Data.EntityConfig
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            // For more information google Fluent API

            HasKey(p => p.ClientId);

            Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.Phone)
                .HasMaxLength(25)
                .IsRequired();

            // boolean values are required by nature
            Property(p => p.Active)
                .IsRequired();

            Ignore(p => p.ValidationResult);

        }
    }
}
