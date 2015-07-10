using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Tbl.Framework.Xml;

namespace Tbl.Framework.Web.DelegatingHandlers
{
    /// <summary>
    /// A message handler which examines the content of an HttpRequest and adds an XmlSignature
    /// </summary>
    public sealed class SignedXmlHandler : DelegatingHandler
    {
        /// <summary>
        /// The X509Certificate
        /// </summary>
        private readonly X509Certificate2 certificate;

        /// <summary>
        /// The XML Processor, to sign the request
        /// </summary>
        private readonly IXmlSignatureProcessor xmlProcessor;

        /// <summary>
        /// Constructor for instance
        /// </summary>
        /// <param name="innerHandler">The inner handler</param>
        /// <param name="xmlProcessor">The xml signature processor</param>
        public SignedXmlHandler(HttpMessageHandler innerHandler, IXmlSignatureProcessor xmlProcessor, X509Certificate2 certificate) :
            base(innerHandler)
        {
            this.xmlProcessor = xmlProcessor;
            this.certificate = certificate;
        }

        /// <summary>
        /// Overrides the SendAsync method, signs the outgoing xml
        /// </summary>
        /// <param name="request">The http request message</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (Stream stream = request.Content.ReadAsStreamAsync().Result)
            {
                XmlDocument doc = this.xmlProcessor.SignXml(stream, this.certificate);
                XmlWriterSettings settings = new XmlWriterSettings { OmitXmlDeclaration = true };

                string message = string.Empty;
                using (StringWriter stringWriter = new StringWriter())
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    doc.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                    message = stringWriter.GetStringBuilder().ToString();
                }

                request.Content = new StringContent(message, Encoding.UTF8, "application/xml");
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
