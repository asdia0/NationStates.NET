namespace NationStates.NET
{
    /// <summary>
    /// Types of <see cref="Event"/>s.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// Law implemented.
        /// </summary>
        Law,

        /// <summary>
        /// Nation property changed.
        /// </summary>
        Change,

        /// <summary>
        /// Dispatch created/edited.
        /// </summary>
        Dispatch,

        /// <summary>
        /// RMB post created.
        /// </summary>
        RMB,

        /// <summary>
        /// Embassy initiated/rejected/constructed/closed.
        /// </summary>
        Embassy,

        /// <summary>
        /// Nation ejected from region.
        /// </summary>
        Eject,

        /// <summary>
        /// Region property changed.
        /// </summary>
        Admin,

        /// <summary>
        /// Nation moved regions.
        /// </summary>
        Move,

        /// <summary>
        /// Nation founded/refounded.
        /// </summary>
        Founding,

        /// <summary>
        /// Nation ceased-to-exist.
        /// </summary>
        CTE,

        /// <summary>
        /// Nation voted on a World Assembly bill.
        /// </summary>
        Vote,

        /// <summary>
        /// Resolution approved.
        /// </summary>
        Resolution,

        /// <summary>
        /// Nation became a member of the World Assembly/withdrew from the World Assembly.
        /// </summary>
        Member,

        /// <summary>
        /// Nation was endorsed.
        /// </summary>
        Endo,
    }
}