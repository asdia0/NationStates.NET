namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a nation during N-Day.
    /// </summary>
    public struct NDayNation
    {
        /// <summary>
        /// Gets the nation's stats.
        /// </summary>
        [JsonProperty]
        public NDay Stats { get; }

        /// <summary>
        /// Gets the faction the nation is in.
        /// </summary>
        [JsonProperty]
        public string? Faction { get; }

        /// <summary>
        /// Gets the nation's specialty.
        /// </summary>
        [JsonProperty]
        public Specialty Specialty { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NDayNation"/> struct.
        /// </summary>
        /// <param name="stats">The nation's stats.</param>
        /// <param name="faction">The faction the nation is in.</param>
        /// <param name="specialty">The nation's specialty.</param>
        public NDayNation(NDay stats, string? faction, Specialty specialty)
        {
            this.Stats = stats;
            this.Faction = faction;
            this.Specialty = specialty;
        }

        /// <summary>
        /// Gets a JSON string representing the nation during N-Day.
        /// </summary>
        /// <returns>A JSON string representing the nation during N-Day.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}
