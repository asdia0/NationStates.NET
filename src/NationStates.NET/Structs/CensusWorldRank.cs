namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a world census record.
    /// </summary>
    public struct CensusWorldRank
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        [JsonProperty]
        public int ID { get; }

        /// <summary>
        /// Gets the nation's name.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Gets the nation's rank.
        /// </summary>
        [JsonProperty]
        public long Rank { get; }

        /// <summary>
        /// Gets the nation's score.
        /// </summary>
        [JsonProperty]
        public double Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusWorldRank"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="name">The nation's name.</param>
        /// <param name="score">The nation's score.</param>
        /// <param name="rank">The nation's rank in the world.</param>
        public CensusWorldRank(int id, string name, double score, long rank)
        {
            this.ID = id;
            this.Name = name;
            this.Score = score;
            this.Rank = rank;
        }

        /// <summary>
        /// Gets a JSON string representing the record.
        /// </summary>
        /// <returns>A JSON string representing the record.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}