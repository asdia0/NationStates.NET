namespace NationStates.NET
{
    /// <summary>
    /// Represents a national census record.
    /// </summary>
    public class NationCensus
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
        /// Gets or sets the nation's regional rank.
        /// </summary>
        public long RegionRank { get; set; }

        /// <summary>
        /// Gets or sets the nation's world rank as a percentage.
        /// </summary>
        public double WorldPercentage { get; set; }

        /// <summary>
        /// Gets or sets the nation's regional rank as a percentage.
        /// </summary>
        public double RegionPercentage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationCensus"/> class.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The nation's world rank.</param>
        /// <param name="regionRank">The nation's regional rank.</param>
        /// <param name="worldPercentage">The nation's world rank as a percentage.</param>
        /// <param name="regionPercentage">The nation's regional rank as a percentage.</param>
        public NationCensus(int id, double score, long worldRank, long regionRank, double worldPercentage, double regionPercentage)
        {
            this.ID = id;
            this.Score = score;
            this.WorldRank = worldRank;
            this.RegionRank = regionRank;
            this.WorldPercentage = worldPercentage;
            this.RegionPercentage = regionPercentage;
        }
    }
}
