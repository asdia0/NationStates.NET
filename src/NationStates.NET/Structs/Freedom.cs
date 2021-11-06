namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a nation's freedom levels.
    /// </summary>
    public struct Freedom
    {
        /// <summary>
        /// Gets the nation's civil rights level.
        /// </summary>
        [JsonProperty]
        public CivilRights CivilRights { get; }

        /// <summary>
        /// Gets the nation's economy level.
        /// </summary>
        [JsonProperty]
        public Economy Economy { get; }

        /// <summary>
        /// Gets the nation's political freedoms level.
        /// </summary>
        [JsonProperty]
        public PoliticalFreedoms PoliticalFreedoms { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Freedom"/> struct.
        /// </summary>
        /// <param name="civilRights">Civil rights level.</param>
        /// <param name="economy">Economy level.</param>
        /// <param name="politicalFreedoms">Political freedoms level.</param>
        public Freedom(CivilRights civilRights, Economy economy, PoliticalFreedoms politicalFreedoms)
        {
            this.CivilRights = civilRights;
            this.Economy = economy;
            this.PoliticalFreedoms = politicalFreedoms;
        }

        /// <summary>
        /// Gets a JSON string representing the nation's freedom levels.
        /// </summary>
        /// <returns>A JSON string representing the nation's freedom levels.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}