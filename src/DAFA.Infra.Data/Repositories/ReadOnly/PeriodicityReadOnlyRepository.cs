using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using Dapper;
using System.Collections.Generic;

namespace DAFA.Infra.Data.Repositories.ReadOnly
{
    public class PeriodicityReadOnlyRepository
        : BaseReadOnlyRepository<Periodicity, DAFAContext>, IPeriodicityReadOnlyRepository
    {
        public IEnumerable<Periodicity> GetActive()
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM Periodicity p WHERE p.ACTIVE = true";

            using (var connection = Connection)
            {
                connection.Open();

                var periodicities = connection.Query<Periodicity>(sql);

                return periodicities;
            }
        }

        public new IEnumerable<Periodicity> GetAll()
        {
            // TODO: SQL Commands and Querys should be moved to a readonly Command or Query class
            const string sql = "SELECT * FROM Periodicity p";

            using (var connection = Connection)
            {
                connection.Open();

                var periodicities = connection.Query<Periodicity>(sql);

                return periodicities;
            }
        }
    }
}
