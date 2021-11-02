namespace NationStates.NET
{
    using System;

    /// <summary>
    /// Defines an exception thrown in this project.
    /// </summary>
    public class NSError : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NSError"/> class.
        /// </summary>
        public NSError()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NSError"/> class.
        /// </summary>
        /// <param name="message">A message about the exception.</param>
        public NSError(string message)
            : base(message)
        { }
    }
}
