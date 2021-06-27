namespace NationStates.NET
{
    /// <summary>
    /// Represents a World Assembly badge.
    /// </summary>
    public class WABadge
    {
        /// <summary>
        /// Gets or sets type of World Assembly badge.
        /// </summary>
        public WABadgeType Type { get; set; }

        /// <summary>
        /// Gets or sets the ID of Security Council resolution that granted the World Assembly badge.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WABadge"/> class.
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
