using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using Dapper;
using System.Collections.Generic;

namespace DAFA.Infra.Data.Repositories.ReadOnly
{
    public class EventTypeReadOnlyRepository
        : BaseReadOnlyRepository<EventType, DAFAContext>, IEventTypeReadOnlyRepository
    {
        public IEnumerable<EventType> GetActive()
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM EventType e WHERE e.ACTIVE = true";

            using (var connection = Connection)
            {
                connection.Open();

                var examples = connection.Query<EventType>(sql);

                return examples;
            }
        }

        public new IEnumerable<EventType> GetAll()
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM EventType e";

            using (var connection = Connection)
            {
                connection.Open();

                var examples = connection.Query<EventType>(sql);

                return examples;
            }
        }
    }
}
