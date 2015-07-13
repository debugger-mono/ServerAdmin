using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tbl.ServerAdmin.Providers.Users;

namespace Tbl.ServerAdmin.Providers.Linux.Users
{
    public class LinuxUserInfoProvider : IUserInfoProvider
    {
        public List<IUserInfo> GetUsers()
        {
            string contents = this.PasswdFile();
            string[] rows = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<IUserInfo> users = rows.Select(r => this.ParseRow(r)).ToList();
            return users;
        }

        private string PasswdFile()
        {
            return File.ReadAllText(@"/etc/passwd");
        }

        private IUserInfo ParseRow(string row)
        {
            string[] item = row.Split(new char[] { ':' });

            return new UserInfo
            {
                Name = item[0],
                UserId = Convert.ToInt32(item[2]),
                GroupId = Convert.ToInt32(item[3]),
                FullName = item[4]
            };
        }
    }
}
