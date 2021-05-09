using System;
using System.Diagnostics.CodeAnalysis;

namespace Blogifier.Shared.Exceptions
{
    /// <summary>
    ///  Base class for handling error, 
    ///  when server cannot produce a response matching the list of acceptable values
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NotAcceptableException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotAcceptableException"/> class.
        /// </summary>
        public NotAcceptableException() : this("Not Acceptable") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotAcceptableException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public NotAcceptableException(string message) : base(message) { }

    }
}
