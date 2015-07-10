using System.Security.Cryptography.X509Certificates;

namespace Tbl.Framework.Web.Security
{
    /// <summary>
    /// ICertificateStore interface - Defines contract for implementing Certificate store
    /// </summary>
    public interface ICertificateStore
    {
        /// <summary>
        /// Gets the local certificate
        /// </summary>
        /// <param name="certificateNameOrSerial">The name of certificate file or certificate's serial number</param>
        /// <param name="certificatePassword">The certificate's password if loading from file</param>
        /// <returns>The local certificate</returns>
        X509Certificate2 GetLocalCertificate(string certificateNameOrSerial, string certificatePassword);
    }
}