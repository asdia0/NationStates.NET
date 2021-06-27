namespace NationStates.NET
{
    using System;

    public class NSError : Exception
    {
        public NSError()
        {

        }

        public NSError(string message) : base(message)
        {

        }

        public NSError(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
