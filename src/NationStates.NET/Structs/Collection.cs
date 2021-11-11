namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using static Utility;

    /// <summary>
    /// Represents a collection of cards.
    /// </summary>
    public struct Collection
    {
        /// <summary>
        /// Gets the collection's ID.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets the time at which the collection was last updated.
        /// </summary>
        [JsonProperty]
        public DateTime LastUpdated { get; }

        /// <summary>
        /// Gets the name of the collection.
        /// </summary>
        [JsonProperty]
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collection"/> struct.
        /// </summary>
        /// <param name="id">The collection's ID.</param>
        /// <param name="lastUpdated">The time at which the collection was last updated.</param>
        /// <param name="name">The name of the collection.</param>
        public Collection(long id, DateTime lastUpdated, string name)
        {
            this.ID = id;
            this.LastUpdated = lastUpdated;
            this.Name = name;
        }

        /// <summary>
        /// Gets a JSON string representing the collection.
        /// </summary>
        /// <returns>A JSON string representing the collection.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}
