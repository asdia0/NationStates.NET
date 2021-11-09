namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using static Utility;

    /// <summary>
    /// Represents an event.
    /// </summary>
    public struct Event
    {
        /// <summary>
        /// Gets the description of the event.
        /// </summary>
        [JsonProperty]
        public string Text { get; }

        /// <summary>
        /// Gets the time at which the event occurred.
        /// </summary>
        [JsonProperty]
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

        /// <summary>
        /// Gets a JSON string representing the event.
        /// </summary>
        /// <returns>A JSON string representing the event.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}