namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of Security Council proposal categories.
    /// </summary>
    public enum SCCategory
    {
        /// <summary>
        /// Remove a resolution from internatinal law.
        /// </summary>
        Repeal,

        /// <summary>
        /// Recognizes outstanding contribution by a nation or region.
        /// </summary>
        Commendation,

        /// <summary>
        /// Expresses shock and dismay at a nation or region.
        /// </summary>
        Condemnation,

        /// <summary>
        /// Expresses a position on international affairs and obligations.
        /// </summary>
        Declaration,

        /// <summary>
        /// Strikes down Delegate-imposed barriers to free entry in a region.
        /// </summary>
        Liberation,
    }
}