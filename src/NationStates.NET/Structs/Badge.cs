namespace NationStates.NET
{
    /// <summary>
    /// Represents a World Assembly badge.
    /// </summary>
    public struct Badge
    {
        /// <summary>
        /// Gets the ID of Security Council resolution that granted the World Assembly badge.
        /// </summary>
        public long ID { get; }

        /// <summary>
        /// Gets type of World Assembly badge.
        /// </summary>
        public BadgeType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Badge"/> struct.
        /// </summary>
        /// <param name="type">Type of World Assembly badge.</param>
        /// <param name="id">ID of Security Council resolution that granted the World Assembly badge.</param>
        public Badge(BadgeType type, long id)
        {
            this.Type = type;
            this.ID = id;
        }
    }
}