using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Blogifier.Shared.Exceptions
{
    /// <summary>
    /// Base class for handling exception using HttpStatusCode
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SpecifyHttpStatusCodeException : Exception
    {
        /// <summary>
        /// Base class status code
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LocusEmergencyHttpException"/> class.
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        public SpecifyHttpStatusCodeException(HttpStatusCode httpStatusCode, string message) : base(message)
            => HttpStatusCode = httpStatusCode;
    }
}
