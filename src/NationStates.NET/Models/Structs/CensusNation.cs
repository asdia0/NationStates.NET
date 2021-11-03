namespace NationStates.NET
{
    /// <summary>
    /// Defines a national census record.
    /// </summary>
    public struct CensusNation
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the name of the nation.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the census data.
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Gets the nation's world rank.
        /// </summary>
        public long WorldRank { get; }

        /// <summary>
        /// Gets the nation's regional rank.
        /// </summary>
        public long RegionRank { get; }

        /// <summary>
        /// Gets the nation's world rank as a percentage.
        /// </summary>
        public double WorldPercentage { get; }

        /// <summary>
        /// Gets the nation's regional rank as a percentage.
        /// </summary>
        public double RegionPercentage { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusNation"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="name">The name of the nation.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The nation's world rank.</param>
        /// <param name="regionRank">The nation's regional rank.</param>
        /// <param name="worldPercentage">The nation's world rank as a percentage.</param>
        /// <param name="regionPercentage">The nation's regional rank as a percentage.</param>
        public CensusNation(int id, string name, double score, long worldRank, long regionRank, double worldPercentage, double regionPercentage)
        {
            this.ID = id;
            this.Name = name;
            this.Score = score;
            this.WorldRank = worldRank;
            this.RegionRank = regionRank;
            this.WorldPercentage = worldPercentage;
            this.RegionPercentage = regionPercentage;
        }
    }
}
