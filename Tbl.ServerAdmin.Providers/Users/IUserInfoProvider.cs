using System.Collections.Generic;

namespace Tbl.ServerAdmin.Providers.Users
{
    public interface IUserInfoProvider
    {
        List<IUserInfo> GetUsers();
    }
}
