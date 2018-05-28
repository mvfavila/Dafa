using DAFA.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAFA.Infra.Data.EntityConfig
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            // For more information google Fluent API

            HasKey(e => e.EventId);

            Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            Property(e => e.Description)
                .HasMaxLength(250)
                .IsRequired();

            Property(e => e.Date)
                .IsRequired();

            Property(e => e.EventTypeId)
                .IsRequired();

            HasRequired(e => e.EventType)
                .WithMany(et => et.Events)
                .HasForeignKey(e => e.EventTypeId);

            Ignore(e => e.ValidationResult);
        }
    }
}
