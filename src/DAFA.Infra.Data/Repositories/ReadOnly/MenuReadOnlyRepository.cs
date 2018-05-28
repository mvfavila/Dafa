using Dapper;
using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Infra.Data.Context;
using DAFA.Infra.Data.SQL;
using System.Collections.Generic;

namespace DAFA.Infra.Data.Repositories.ReadOnly
{
    public class MenuReadOnlyRepository : BaseReadOnlyRepository<MenuItem, DAFAContext>, IMenuReadOnlyRepository
    {
        public new IEnumerable<MenuItem> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();

                var cities = connection.Query<MenuItem>(MenuQuery.GET_ALL_MENU_ITEMS);

                return cities;
            }
        }
    }
}
