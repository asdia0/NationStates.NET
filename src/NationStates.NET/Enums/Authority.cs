namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of authorities for regional officers.
    /// </summary>
    public enum Authority
    {
        /// <summary>
        /// Has executive powers.
        /// </summary>
        Executive,

        /// <summary>
        /// Has world assembly powers (WA Delegate).
        /// </summary>
        World_Assembly,

        /// <summary>
        /// Has appearance powers (factbook, tags, etc).
        /// </summary>
        Appearance,

        /// <summary>
        /// Has border control powers (password).
        /// </summary>
        Border_Control,

        /// <summary>
        /// Has communications powers (RMB).
        /// </summary>
        Communications,

        /// <summary>
        /// Has embassy powers.
        /// </summary>
        Embassies,

        /// <summary>
        /// Has poll powers.
        /// </summary>
        Polls,
    }
}