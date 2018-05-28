using DAFA.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAFA.Infra.Data.EntityConfig
{
    public class EventTypeConfiguration : EntityTypeConfiguration<EventType>
    {
        public EventTypeConfiguration()
        {
            // For more information google Fluent API

            HasKey(e => e.EventTypeId);

            Property(e => e.Name)
                .HasMaxLength(250)
                .IsRequired();

            Property(e => e.NumberOfDaysToWarning)
                .IsRequired();

            // boolean values are required by nature
            Property(e => e.Active)
                .IsRequired();

            Ignore(e => e.ValidationResult);
        }
    }
}
