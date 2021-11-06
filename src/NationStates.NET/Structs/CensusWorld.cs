namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a global census record.
    /// </summary>
    public struct CensusWorld
    {
        /// <summary>
        /// Gets the census ID.
        /// </summary>
        [JsonProperty]
        public int ID { get; }

        /// <summary>
        /// Gets the global average value of the census data.
        /// </summary>
        [JsonProperty]
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

        /// <summary>
        /// Gets a JSON string representing the record.
        /// </summary>
        /// <returns>A JSON string representing the record.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}