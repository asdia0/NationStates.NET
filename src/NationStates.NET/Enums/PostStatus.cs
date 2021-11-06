namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of post statuses.
    /// </summary>
    public enum PostStatus
    {
        /// <summary>
        /// Regular post.
        /// </summary>
        Regular,

        /// <summary>
        /// Post was supressed by a regional officer.
        /// </summary>
        SupressedViewable,

        /// <summary>
        /// Post was deleted by the poster.
        /// </summary>
        Deleted,

        /// <summary>
        /// Post was supressed by a moderator.
        /// </summary>
        SupressedUnviewable,
    }
}