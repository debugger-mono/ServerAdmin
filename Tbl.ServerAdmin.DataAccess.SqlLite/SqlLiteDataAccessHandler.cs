using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
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

        public override IEnumerable<IDbDataParameter> DiscoverParameters(IDbCommand command)
        {
            if (command.CommandType == CommandType.Text)
            {
                return this.DiscoverParametersInternal(command);
            }
            else
            {
                throw new NotImplementedException("Only CommandType:Text are supported on SqlLite");
            }
        }

        private IEnumerable<IDbDataParameter> DiscoverParametersInternal(IDbCommand command)
        {
            string commandText = command.CommandText;
            Regex regex = new Regex(@"@\w*");
            MatchCollection matches = regex.Matches(commandText);
            List<SqliteParameter> cmdParams = new List<SqliteParameter>();

            foreach (Match match in matches)
            {
                cmdParams.Add(new SqliteParameter(match.Value));
            }

            return cmdParams;
        }

        #endregion
    }
}

