namespace NationStates.NET.Nation
{
    using System;

    /// <summary>
    /// Represents an event.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Time at which the event occurred.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Event"/> class.
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
