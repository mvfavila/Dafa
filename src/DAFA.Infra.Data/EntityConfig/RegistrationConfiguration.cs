using DAFA.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAFA.Infra.Data.EntityConfig
{
    public class RegistrationConfiguration : EntityTypeConfiguration<Registration>
    {
        public RegistrationConfiguration()
        {
            // For more information google Fluent API

            HasKey(e => e.RegistrationId);

            HasRequired(r => r.Field)
                .WithMany(f => f.Registrations)
                .HasForeignKey(r => r.FieldId);

            HasRequired(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId);

            HasRequired(r => r.Periodicity)
                .WithMany(p => p.Registrations)
                .HasForeignKey(r => r.PeriodicityId);

            Property(e => e.Active)
                .IsRequired();

            Ignore(e => e.ValidationResult);
        }
    }
}
