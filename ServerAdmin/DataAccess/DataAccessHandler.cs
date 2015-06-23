using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Mono.Data.Sqlite;

namespace ServerAdmin.DataAccess
{
	public class DataAccessHandler : IDataAccessHandler
	{
		private IDbConnection connection;
		private IDbTransaction transaction;

		public T ConstructObject<T> (string command, Func<IDataReader, T> constructor)
		{
			return this.ConstructObject<T> (command, constructor, new object[] { });
		}

		public T ConstructObject<T> (string command, Func<IDataReader, T> constructor, params object[] args)
		{
			using (IDataReader reader = this.ExecuteReader (command, args)) {
				if (reader.Read ()) {
					T obj = constructor (reader);
					return obj;
				}

				return default(T);
			}
		}

		public List<T> ConstructList<T> (string command, Func<IDataReader, T> constructor)
		{
			return this.ConstructList<T> (command, constructor, new object[] { });
		}

		public List<T> ConstructList<T> (string storedProcedure, Func<IDataReader, T> constructor, params object[] args)
		{
			List<T> list = new List<T> ();

			using (IDataReader reader = this.ExecuteReader (storedProcedure, args)) {
				while (reader.Read ()) {
					T obj = constructor (reader);

					if (obj != null) {
						list.Add (obj);
					}
				}
			}

			return list;
		}

		public object ExecuteScalar (string command, params object[] args)
		{
			if (string.IsNullOrWhiteSpace (command)) {
				throw new ArgumentNullException ("command can't be null or empty");
			}

			if (args == null) {
				throw new ArgumentNullException ("args can't be null");
			}

			object returnValue = null;

			this.RunInTransaction (
				() => {
					using (IDataReader reader = this.ExecuteReader (command, args)) {
						if (reader.Read () && (reader.FieldCount > 0)) {
							returnValue = reader.GetValue (0);
						}
					}
				});

			return returnValue;
		}

		public IDataReader ExecuteReader (string command)
		{
			if (string.IsNullOrWhiteSpace (command)) {
				throw new ArgumentNullException ("command can't be null or empty");
			}

			return this.ExecuteReader (command, new object[] { });
		}

		public IDataReader ExecuteReader (string command, params object[] args)
		{
			if (string.IsNullOrWhiteSpace (command)) {
				throw new ArgumentNullException ("command can't be null or empty");
			}

			if (args == null) {
				throw new ArgumentNullException ("args can't be null");
			}

			using (IDbCommand cmd = this.connection.CreateCommand ()) {
				cmd.CommandText = command;

				foreach (object arg in args) {
					cmd.Parameters.Add (arg);
				}

				IDataReader reader = null;

				if (this.InTransaction ()) {
					reader = cmd.ExecuteReader ();
				} else {
					reader = cmd.ExecuteReader (CommandBehavior.CloseConnection);
				}

				return reader;
			}
		}

		public bool InTransaction ()
		{
			return (this.GetTransaction () != null);
		}

		public void BeginTransaction ()
		{
			// check if currently in a transaction
			if (this.InTransaction ()) {
				throw new DataException ("You can't begin a new transaction where there is already an active transaction.");
			}

			IDbConnection conn = this.GetConnection ();
			IDbTransaction transaction = conn.BeginTransaction (IsolationLevel.ReadCommitted);
			this.SetTransaction (transaction);
		}

		public void RunInTransaction (Action tc)
		{
			bool isTransactionOwner = !this.InTransaction ();

			// If we're the transaction owner we are responsible for beginning the transaction.
			if (isTransactionOwner) {
				this.BeginTransaction ();
			}

			try {
				tc ();

				// Commit if we are the transaction owner
				if (isTransactionOwner) {
					this.Commit ();
				}
			} catch (Exception ex) {
				// Rollback if we are the transaction owner
				if (isTransactionOwner) {
					
					this.Rollback ();
				}

				throw;
			}
		}

		public void Rollback ()
		{
			IDbTransaction transaction = this.GetTransaction ();

			if (transaction != null) {
				IDbConnection connection = transaction.Connection;
				try {
					transaction.Rollback ();
					this.SetTransaction (null);
				} finally {
					if (connection != null && connection.State != ConnectionState.Closed) {
						connection.Close ();
					}
				}
			} else {
				throw new DataException ("No transaction is available for rollback.");
			}
		}

		public void Commit ()
		{
			IDbTransaction transaction = this.GetTransaction ();

			if (transaction == null) {
				throw new DataException ("You can't commit a transaction where there is no active transaction.");
			}

			IDbConnection connection = transaction.Connection;

			try {
				transaction.Commit ();
				this.SetTransaction (null);
			} finally {
				connection.Close ();
			}
		}

		public T Convert<T> (object data)
		{
			T value = default(T);

			// if we're dealing with a generic type
			if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition ().Equals (typeof(Nullable<>))) {
				value = (T)Convert.ChangeType (data, Nullable.GetUnderlyingType (typeof(T)));
			} else {
				value = (T)Convert.ChangeType (data, typeof(T));
			}

			return value;
		}

		private IDbTransaction GetTransaction ()
		{
			return this.transaction;
		}

		private void SetTransaction (IDbTransaction transaction)
		{
			this.transaction = transaction;
		}

		private IDbConnection GetConnection ()
		{
			IDbConnection connection = null;

			if (this.GetTransaction () != null) {
				connection = this.GetTransaction ().Connection;
			} else {
				connection = new SqliteConnection (ConfigurationManager.ConnectionStrings ["DefaultConnection"].ConnectionString);
				connection.Open ();
			}

			return connection;
		}
	}
}