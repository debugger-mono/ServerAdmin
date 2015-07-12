using Tbl.ServerAdmin.Providers.Users;

namespace Tbl.ServerAdmin.Providers.Windows.Users
{
    public class UserInfo : IUserInfo
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Domain { get; set; }

        public string Description { get; set; }

        public bool PasswordRequired { get; set; }
    }
}