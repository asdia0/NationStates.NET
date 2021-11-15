namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a World Assembly badge.
    /// </summary>
    public struct Badge
    {
        /// <summary>
        /// Gets the name of the entity that has the badge.
        /// </summary>
        [JsonProperty]
        public string Entity { get; }

        /// <summary>
        /// Gets the ID of Security Council resolution that granted the World Assembly badge.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets type of World Assembly badge.
        /// </summary>
        [JsonProperty]
        public BadgeType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Badge"/> struct.
        /// </summary>
        /// <param name="entity">The name of the entity that has the badge.</param>
        /// <param name="type">Type of World Assembly badge.</param>
        /// <param name="id">ID of Security Council resolution that granted the World Assembly badge.</param>
        public Badge(string entity, BadgeType type, long id)
        {
            this.Entity = entity;
            this.Type = type;
            this.ID = id;
        }

        /// <summary>
        /// Gets a JSON string representing the badge.
        /// </summary>
        /// <returns>A JSON string representing the badge.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}