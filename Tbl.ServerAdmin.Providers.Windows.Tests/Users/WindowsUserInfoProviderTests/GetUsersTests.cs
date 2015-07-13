using System.Collections.Generic;
using Tbl.ServerAdmin.Providers.Users;
using Tbl.ServerAdmin.Providers.Windows.Users;
using Xunit;

namespace Tbl.ServerAdmin.Providers.Windows.Tests.Users.WindowsUserInfoProviderTests
{
    public class GetUsersTests
    {
        [Fact]
        public void ShouldReturnUsers()
        {
            // Arrange
            WindowsUserInfoProvider provider = new WindowsUserInfoProvider();

            // Act
            List<IUserInfo> userInfos = provider.GetUsers();

            // Assert
            Assert.NotEmpty(userInfos);
            Assert.True(userInfos.Count > 0);
        }
    }
}