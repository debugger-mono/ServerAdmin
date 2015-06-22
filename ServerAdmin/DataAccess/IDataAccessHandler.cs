
using System;
using System.Collections.Generic;
using System.Data;

namespace ServerAdmin.DataAccess
{
    public interface IDataAccessHandler
    {
        T ConstructObject<T>(string command, Func<IDataReader, T> constructor);

        T ConstructObject<T>(string command, Func<IDataReader, T> constructor, params object[] args);

        List<T> ConstructList<T>(string command, Func<IDataReader, T> constructor);

        List<T> ConstructList<T>(string storedProcedure, Func<IDataReader, T> constructor, params object[] args);

        IDataReader ExecuteReader(string command);

        IDataReader ExecuteReader(string command, params object[] args);

        object ExecuteScalar(string command, params object[] args);

        bool InTransaction();

        void BeginTransaction();

        void RunInTransaction(Action tc);

        void Rollback();

        void Commit();

        T Convert<T>(object data);
    }
}