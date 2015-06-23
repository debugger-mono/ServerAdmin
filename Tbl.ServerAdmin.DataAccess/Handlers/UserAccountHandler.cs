using System;
using System.Data;
using Tbl.ServerAdmin.DataAccess.Commands;
using Tbl.ServerAdmin.DataAccess.Core;
using Tbl.ServerAdmin.DataAccess.Models;

namespace Tbl.ServerAdmin.DataAccess.Handlers
{
    public class UserAccountHandler : IUserAccountHandler, IConstruct<UserAccount>
    {
        private readonly IDataAccessHandler<ServerAdminDbContext> dataAccess;
        private readonly ICommandTextProvider<ServerAdminDbContext> commandProvider;

        public UserAccountHandler(IDataAccessHandler<ServerAdminDbContext> dataAccess, ICommandTextProvider<ServerAdminDbContext> commandProvider)
        {
            this.dataAccess = dataAccess;
            this.commandProvider = commandProvider;
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

            string command = this.commandProvider.GetCommandText(CommandNames.P_UserAccount_Insert);

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

            string command = this.commandProvider.GetCommandText(CommandNames.P_UserAccount_Validate);

            var result = (bool)this.dataAccess.ExecuteScalar(command, new[] { account.Username, account.Password });

            return result;
        }
    }
}
