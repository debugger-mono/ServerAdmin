using Tbl.ServerAdmin.Providers.Users;

namespace Tbl.ServerAdmin.Providers.Linux.Users
{
    public class UserInfo : IUserInfo
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public int UserId { get; set; }

        public int GroupId { get; set; }
    }
}
