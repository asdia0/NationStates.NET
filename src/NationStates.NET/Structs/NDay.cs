namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents an entity's N-Day statistics.
    /// </summary>
    public struct NDay
    {
        /// <summary>
        /// Gets the current amount of nukes incoming towards the entity.
        /// </summary>
        [JsonProperty]
        public long Incoming { get; }

        /// <summary>
        /// Gets the number of nukes the entity has intercepted.
        /// </summary>
        [JsonProperty]
        public long Intercepts { get; }

        /// <summary>
        /// Gets the current amount of nukes launched by the entity.
        /// </summary>
        [JsonProperty]
        public long Launches { get; }

        /// <summary>
        /// Gets the amount of nukes the entity has in total.
        /// </summary>
        [JsonProperty]
        public long Nukes { get; }

        /// <summary>
        /// Gets the amount of production points the entity has in total.
        /// </summary>
        [JsonProperty]
        public long Production { get; }

        /// <summary>
        /// Gets the total number of radiation the entity has received.
        /// </summary>
        [JsonProperty]
        public long Radiation { get; }

        /// <summary>
        /// Gets the amount of shields the entity has in total.
        /// </summary>
        [JsonProperty]
        public long Shields { get; }

        /// <summary>
        /// Gets the total number of radiation imposed onto other entities.
        /// </summary>
        [JsonProperty]
        public long Strikes { get; }

        /// <summary>
        /// Gets the number of nukes targeted towards the entity.
        /// </summary>
        [JsonProperty]
        public long Targeted { get; }

        /// <summary>
        /// Gets the amount of targets the entity has in total.
        /// </summary>
        [JsonProperty]
        public long Targets { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NDay"/> struct.
        /// </summary>
        /// <param name="incoming">The current amount of nukes incoming towards the entity.</param>
        /// <param name="intercepts">The number of nukes the entity has intercepted.</param>
        /// <param name="launches">THe current amount of nukes launched by the entity.</param>
        /// <param name="nukes">The amount of nukes the entity has in total.</param>
        /// <param name="production">The amount of production points the entity has in total.</param>
        /// <param name="radiation">The total number of radiation the entity has received.</param>
        /// <param name="shields">The amount of shields the entity has in total.</param>
        /// <param name="strikes">The total number of radiation imposed onto other entities.</param>
        /// <param name="targeted">The number of nukes targeted towards the entity.</param>
        /// <param name="targets">The amount of targets the entity has in total.</param>
        public NDay(long incoming, long intercepts, long launches, long nukes, long production, long radiation, long shields, long strikes, long targeted, long targets)
        {
            this.Incoming = incoming;
            this.Intercepts = intercepts;
            this.Launches = launches;
            this.Nukes = nukes;
            this.Production = production;
            this.Radiation = radiation;
            this.Shields = shields;
            this.Strikes = strikes;
            this.Targeted = targeted;
            this.Targets = targets;
        }

        /// <summary>
        /// Gets a JSON string representing the faction.
        /// </summary>
        /// <returns>A JSON string representing the faction.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}