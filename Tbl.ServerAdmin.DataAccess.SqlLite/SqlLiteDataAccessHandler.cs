using System.Configuration;
using System.Data;
using Mono.Data.Sqlite;
using Tbl.ServerAdmin.DataAccess.Core;

namespace Tbl.ServerAdmin.DataAccess.SqlLite
{
    public class SqlLiteDataAccessHandler<TDBContext> : BaseDataAccessHandler<TDBContext> where TDBContext : DatabaseContext
    {
        public SqlLiteDataAccessHandler(TDBContext dbContext)
            : base(dbContext)
        {
        }

        #region implemented overriden members of BaseDataAccessHandler

        public override IDataReader ExecuteReader(string command, params object[] args)
        {
            return this.ExecuteReader(command, CommandType.Text, args);
        }

        public override IDbConnection GetConnection()
        {
            IDbConnection connection = null;

            if (this.GetTransaction() != null)
            {
                connection = this.GetTransaction().Connection;
            }
            else
            {
                connection = new SqliteConnection(ConfigurationManager.ConnectionStrings[this.dbContext.ConnectionStringKey].ConnectionString);
                connection.Open();
            }

            return connection;
        }

        public override void AddParameters(IDbCommand command, params object[] args)
        {

        }

        #endregion
    }
}

