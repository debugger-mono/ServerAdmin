using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Tbl.Framework.Xml
{
    public interface IXmlSignatureProcessor
    {
        /// <summary>
        /// Signs the XML.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="certificate">The certificate.</param>
        /// <returns></returns>
        XmlDocument SignXml(Stream stream, X509Certificate2 certificate);

        /// <summary>
        /// Verifies the XML
        /// </summary>
        /// <param name="document">The document</param>
        /// <param name="certificate">The certificate</param>
        /// <returns>True if valid signature</returns>
        bool VerifySignature(XmlDocument document, X509Certificate2 certificate);
    }
}
