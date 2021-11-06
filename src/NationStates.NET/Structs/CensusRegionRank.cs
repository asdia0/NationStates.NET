namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a census ranking in a region.
    /// </summary>
    public struct CensusRegionRank
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        [JsonProperty]
        public int ID { get; }

        /// <summary>
        /// Gets the name of the nation.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Gets the nation's rank in the region.
        /// </summary>
        [JsonProperty]
        public long Rank { get; }

        /// <summary>
        /// Gets the nation's census value.
        /// </summary>
        [JsonProperty]
        public double Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusRegionRank"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="name">The name of the nation.</param>
        /// <param name="rank">The nation's rank in the region.</param>
        /// <param name="score">The nation's census value.</param>
        public CensusRegionRank(int id, string name, long rank, double score)
        {
            this.ID = id;
            this.Name = name;
            this.Rank = rank;
            this.Score = score;
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