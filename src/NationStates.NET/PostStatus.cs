namespace NationStates.NET
{
    /// <summary>
    /// Types of post statuses.
    /// </summary>
    public enum PostStatus
    {
        Regular,

        /// <summary>
        /// Post was supressed by a regional officer.
        /// </summary>
        SupressedViewable,

        Deleted,

        /// <summary>
        /// Post was supressed by a moderator.
        /// </summary>
        SupressedUnviewable,
    }
}
