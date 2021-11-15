namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a census rank.
    /// </summary>
    public struct CensusRank
    {
        /// <summary>
        /// Gets the entity's name.
        /// </summary>
        [JsonProperty]
        public string Entity { get; }

        /// <summary>
        /// Gets the census ID.
        /// </summary>
        [JsonProperty]
        public int ID { get; }

        /// <summary>
        /// Gets the entity's rank.
        /// </summary>
        [JsonProperty]
        public long Rank { get; }

        /// <summary>
        /// Gets the entity's score.
        /// </summary>
        [JsonProperty]
        public double Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusRank"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="entity">The entity's name.</param>
        /// <param name="score">The entity's score.</param>
        /// <param name="rank">The entity's rank.</param>
        public CensusRank(int id, string entity, double score, long rank)
        {
            this.ID = id;
            this.Entity = entity;
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