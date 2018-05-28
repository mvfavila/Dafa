using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using Dapper;
using System.Collections.Generic;

namespace DAFA.Infra.Data.Repositories.ReadOnly
{
    public class EventReadOnlyRepository
        : BaseReadOnlyRepository<Event, DAFAContext>, IEventReadOnlyRepository
    {
        public IEnumerable<Event> GetActive()
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM Event e WHERE e.ACTIVE = true";

            using (var connection = Connection)
            {
                connection.Open();

                var examples = connection.Query<Event>(sql);

                return examples;
            }
        }

        public new IEnumerable<Event> GetAll()
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM Event e";

            using (var connection = Connection)
            {
                connection.Open();

                var examples = connection.Query<Event>(sql);

                return examples;
            }
        }
    }
}
