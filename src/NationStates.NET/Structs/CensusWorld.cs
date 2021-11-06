namespace NationStates.NET
{
    /// <summary>
    /// Represents a global census record.
    /// </summary>
    public struct CensusWorld
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the global average value of the census data.
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusWorld"/> struct.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="score">The global average value of the census data.</param>
        public CensusWorld(int id, double score)
        {
            this.ID = id;
            this.Score = score;
        }
    }
}