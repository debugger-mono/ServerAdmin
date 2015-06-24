using System.Collections.Generic;
using Tbl.ServerAdmin.DataAccess.Core;

namespace Tbl.ServerAdmin.DataAccess.Commands
{
    public class SqlLiteCommandDictionary : ICommandDictionary
    {
        private readonly Dictionary<string, string> dictionary;

        public SqlLiteCommandDictionary()
        {
            this.dictionary = new Dictionary<string, string>();
            this.PopulateDictionary();
        }

        public Dictionary<string, string> GetCommandsDictionary()
        {
            return this.dictionary;
        }

        private void PopulateDictionary()
        {
            this.UserAccountHandler();
        }

        private void UserAccountHandler()
        {
            this.dictionary.Add(CommandNames.P_UserAccount_Insert, @"INSERT INTO UserAccount (Username, Password) VALUES (@Username, @Password);");
            this.dictionary.Add(CommandNames.P_UserAccount_Validate, @"SELECT * FROM UserAccount WHERE Username = @Username AND Password = @Password LIMIT 1;");
        }
    }
}
