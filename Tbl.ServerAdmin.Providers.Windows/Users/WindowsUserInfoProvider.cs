using System.Collections.Generic;
using System.Management;
using Tbl.ServerAdmin.Providers.Users;

namespace Tbl.ServerAdmin.Providers.Windows.Users
{
    public class WindowsUserInfoProvider : IUserInfoProvider
    {
        public List<IUserInfo> GetUsers()
        {
            ManagementObjectSearcher usersSearcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_UserAccount WHERE LocalAccount = True");
            ManagementObjectCollection users = usersSearcher.Get();

            List<IUserInfo> userInfos = new List<IUserInfo>(users.Count);

            foreach (ManagementObject user in users)
            {
                UserInfo userInfo = new UserInfo
                {
                    Name = user["Name"].ToString(),
                    Description = user["Description"].ToString(),
                    Domain = user["Domain"].ToString(),
                    PasswordRequired = (bool)user["PasswordRequired"],
                    FullName = user["FullName"].ToString(),
                };

                userInfos.Add(userInfo);
            }

            return userInfos;
        }
    }
}