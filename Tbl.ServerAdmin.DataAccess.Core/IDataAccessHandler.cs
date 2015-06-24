using System;
using System.Collections.Generic;
using System.Data;

namespace Tbl.ServerAdmin.DataAccess.Core
{
    public interface IDataAccessHandler<TDBContext> where TDBContext : DatabaseContext
    {
        IDbConnection GetConnection();

        void AddParameters(IDbCommand command, params object[] args);

        T ConstructObject<T>(string command, Func<IDataReader, T> constructor);

        T ConstructObject<T>(string command, Func<IDataReader, T> constructor, params object[] args);

        List<T> ConstructList<T>(string command, Func<IDataReader, T> constructor);

        List<T> ConstructList<T>(string storedProcedure, Func<IDataReader, T> constructor, params object[] args);

        IDataReader ExecuteReader(string command);

        IDataReader ExecuteReader(string command, CommandType commandType, params object[] args);

        object ExecuteScalar(string command, params object[] args);

        bool InTransaction();

        void BeginTransaction();

        void RunInTransaction(Action tc);

        void Rollback();

        void Commit();
    }
}