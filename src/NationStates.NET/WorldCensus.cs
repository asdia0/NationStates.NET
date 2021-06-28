namespace NationStates.NET
{
    /// <summary>
    /// Defines a world census record.
    /// </summary>
    public struct WorldCensus
    {
        /// <summary>
        /// The census ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// The nation's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The nation's score.
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldCensus"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="name">The nation's name.</param>
        /// <param name="score">The nation's score.</param>
        public WorldCensus(int id, string name, double score)
        {
            this.ID = id;
            this.Name = name;
            this.Score = score;
        }
    }
}
