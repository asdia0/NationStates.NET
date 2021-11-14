namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a faction rank.
    /// </summary>
    public struct FactionRank
    {
        /// <summary>
        /// Gets the faction's ID.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets the faction's rank.
        /// </summary>
        [JsonProperty]
        public long Rank { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FactionRank"/> struct.
        /// </summary>
        /// <param name="id">The faction's ID.</param>
        /// <param name="rank">The faction's rank.</param>
        public FactionRank(long id, long rank)
        {
            this.ID = id;
            this.Rank = rank;
        }


        /// <summary>
        /// Gets a JSON string representing the rank.
        /// </summary>
        /// <returns>A JSON string representing the rank.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}
