namespace NationStates.NET
{
    /// <summary>
    /// Defines a census record.
    /// </summary>
    public struct RegionCensus
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the value of the census data.
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Gets the region's world rank.
        /// </summary>
        public long WorldRank { get; }

        /// <summary>
        /// Gets the region's world rank as a percentage.
        /// </summary>
        public double WorldPercentage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionCensus"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The region's world rank.</param>
        /// <param name="worldPercentage">The region's world rank as a percentage.</param>
        public RegionCensus(int id, double score, long worldRank, double worldPercentage)
        {
            this.ID = id;
            this.Score = score;
            this.WorldRank = worldRank;
            this.WorldPercentage = worldPercentage;
        }
    }
}
