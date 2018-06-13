using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using Dapper;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Event> GetOverdueEvents()
        {
            const string sql = @"SELECT
                                    e.*,
	                                field.FieldId as 'Id',
	                                field.*
                                FROM Event e
                                INNER JOIN Field field ON e.FieldId = field.FieldId
                                INNER JOIN EventType eventType ON eventType.EventTypeId = e.EventTypeId
                                WHERE DATEDIFF(day, GETDATE(), e.Date) <= eventType.NumberOfDaysToWarning";

            using (var connection = Connection)
            {
                connection.Open();

                var events = connection.Query<Event, Field, Event>(
                        sql,
                        (e, field) =>
                        {
                            e.SetField(field);
                            return e;
                        });

                return events.ToList();
            }
        }
    }
}
