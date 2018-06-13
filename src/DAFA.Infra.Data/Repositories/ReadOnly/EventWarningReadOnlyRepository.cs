using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using Dapper;
using System;
using System.Collections.Generic;

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
	                                e.*
                                FROM EventWarning ew
                                INNER JOIN Event e ON ew.EventId = e.EventId
                                WHERE ew.Solved = 'False'";

            using (var connection = Connection)
            {
                connection.Open();

                var events = connection.Query<EventWarning, Event, EventWarning>(
                        sql,
                        (ew, e) =>
                        {
                            ew.SetEvent(e);
                            return ew;
                        });

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
    }
}
