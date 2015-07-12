using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tbl.ServerAdmin.Providers.Users;
using Tbl.ServerAdmin.Providers.Windows.Users;

namespace Tbl.ServerAdmin.Providers.Windows.Tests.Users.WindowsUserInfoProviderTests
{
    [TestClass]
    public class GetUsersTests
    {
        [TestMethod]
        public void ShouldReturnUsers()
        {
            // Arrange
            WindowsUserInfoProvider provider = new WindowsUserInfoProvider();

            // Act
            List<IUserInfo> userInfos = provider.GetUsers();

            // Assert
            Assert.IsNotNull(userInfos);
            Assert.IsTrue(userInfos.Count > 0);
        }
    }
}
