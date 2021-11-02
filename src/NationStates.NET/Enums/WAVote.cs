namespace NationStates.NET
{
    /// <summary>
    /// Types of World Assembly votes.
    /// </summary>
    public enum WAVote
    {
        /// <summary>
        /// Supports the bill.
        /// </summary>
        For,

        /// <summary>
        /// Opposes the bill.
        /// </summary>
        Against,

        /// <summary>
        /// Has not voted.
        /// </summary>
        Undecided,

        /// <summary>
        /// No vote is being held.
        /// </summary>
        Null,
    }
}
