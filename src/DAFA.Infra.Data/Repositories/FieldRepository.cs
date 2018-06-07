using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;

namespace DAFA.Infra.Data.Repositories
{
    public class FieldRepository : BaseRepository<Field, DAFAContext>, IFieldRepository
    {
        public new void Update(Field model)
        {
            var context = ((DAFAContext)base.context);

            var existingParent = context.Fields
                .Where(f => f.FieldId == model.FieldId)
                .Include(p => p.Events)
                .SingleOrDefault();

            // Update parent
            context.Entry(existingParent).CurrentValues.SetValues(model);

            // Delete children
            foreach (var existingChild in existingParent.Events.ToList())
            {
                if (!model.Events.Any(e => e.EventId == existingChild.EventId))
                    context.Events.Remove(existingChild);
            }

            // Update and Insert children
            foreach (var childModel in model.Events)
            {
                var existingChild = existingParent.Events
                    .SingleOrDefault(e => e.EventId == childModel.EventId);

                if (existingChild != null)
                    // Update child
                    context.Entry(existingChild).CurrentValues.SetValues(childModel);
                else
                {
                    // Insert child
                    existingParent.Events.Add(childModel);
                }
            }
        }
    }
}
