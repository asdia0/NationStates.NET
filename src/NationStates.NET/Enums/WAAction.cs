namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of actions delegates can do to the <see cref="WAProposalAtVote"/>.
    /// </summary>
    public enum WAAction
    {
        /// <summary>
        /// Vote against the proposal.
        /// </summary>
        Against,

        /// <summary>
        /// Vote for the proposal.
        /// </summary>
        For,

        /// <summary>
        /// Withdraw their vote for the proposal.
        /// </summary>
        Withdrew,
    }
}