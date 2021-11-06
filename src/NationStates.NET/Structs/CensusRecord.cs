namespace NationStates.NET
{
    using System;

    /// <summary>
    /// Represents a historical census record.
    /// </summary>
    public struct CensusRecord
    {
        /// <summary>
        /// Gets the record's census ID.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the census data.
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Gets the time the census was recorded at.
        /// </summary>
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
    }
}