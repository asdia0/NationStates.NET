namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using static Utility;

    /// <summary>
    /// Represents a historical census record.
    /// </summary>
    public struct CensusRecord
    {
        /// <summary>
        /// Gets the record's census ID.
        /// </summary>
        [JsonProperty]
        public int ID { get; }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Gets the value of the census data.
        /// </summary>
        [JsonProperty]
        public double Score { get; }

        /// <summary>
        /// Gets the time the census was recorded at.
        /// </summary>
        [JsonProperty]
        public DateTime Timestamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CensusRecord"/> struct.
        /// </summary>
        /// <param name="id">The record's census ID.</param>
        /// <param name="name">The name of the entity.</param>
        /// <param name="score">The value of the census data.</param>
        /// <param name="timestamp">The time the census was recorded at.</param>
        public CensusRecord(int id, string name, double score, DateTime timestamp)
        {
            this.ID = id;
            this.Name = name;
            this.Score = score;
            this.Timestamp = timestamp;
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