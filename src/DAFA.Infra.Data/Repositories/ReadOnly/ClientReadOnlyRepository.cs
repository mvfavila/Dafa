using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using Dapper;
using System.Collections.Generic;

namespace DAFA.Infra.Data.Repositories.ReadOnly
{
    public class ClientReadOnlyRepository
        : BaseReadOnlyRepository<Client, DAFAContext>, IClientReadOnlyRepository
    {
        public IEnumerable<Client> GetActive()
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM Client c WHERE c.ACTIVE = true";

            using (var connection = Connection)
            {
                connection.Open();

                var clients = connection.Query<Client>(sql);

                return clients;
            }
        }

        public new IEnumerable<Client> GetAll()
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM Client c";

            using (var connection = Connection)
            {
                connection.Open();

                var clients = connection.Query<Client>(sql);

                return clients;
            }
        }
    }
}
