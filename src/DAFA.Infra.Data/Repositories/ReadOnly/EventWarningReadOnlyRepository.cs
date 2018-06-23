using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAFA.Infra.Data.Repositories.ReadOnly
{
    public class EventWarningReadOnlyRepository
        : BaseReadOnlyRepository<EventWarning, DAFAContext>, IEventWarningReadOnlyRepository
    {
        public IEnumerable<EventWarning> GetUnsolved()
        {
            const string sql = @"SELECT
                                     ew.*,
	                                 e.EventId as 'Id',
	                                 e.*,
	                                 e.FieldId as 'Id2',
	                                 f.*
                                 FROM EventWarning ew
                                 INNER JOIN Event e ON ew.EventId = e.EventId
                                 INNER JOIN Field f ON e.FieldId = f.FieldId
                                 WHERE ew.Solved = 'False'";

            using (var connection = Connection)
            {
                connection.Open();

                var events = connection.Query<EventWarning, Event, Field, EventWarning>(
                        sql,
                        (ew, e, f) =>
                        {
                            e.SetField(f);
                            ew.SetEvent(e);
                            return ew;
                        },
                        splitOn: "Id, Id2");

                return events;
            }
        }

        public IEnumerable<EventWarning> GetUnsolvedByClient(Guid id)
        {
            const string sql = @"SELECT
                                    ew.*,
	                                e.EventId as 'Id',
	                                e.*
                                FROM EventWarning ew
                                INNER JOIN Event e ON ew.EventId = e.EventId
                                INNER JOIN Field f ON f.FieldId = e.FieldId
                                WHERE f.ClientId = @CLIENT_ID
                                    AND ew.Solved = 'False'";

            using (var connection = Connection)
            {
                connection.Open();

                var events = connection.Query<EventWarning, Event, EventWarning>(
                        sql,
                        (ew, e) =>
                        {
                            ew.SetEvent(e);
                            return ew;
                        },
                        new { CLIENT_ID = id });

                return events;
            }
        }

        public new EventWarning GetById(Guid id)
        {
            const string sql = @"SELECT
                                     ew.*,
	                                 e.EventId as 'Id',
	                                 e.*,
	                                 e.FieldId as 'Id2',
	                                 f.*
                                 FROM EventWarning ew
                                 INNER JOIN Event e ON ew.EventId = e.EventId
                                 INNER JOIN Field f ON e.FieldId = f.FieldId
                                 WHERE ew.EventWarningId = @EVENT_WARNING_ID";

            using (var connection = Connection)
            {
                connection.Open();

                var events = connection.Query<EventWarning, Event, Field, EventWarning>(
                        sql,
                        (ew, e, f) =>
                        {
                            e.SetField(f);
                            ew.SetEvent(e);
                            return ew;
                        },
                        new { EVENT_WARNING_ID = id },
                        splitOn: "Id, Id2");

                return events.FirstOrDefault();
            }
        }
    }
}
