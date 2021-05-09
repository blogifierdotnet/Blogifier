using System;
using System.Diagnostics.CodeAnalysis;

namespace Blogifier.Shared.Exceptions
{
    /// <summary>
    /// Base class for handling business logic exception
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BusinessLogicException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public BusinessLogicException(string message) : base(message) { }

    }
}
