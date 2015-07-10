using System;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Tbl.Framework.Configuration;

namespace Tbl.Framework.Web.Security
{
    /// <summary>
    /// Stub implementation of certificate store for public api
    /// </summary>
    public sealed class CertificateStore : ICertificateStore
    {
        /// <summary>
        /// The http context
        /// </summary>
        private readonly HttpContextBase httpContext;

        /// <summary>
        /// The configuration manager
        /// </summary>
        private readonly IConfigManager configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateStore" /> class.
        /// </summary>
        /// <param name="httpContext">The http context</param>
        /// <param name="configManager">The config manager</param>
        public CertificateStore(HttpContextBase httpContext, IConfigManager configManager)
        {
            this.httpContext = httpContext;
            this.configManager = configManager;
        }

        /// <summary>
        /// Gets the local certificate
        /// </summary>
        /// <param name="certificateNameOrSerial">The name of certificate file or certificate's serial number</param>
        /// <param name="certificatePassword">The certificate's password if loading from file</param>
        /// <returns>The local certificate</returns>
        public X509Certificate2 GetLocalCertificate(string certificateNameOrSerial, string certificatePassword)
        {
            X509Certificate2 certificate = null;
            if (certificateNameOrSerial.EndsWith(".pfx"))
            {
                // A file certificate
                certificate = new X509Certificate2(this.httpContext.Server.MapPath(certificateNameOrSerial), certificatePassword);
            }
            else
            {
                // A certificate serial
                X509Store store = new X509Store(StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySerialNumber, certificateNameOrSerial, false);
                if (certs.Count > 0)
                {
                    certificate = certs[0];
                }

                store.Close();
            }

            if (certificate == null)
            {
                throw new Exception(string.Format("Couldn't find certificate: {0}", certificateNameOrSerial));
            }

            return certificate;
        }
    }
}