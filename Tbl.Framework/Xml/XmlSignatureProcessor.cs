using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Tbl.Framework.Xml
{
    public sealed class XmlSignatureProcessor : IXmlSignatureProcessor
    {
        /// <summary>
        /// Signs the XML.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="certificate">The certificate.</param>
        /// <returns></returns>
        public XmlDocument SignXml(Stream stream, X509Certificate2 certificate)
        {
            if (certificate != null && !certificate.HasPrivateKey)
            {
                throw new ArgumentException(
                    "The certificate does not have a private key and can't be used for signing.");
            }

            // Create a new XML document.
            XmlDocument doc = new XmlDocument();

            // Format the document to ignore white spaces.
            doc.PreserveWhitespace = false;

            // Load the passed XML file using it's name.
            doc.Load(stream);

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(doc);

            // Add the key to the SignedXml document. 
            signedXml.SigningKey = certificate.PrivateKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            string id = doc.DocumentElement.GetAttribute("ID");
            reference.Uri = !string.IsNullOrEmpty(id) ? string.Concat("#", id) : string.Empty;

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Create a new KeyInfo object.
            KeyInfo keyInfo = new KeyInfo();

            //// Load the certificate into a KeyInfoX509Data object
            //// and add it to the KeyInfo object.
            //// Create an X509IssuerSerial object and add it to the
            //// KeyInfoX509Data object.

            KeyInfoX509Data kdata = new KeyInfoX509Data(certificate);

            X509IssuerSerial xserial = new X509IssuerSerial();

            xserial.IssuerName = certificate.IssuerName.Name;
            xserial.SerialNumber = certificate.SerialNumber;

            kdata.AddIssuerSerial(xserial.IssuerName, xserial.SerialNumber);

            keyInfo.AddClause(kdata);

            // Add the KeyInfo object to the SignedXml object.
            signedXml.KeyInfo = keyInfo;

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));

            if (doc.FirstChild is XmlDeclaration)
            {
                doc.RemoveChild(doc.FirstChild);
            }

            return doc;

        }

        /// <summary>
        /// Verifies the XML
        /// </summary>
        /// <param name="document">The document</param>
        /// <param name="certificate">The certificate</param>
        /// <returns>True if valid signature</returns>
        public bool VerifySignature(XmlDocument document, X509Certificate2 certificate)
        {
            if (document == null || certificate == null)
            {
                return false;
            }

            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = document.GetElementsByTagName("Signature", "http://www.w3.org/2000/09/xmldsig#");
            if (nodeList.Count > 0)
            {
                // Create a new SignedXml object and pass it
                // the XML document class.
                SignedXml signedXml = new SignedXml(document);

                // Load the signature node.
                signedXml.LoadXml((XmlElement)nodeList[0]);

                // Check the signature and return the result.
                return signedXml.CheckSignature(certificate, true);
            }

            return false;
        }
    }
}
