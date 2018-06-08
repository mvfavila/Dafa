using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using Dapper;

namespace DAFA.Infra.Data.Repositories.ReadOnly
{
    public class FieldReadOnlyRepository
        : BaseReadOnlyRepository<Field, DAFAContext>, IFieldReadOnlyRepository
    {
        public new Field GetById(Guid id)
        {
            var context = ((DAFAContext)base.context);

            return context.Fields
                .Where(f => f.FieldId == id)
                .Include(f => f.Events)
                .SingleOrDefault();
        }

        public IEnumerable<Field> GetActiveByClient(Guid clientId)
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM Field f WHERE f.ACTIVE = true AND f.ClientId = @CLIENT_ID";

            using (var connection = Connection)
            {
                connection.Open();

                var clients = connection.Query<Field>(sql, new { CLIENT_ID = clientId });

                return clients;
            }
        }

        public IEnumerable<Field> GetAllByClient(Guid clientId)
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM Field f WHERE f.ClientId = @CLIENT_ID";

            using (var connection = Connection)
            {
                connection.Open();

                var clients = connection.Query<Field>(sql, new { CLIENT_ID = clientId });

                return clients;
            }
        }
    }
}
