namespace NationStates.NET
{
    /// <summary>
    /// Represents a census record.
    /// </summary>
    public class RegionCensus
    {
        /// <summary>
        /// The census ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The value of the census data.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The nation's world rank.
        /// </summary>
        public long WorldRank { get; set; }

        /// <summary>
        /// The nation's world rank as a percentage.
        /// </summary>
        public double WorldPercentage { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="NationCensus"/> class.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The nation's world rank.</param>
        /// <param name="regionRank">The nation's regional rank.</param>
        /// <param name="worldPercentage">The nation's world rank as a percentage.</param>
        /// <param name="regionPercentage">The nation's regional rank as a percentage.</param>
        public RegionCensus(int id, double score, long worldRank, double worldPercentage)
        {
            this.ID = id;
            this.Score = score;
            this.WorldRank = worldRank;
            this.WorldPercentage = worldPercentage;
        }
    }
}
