using System.Data.Entity;
using DAFA.Infra.Data.Interfaces;

namespace DAFA.Infra.Data.Context
{
    /// <summary>
    /// Base database context.
    /// </summary>
    public class BaseDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// Represents the standard max number of characters in a varchar column.
        /// </summary>
        internal const int STANDARD_VARCHAR_COLUMN_MAX_SIZE = 100;
        /// <summary>
        /// Represents the name of the columns which holds the date when the<br/>
        /// registy was recorded in the database.
        /// </summary>
        internal const string NAME_FOR_REGISTER_DATE_PROPERTY = "RegisterDate";

        /// <summary>
        /// Database base context constructor.
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public BaseDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {

        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}