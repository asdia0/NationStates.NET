namespace NationStates.NET
{
    using System;

    /// <summary>
    /// Defines an event.
    /// </summary>
    public struct Event
    {
        /// <summary>
        /// Gets the description of the event.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the time at which the event occurred.
        /// </summary>
        public DateTime TimeStamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> struct.
        /// </summary>
        /// <param name="timestamp">Time at which the event occurred.</param>
        /// <param name="text">Description of the event.</param>
        public Event(DateTime timestamp, string text)
        {
            this.TimeStamp = timestamp;
            this.Text = text;
        }
    }
}