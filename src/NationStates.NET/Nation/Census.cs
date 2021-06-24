namespace NationStates.NET.Nation
{
    /// <summary>
    /// Represents a census record.
    /// </summary>
    public class Census
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
        /// The nation's regional rank.
        /// </summary>
        public long RegionRank { get; set; }

        /// <summary>
        /// The nation's world rank as a percentage.
        /// </summary>
        public double WorldPercentage { get; set; }

        /// <summary>
        /// The nation's regional rank as a percentage.
        /// </summary>
        public double RegionPercentage { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Census"/> class.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The nation's world rank.</param>
        /// <param name="regionRank">The nation's regional rank.</param>
        /// <param name="worldPercentage">The nation's world rank as a percentage.</param>
        /// <param name="regionPercentage">The nation's regional rank as a percentage.</param>
        public Census(int id, double score, long worldRank, long regionRank, double worldPercentage, double regionPercentage)
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
