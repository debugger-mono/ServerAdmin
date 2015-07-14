using System;
using System.Security.Cryptography;
using System.Text;
using Tbl.Framework.Cryptography;

namespace Tbl.Framework.Licensing
{
    public sealed class License
    {
        public License(int productId, int productVersion, LicenseType type, LicenseCategory category, string registeredName, string registeredEmail, DateTime? startDate, DateTime? endDate)
        {
            this.ProductId = productId;
            this.ProductVersion = productVersion;
            this.Type = type;
            this.Category = category;
            this.RegisteredName = registeredName;
            this.RegisteredEmail = registeredEmail;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public int ProductId { get; set; }

        public int ProductVersion { get; set; }

        public LicenseType Type { get; set; }

        public LicenseCategory Category { get; set; }

        public string RegisteredName { get; set; }

        public string RegisteredEmail { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

#if DEBUG

        public static byte[] Encode(License license, string secretKey)
        {
            byte[] keyBytes;
            using (MD5Managed md5 = new MD5Managed())
            {
                keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
            }

            string licenceData = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}"
                , license.ProductId, license.ProductVersion, (int)license.Type
                , (int)license.Category, license.RegisteredName, license.RegisteredEmail
                , license.StartDate.HasValue ? license.StartDate.Value : DateTime.MinValue
                , license.EndDate.HasValue ? license.EndDate.Value : DateTime.MaxValue);

            using (TripleDESCryptoServiceProvider tripleDesCryptoProvider = new TripleDESCryptoServiceProvider())
            {
                tripleDesCryptoProvider.Key = keyBytes;
                tripleDesCryptoProvider.Mode = CipherMode.ECB;
                tripleDesCryptoProvider.Padding = PaddingMode.PKCS7;
                byte[] bytes = Encoding.UTF8.GetBytes(licenceData);

                using (ICryptoTransform cryptoTransform = tripleDesCryptoProvider.CreateEncryptor())
                {
                    byte[] transform = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                    return transform;
                }
            }
        }

        public static License Decode(byte[] bytes, string secretKey)
        {
            byte[] keyBytes;
            using (MD5Managed md5 = new MD5Managed())
            {
                keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(secretKey));
            }

            string licenceData = string.Empty;
            using (TripleDESCryptoServiceProvider tripleDesCryptoProvider = new TripleDESCryptoServiceProvider())
            {
                tripleDesCryptoProvider.Key = keyBytes;
                tripleDesCryptoProvider.Mode = CipherMode.ECB;
                tripleDesCryptoProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = tripleDesCryptoProvider.CreateDecryptor())
                {
                    byte[] transform = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
                    licenceData = Encoding.UTF8.GetString(transform);
                }
            }

            string[] data = licenceData.Split(new char[] { '|' });
            return new License(int.Parse(data[0]), int.Parse(data[1]), (LicenseType)Enum.Parse(typeof(LicenseType), data[2]), (LicenseCategory)Enum.Parse(typeof(LicenseCategory), data[3]),
                data[4], data[5], DateTime.Parse(data[6]), DateTime.Parse(data[7]));
        }

#endif
    }
}