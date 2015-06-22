
using System;
using System.Data;
using ServerAdmin.DataAccess.Models;
namespace ServerAdmin.DataAccess
{
    public class UserAccountHandler : IConstruct<UserAccount>
    {
        private readonly DataAccessHandler dataAccess;

        public UserAccountHandler(DataAccessHandler dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public UserAccount Construct(IDataReader reader, string columnPrefix = "")
        {
            return new UserAccount
            {
                Username = reader.GetValue<string>("Username"),
                Password = reader.GetValue<string>("Password")
            };
        }

        public int Add(UserAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("UserAccount can't be null");
            }

            if (string.IsNullOrWhiteSpace(account.Username))
            {
                throw new ArgumentNullException("Username can't be null or empty");
            }

            if (string.IsNullOrWhiteSpace(account.Password))
            {
                throw new ArgumentNullException("Password can't be null or empty");
            }

            const string command = "INSERT INTO UserAccount (Username, Password) VALUES (@Username, @Password)";

            var result = (int)this.dataAccess.ExecuteScalar(command, new[] { account.Username, account.Password });

            return result;
        }

        public bool Validate(UserAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("UserAccount can't be null");
            }

            if (string.IsNullOrWhiteSpace(account.Username))
            {
                throw new ArgumentNullException("Username can't be null or empty");
            }

            if (string.IsNullOrWhiteSpace(account.Password))
            {
                throw new ArgumentNullException("Password can't be null or empty");
            }

            const string command = "SELECT * FROM UserAccount WHERE Username = @Username AND Password = @Password LIMIT 1";

            var result = (bool)this.dataAccess.ExecuteScalar(command, new[] { account.Username, account.Password });

            return result;
        }
    }
}