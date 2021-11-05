namespace NationStates.NET
{
    /// <summary>
    /// Defines a World Assembly badge.
    /// </summary>
    public struct WABadge
    {
        /// <summary>
        /// Gets the ID of Security Council resolution that granted the World Assembly badge.
        /// </summary>
        public long ID { get; }

        /// <summary>
        /// Gets type of World Assembly badge.
        /// </summary>
        public WABadgeType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WABadge"/> struct.
        /// </summary>
        /// <param name="type">Type of World Assembly badge.</param>
        /// <param name="id">ID of Security Council resolution that granted the World Assembly badge.</param>
        public WABadge(WABadgeType type, long id)
        {
            this.Type = type;
            this.ID = id;
        }
    }
}