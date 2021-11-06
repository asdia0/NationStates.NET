namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a cause of death.
    /// </summary>
    public struct Death
    {
        /// <summary>
        /// Gets the cause of death.
        /// </summary>
        [JsonProperty]
        public string Cause { get; }

        /// <summary>
        /// Gets the death's national frequency in percentage.
        /// </summary>
        [JsonProperty]
        public double Frequency { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Death"/> struct.
        /// </summary>
        /// <param name="cause">Cause of death.</param>
        /// <param name="frequency">Frequency in percentage.</param>
        public Death(string cause, double frequency)
        {
            this.Cause = cause;
            this.Frequency = frequency;
        }

        /// <summary>
        /// Gets a JSON string representing the cause of death.
        /// </summary>
        /// <returns>A JSON string representing the cause of death.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}