using System;
using System.Data;

namespace Tbl.ServerAdmin.DataAccess.Core
{
	public static class DataReaderExtension
	{
		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T">The type of column</typeparam>
		/// <param name="reader">The reader.</param>
		/// <param name="columnName">The column Name.</param>
		/// <param name="columnPrefix">Prefix to column name</param>
		/// <returns></returns>
		public static T GetValue<T> (this IDataRecord reader, string columnName, string columnPrefix)
		{
			return GetValue<T> (reader, string.Format ("{0}{1}", columnPrefix, columnName));
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T">The type of column</typeparam>
		/// <param name="reader">The reader.</param>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		public static T GetValue<T> (this IDataRecord reader, string column)
		{
			return GetValue<T> (reader, column, default(T));
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T">The type of column</typeparam>
		/// <param name="reader">The reader.</param>
		/// <param name="column">The column.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static T GetValue<T> (this IDataRecord reader, string column, T defaultValue)
		{
			T value = defaultValue;

			int ordinal = reader.GetOrdinal (column);

			if (ordinal < 0) {
				throw new ArgumentException (string.Concat ("There is no column with the name '", column, "' part of the record."));
			}

			if (!reader.IsDBNull (ordinal)) {
				object obj = reader.GetValue (ordinal);

				// All date read from the database should be interpreted as UTC
				if (obj is DateTime) {
					obj = DateTime.SpecifyKind ((DateTime)obj, DateTimeKind.Utc);
				}

				value = (T)obj;
			}

			return value;
		}
	}
}

