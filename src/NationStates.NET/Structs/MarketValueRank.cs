namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a card value rank.
    /// </summary>
    public struct MarketValueRank
    {
        /// <summary>
        /// Gets the card's ID.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets the card's season.
        /// </summary>
        [JsonProperty]
        public int Season { get; }

        /// <summary>
        /// Gets the card's rank.
        /// </summary>
        [JsonProperty]
        public long Rank { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketValueRank"/> struct.
        /// </summary>
        /// <param name="id">The card's ID.</param>
        /// <param name="season">The card's season.</param>
        /// <param name="rank">The card's rank.</param>
        public MarketValueRank(long id, int season, long rank)
        {
            this.ID = id;
            this.Season = season;
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