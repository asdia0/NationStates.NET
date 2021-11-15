namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a regional census record.
    /// </summary>
    public struct CensusRegion
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        [JsonProperty]
        public int ID { get; }

        /// <summary>
        /// Gets the name of the region.
        /// </summary>
        [JsonProperty]
        public string Region { get; }

        /// <summary>
        /// Gets the value of the census data.
        /// </summary>
        [JsonProperty]
        public double Score { get; }

        /// <summary>
        /// Gets the region's world rank as a percentage.
        /// </summary>
        [JsonProperty]
        public double WorldPercentage { get; }

        /// <summary>
        /// Gets the region's world rank.
        /// </summary>
        [JsonProperty]
        public long WorldRank { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusRegion"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="region">The name of the region.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The region's world rank.</param>
        /// <param name="worldPercentage">The region's world rank as a percentage.</param>
        public CensusRegion(int id, string region, double score, long worldRank, double worldPercentage)
        {
            this.ID = id;
            this.Region = region;
            this.Score = score;
            this.WorldRank = worldRank;
            this.WorldPercentage = worldPercentage;
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