namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a national census record.
    /// </summary>
    public struct CensusNation
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
        public string Nation { get; }

        /// <summary>
        /// Gets the nation's regional rank as a percentage.
        /// </summary>
        [JsonProperty]
        public double RegionPercentage { get; }

        /// <summary>
        /// Gets the nation's regional rank.
        /// </summary>
        [JsonProperty]
        public long RegionRank { get; }

        /// <summary>
        /// Gets the value of the census data.
        /// </summary>
        [JsonProperty]
        public double Score { get; }

        /// <summary>
        /// Gets the nation's world rank as a percentage.
        /// </summary>
        [JsonProperty]
        public double WorldPercentage { get; }

        /// <summary>
        /// Gets the nation's world rank.
        /// </summary>
        [JsonProperty]
        public long WorldRank { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusNation"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="nation">The name of the nation.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The nation's world rank.</param>
        /// <param name="regionRank">The nation's regional rank.</param>
        /// <param name="worldPercentage">The nation's world rank as a percentage.</param>
        /// <param name="regionPercentage">The nation's regional rank as a percentage.</param>
        public CensusNation(int id, string nation, double score, long worldRank, long regionRank, double worldPercentage, double regionPercentage)
        {
            this.ID = id;
            this.Nation = nation;
            this.Score = score;
            this.WorldRank = worldRank;
            this.RegionRank = regionRank;
            this.WorldPercentage = worldPercentage;
            this.RegionPercentage = regionPercentage;
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