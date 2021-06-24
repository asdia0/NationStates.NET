namespace NationStates.NET
{
    /// <summary>
    /// Types of post statuses.
    /// </summary>
    public enum PostStatus
    {
        Regular = 0,

        /// <summary>
        /// Post was supressed by a regional officer.
        /// </summary>
        SupressedViewable = 1,

        Deleted = 2,

        /// <summary>
        /// Post was supressed by a moderator.
        /// </summary>
        SupressedUnviewable = 9,
    }
}
