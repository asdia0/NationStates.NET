namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of actions delegates can do to the <see cref="ProposalAtVote"/>.
    /// </summary>
    public enum DelegateAction
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