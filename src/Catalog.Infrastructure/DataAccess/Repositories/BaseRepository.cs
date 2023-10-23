using System.Data;

namespace Catalog.Infrastructure.DataAccess.Repositories
{
    public class BaseRepository
    {
        internal IDbConnection connection;

        public BaseRepository(IDbConnection connection)
        {
            this.connection = connection;
        }
    }
}

