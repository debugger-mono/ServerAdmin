using System;
using Tbl.Framework.Cryptography;
using Tbl.Framework.Licensing;
using Xunit;

namespace Tbl.Framework.Tests.Licensing
{
    public class LicenseTests
    {

        [Fact]
        public void ShouldEncodeDecodeLicenceCorrectly()
        {
            // Arrange
            License license = new License(1, 4, LicenseType.TimeLimited, LicenseCategory.IndividualCommercial, "Holder Name", "user@FakeEmail.com", DateTime.Now, DateTime.Now.AddDays(90));
            string secretKey = "WUM6KL97VLEDNCFV5Z8FYWK2G6YG4GG3YZBKXC57AV6S492MRACM3";

            ICryptoService cryptoService = new CryptoService();

            // Act
            byte[] encodedLicense = License.Encode(license, secretKey);

            License decodedLicence = License.Decode(encodedLicense, secretKey);

            // Assert
            Assert.Equal(license.ProductId, decodedLicence.ProductId);
            Assert.Equal(license.ProductVersion, decodedLicence.ProductVersion);
            Assert.Equal(license.Type, decodedLicence.Type);
            Assert.Equal(license.Category, decodedLicence.Category);
            Assert.Equal(license.RegisteredName, decodedLicence.RegisteredName);
            Assert.Equal(license.RegisteredEmail, decodedLicence.RegisteredEmail);
            Assert.NotStrictEqual(license.StartDate, decodedLicence.StartDate);
            Assert.NotStrictEqual(license.EndDate, decodedLicence.EndDate);
        }
    }
}