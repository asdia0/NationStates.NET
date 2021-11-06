namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of World Assembly memberships.
    /// </summary>
    public enum WAStatus
    {
        /// <summary>
        /// Nation is not a member of the World Assembly.
        /// </summary>
        NonMember,

        /// <summary>
        /// Nation is a member of the World Assembly.
        /// </summary>
        Member,

        /// <summary>
        /// Nation is a World Assembly Delegate,
        /// </summary>
        Delegate,
    }
}