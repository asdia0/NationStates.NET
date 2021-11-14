namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents an entity's Z-Day statistics.
    /// </summary>
    public struct ZDay
    {
        /// <summary>
        /// Gets the number of dead people in millions.
        /// </summary>
        [JsonProperty]
        public long Dead { get; }

        /// <summary>
        /// Gets the number of survivors in millions.
        /// </summary>
        [JsonProperty]
        public long Survivors { get; }

        /// <summary>
        /// Gets the number of zombies in millions.
        /// </summary>
        [JsonProperty]
        public long Zombies { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZDay"/> struct.
        /// </summary>
        /// <param name="survivors">Number of survivors in millions.</param>
        /// <param name="zombies">Number of zombies in millions.</param>
        /// <param name="dead">Number of dead people in millions.</param>
        public ZDay(long survivors, long zombies, long dead)
        {
            this.Survivors = survivors;
            this.Zombies = zombies;
            this.Dead = dead;
        }

        /// <summary>
        /// Gets a JSON string representing the statistics.
        /// </summary>
        /// <returns>A JSON string representing the statistics.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}