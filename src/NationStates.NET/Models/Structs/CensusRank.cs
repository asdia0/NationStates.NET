namespace NationStates.NET
{
    /// <summary>
    /// Represents a census ranking in a region.
    /// </summary>
    public struct CensusRank
    {
        /// <summary>
        /// Gets the name of the nation.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the nation's rank in the region.
        /// </summary>
        public long Rank { get; }

        /// <summary>
        /// Gets the nation's census value.
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusRank"/> struct.
        /// </summary>
        /// <param name="name">The name of the nation.</param>
        /// <param name="rank">The nation's rank in the region.</param>
        /// <param name="score">The nation's census value.</param>
        public CensusRank(string name, long rank, double score)
        {
            this.Name = name;
            this.Rank = rank;
            this.Score = score;
        }
    }
}
