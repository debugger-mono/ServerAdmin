using System;
using System.Collections.Generic;
using System.Data;

namespace Tbl.ServerAdmin.DataAccess.Core
{
    public abstract class BaseDataAccessHandler<TDBContext> : IDataAccessHandler<TDBContext> where TDBContext : DatabaseContext
    {
        protected IDbConnection connection;
        protected IDbTransaction transaction;
        protected TDBContext dbContext;

        public BaseDataAccessHandler(TDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract IDbConnection GetConnection();

        public virtual IDataReader ExecuteReader(string command)
        {
            return this.ExecuteReader(command, new object[] { });
        }

        public virtual IDataReader ExecuteReader(string command, params object[] args)
        {
            return this.ExecuteReader(command, CommandType.StoredProcedure, args);
        }

        public virtual IDataReader ExecuteReader(string command, CommandType commandType, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentNullException("command can't be null or empty");
            }

            if (args == null)
            {
                throw new ArgumentNullException("args can't be null");
            }

            using (IDbCommand cmd = this.connection.CreateCommand())
            {
                cmd.CommandText = command;
                cmd.CommandType = commandType;

                foreach (object arg in args)
                {
                    cmd.Parameters.Add(arg);
                }

                IDataReader reader = null;

                if (this.InTransaction())
                {
                    reader = cmd.ExecuteReader();
                }
                else
                {
                    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }

                return reader;
            }
        }

        public virtual object ExecuteScalar(string command, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentNullException("command can't be null or empty");
            }

            if (args == null)
            {
                throw new ArgumentNullException("args can't be null");
            }

            object returnValue = null;

            this.RunInTransaction(
                () =>
                {
                    using (IDataReader reader = this.ExecuteReader(command, args))
                    {
                        if (reader.Read() && (reader.FieldCount > 0))
                        {
                            returnValue = reader.GetValue(0);
                        }
                    }
                });

            return returnValue;
        }

        public virtual bool InTransaction()
        {
            return (this.GetTransaction() != null);
        }

        public virtual void BeginTransaction()
        {
            // check if currently in a transaction
            if (this.InTransaction())
            {
                throw new DataException("You can't begin a new transaction where there is already an active transaction.");
            }

            IDbConnection conn = this.GetConnection();
            IDbTransaction transaction = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            this.SetTransaction(transaction);
        }

        public virtual void RunInTransaction(Action tc)
        {
            bool isTransactionOwner = !this.InTransaction();

            // If we're the transaction owner we are responsible for beginning the transaction.
            if (isTransactionOwner)
            {
                this.BeginTransaction();
            }

            try
            {
                tc();

                // Commit if we are the transaction owner
                if (isTransactionOwner)
                {
                    this.Commit();
                }
            }
            catch
            {
                // Rollback if we are the transaction owner
                if (isTransactionOwner)
                {
                    this.Rollback();
                }

                throw;
            }
        }

        public virtual void Rollback()
        {
            IDbTransaction transaction = this.GetTransaction();

            if (transaction != null)
            {
                IDbConnection connection = transaction.Connection;
                try
                {
                    transaction.Rollback();
                    this.SetTransaction(null);
                }
                finally
                {
                    if (connection != null && connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                throw new DataException("No transaction is available for rollback.");
            }
        }

        public virtual void Commit()
        {
            IDbTransaction transaction = this.GetTransaction();

            if (transaction == null)
            {
                throw new DataException("You can't commit a transaction where there is no active transaction.");
            }

            IDbConnection connection = transaction.Connection;

            try
            {
                transaction.Commit();
                this.SetTransaction(null);
            }
            finally
            {
                connection.Close();
            }
        }

        public T ConstructObject<T>(string command, Func<IDataReader, T> constructor)
        {
            return this.ConstructObject<T>(command, constructor, new object[] { });
        }

        public T ConstructObject<T>(string command, Func<IDataReader, T> constructor, params object[] args)
        {
            using (IDataReader reader = this.ExecuteReader(command, args))
            {
                if (reader.Read())
                {
                    T obj = constructor(reader);
                    return obj;
                }

                return default(T);
            }
        }

        public List<T> ConstructList<T>(string command, Func<IDataReader, T> constructor)
        {
            return this.ConstructList<T>(command, constructor, new object[] { });
        }

        public List<T> ConstructList<T>(string storedProcedure, Func<IDataReader, T> constructor, params object[] args)
        {
            List<T> list = new List<T>();

            using (IDataReader reader = this.ExecuteReader(storedProcedure, args))
            {
                while (reader.Read())
                {
                    T obj = constructor(reader);

                    if (obj != null)
                    {
                        list.Add(obj);
                    }
                }
            }

            return list;
        }

        public T Convert<T>(object data)
        {
            T value = default(T);

            // if we're dealing with a generic type
            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                value = (T)System.Convert.ChangeType(data, Nullable.GetUnderlyingType(typeof(T)));
            }
            else
            {
                value = (T)System.Convert.ChangeType(data, typeof(T));
            }

            return value;
        }

        protected IDbTransaction GetTransaction()
        {
            return this.transaction;
        }

        protected void SetTransaction(IDbTransaction transaction)
        {
            this.transaction = transaction;
        }
    }
}

