namespace NationStates.NET
{
    /// <summary>
    /// Defines a regional census record.
    /// </summary>
    public struct CensusRegion
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the name of the region.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the census data.
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Gets the region's world rank as a percentage.
        /// </summary>
        public double WorldPercentage { get; }

        /// <summary>
        /// Gets the region's world rank.
        /// </summary>
        public long WorldRank { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusRegion"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="name">The name of the region.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="worldRank">The region's world rank.</param>
        /// <param name="worldPercentage">The region's world rank as a percentage.</param>
        public CensusRegion(int id, string name, double score, long worldRank, double worldPercentage)
        {
            this.ID = id;
            this.Name = name;
            this.Score = score;
            this.WorldRank = worldRank;
            this.WorldPercentage = worldPercentage;
        }
    }
}