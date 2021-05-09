using System;
using System.Diagnostics.CodeAnalysis;

namespace Blogifier.Shared.Exceptions
{
    /// <summary>
    /// Base class for handling server exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ServerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public ServerException(string message) : base(message)
        {
        }
    }
}
