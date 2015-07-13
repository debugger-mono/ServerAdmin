using System.Collections.Generic;
using Tbl.ServerAdmin.Providers.Linux.Users;
using Tbl.ServerAdmin.Providers.Users;
using Xunit;

namespace Tbl.ServerAdmin.Providers.Linux.Tests.Users.LinuxUserInfoProviderTests
{
    public class GetUsersTests
    {
        [Fact]
        public void ShouldReturnUsers()
        {
            // Arrange
            LinuxUserInfoProvider provider = new LinuxUserInfoProvider();

            // Act
            List<IUserInfo> userInfos = provider.GetUsers();

            // Assert
            Assert.NotEmpty(userInfos);
            Assert.True(userInfos.Count > 0);
        }
    }
}
