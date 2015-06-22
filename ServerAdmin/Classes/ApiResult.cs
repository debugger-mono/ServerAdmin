using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace ServerAdmin.Classes
{
    /// <summary>
    /// A wrapping class which sits around an API Response - this will contain things like the build number etc. similar to that which is used for Nova
    /// </summary>
    public class ApiResult : HttpResponseMessage
    {
        /// <summary>
        /// API result message header
        /// </summary>
        private const string MessageHeaderName = "X-ApiResult-Message";

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResult"/> class.
        /// </summary>
        public ApiResult()
            : base(HttpStatusCode.OK)
        {
            this.Messages = new List<string>();
        }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public IEnumerable<string> Messages
        {
            get
            {
                IEnumerable<string> messages;

                if (this.Headers.TryGetValues(MessageHeaderName, out messages))
                {
                    return messages.Select(m => Uri.UnescapeDataString(m));
                }

                return Enumerable.Empty<string>();
            }

            set
            {
                this.Headers.Remove(MessageHeaderName);

                if (value != null)
                {
                    this.Headers.Add(MessageHeaderName, value.Select(m => Uri.EscapeDataString(m)));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has error; otherwise, <c>false</c>.
        /// </value>
        public bool Result
        {
            get
            {
                return this.IsSuccessStatusCode;
            }

            set
            {
                this.StatusCode = value ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            }
        }
    }
}