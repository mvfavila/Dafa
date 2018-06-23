using DAFA.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DAFA.Infra.Data.EntityConfig
{
    public class EventWarningConfiguration : EntityTypeConfiguration<EventWarning>
    {
        public EventWarningConfiguration()
        {
            // For more information google Fluent API

            HasKey(e => e.EventWarningId);

            Property(e => e.Date)
                .IsRequired();

            Property(e => e.SolvedDate)
                .IsOptional();

            Property(e => e.EventId)
                .IsRequired();

            HasRequired(ew => ew.Event)
                .WithMany(e => e.EventWarnings)
                .HasForeignKey(ew => ew.EventId);
        }
    }
}
