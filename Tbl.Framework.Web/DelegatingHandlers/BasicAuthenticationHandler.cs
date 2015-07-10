using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tbl.Framework.Web.DelegatingHandlers
{
    /// <summary>
    /// A message handler which adds basic authorisation credentials to a request
    /// </summary>
    public sealed class BasicAuthenticationHandler : DelegatingHandler
    {
        /// <summary>
        /// The username
        /// </summary>
        private string username;

        /// <summary>
        /// The password
        /// </summary>
        private string password;

        /// <summary>
        /// Constructor for instance
        /// </summary>
        /// <param name="innerHandler">The inner handler</param>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        public BasicAuthenticationHandler(HttpMessageHandler innerHandler, string username, string password)
            : base(innerHandler)
        {
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Overrides the SendAsync method, adding basic auth credentials
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var formattedAuthValue = string.Format("{0}:{1}", username, password);

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(formattedAuthValue)));

            return base.SendAsync(request, cancellationToken);
        }
    }
}