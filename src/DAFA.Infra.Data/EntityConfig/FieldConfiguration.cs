using DAFA.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAFA.Infra.Data.EntityConfig
{
    public class FieldConfiguration : EntityTypeConfiguration<Field>
    {
        public FieldConfiguration()
        {
            // For more information google Fluent API

            HasKey(p => p.FieldId);

            Property(p => p.Name)
                .HasMaxLength(250)
                .IsRequired();

            // boolean values are required by nature
            Property(p => p.Active)
                .IsRequired();

            Ignore(p => p.ValidationResult);

            HasRequired(f => f.Client)
                .WithMany(c => c.Fields)
                .HasForeignKey(f => f.ClientId);
        }
    }
}
