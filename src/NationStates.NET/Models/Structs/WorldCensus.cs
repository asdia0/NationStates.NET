namespace NationStates.NET
{
    /// <summary>
    /// Defines a world census record.
    /// </summary>
    public struct WorldCensus
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the nation's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the nation's score.
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Gets the nation's rank.
        /// </summary>
        public long Rank { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldCensus"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="name">The nation's name.</param>
        /// <param name="score">The nation's score.</param>
        /// <param name="rank">The nation's rank in the world.</param>
        public WorldCensus(int id, string name, double score, long rank)
        {
            this.ID = id;
            this.Name = name;
            this.Score = score;
            this.Rank = rank;
        }
    }
}