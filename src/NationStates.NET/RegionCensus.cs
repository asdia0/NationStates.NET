namespace NationStates.NET
{
    /// <summary>
    /// Represents a census record.
    /// </summary>
    public class RegionCensus
    {
        /// <summary>
        /// Gets or sets the census ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the value of the census data.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets the nation's world rank.
        /// </summary>
        public long WorldRank { get; set; }

        /// <summary>
        /// Gets or sets the nation's world rank as a percentage.
        /// </summary>
        public double WorldPercentage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionCensus"/> class.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The nation's world rank.</param>
        /// <param name="worldPercentage">The nation's world rank as a percentage.</param>
        public RegionCensus(int id, double score, long worldRank, double worldPercentage)
        {
            this.ID = id;
            this.Score = score;
            this.WorldRank = worldRank;
            this.WorldPercentage = worldPercentage;
        }
    }
}
