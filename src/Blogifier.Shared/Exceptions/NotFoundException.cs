using System;
using System.Diagnostics.CodeAnalysis;

namespace Blogifier.Shared.Exceptions
{
    /// <summary>
    ///  Base class for handling error,
    ///  when server can't find the requested resource
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NotFoundExсeption : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundExсeption"/> class.
        /// </summary>
        public NotFoundExсeption() : this("Not Found") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundExсeption"/> class.
        /// </summary>
        /// <param name="message"></param>
        public NotFoundExсeption(string message) : base(message) { }
    }
}
