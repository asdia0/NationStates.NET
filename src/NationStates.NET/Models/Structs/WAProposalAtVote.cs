namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a <see cref="WAProposal"/> that is currently at vote.
    /// </summary>
    public struct WAProposalAtVote
    {
        /// <summary>
        /// Gets the proposal's ID.
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Gets the proposal's category.
        /// </summary>
        public dynamic Category { get; }

        /// <summary>
        /// Gets the council the proposal was submitted in.
        /// </summary>
        public WACouncil Council { get; }

        /// <summary>
        /// Gets the time at which the proposal was created.
        /// </summary>
        public DateTime Created { get; }

        /// <summary>
        /// Gets a log of the delegates' voting activity.
        /// </summary>
        public HashSet<DelegateEntry> DelegateLog { get; }

        /// <summary>
        /// Gets a list of delegate votes against the proposal.
        /// </summary>
        public HashSet<DelegateVote> DelegateVotesAgainst { get; }

        /// <summary>
        /// Gets a list of delegate votes for the proposal.
        /// </summary>
        public HashSet<DelegateVote> DelegateVotesFor { get; }

        /// <summary>
        /// Gets the body of the proposal.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the proposal's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the name of the nation that proposed the proposal.
        /// </summary>
        public string Proposer { get; }

        /// <summary>
        /// Gets the number of nations against the proposal.
        /// </summary>
        public long TotalNationsAgainst { get; }

        /// <summary>
        /// Gets the number of nations for the proposal.
        /// </summary>
        public long TotalNationsFor { get; }

        /// <summary>
        /// Gets the number of votes against the proposal.
        /// </summary>
        public long TotalVotesAgainst { get; }

        /// <summary>
        /// Gets the number of votes for the proposal.
        /// </summary>
        public long TotalVotesFor { get; }

        /// <summary>
        /// Gets the proposal's sub-category.
        /// </summary>
        public dynamic SubCategory { get; }

        /// <summary>
        /// Gets a list of nations voting against the proposal.
        /// </summary>
        public HashSet<string> VotesAgainst { get; }

        /// <summary>
        /// Gets a list of nations voting for the proposal.
        /// </summary>
        public HashSet<string> VotesFor { get; }

        /// <summary>
        /// Gets a list tracking the number of votes against the proposal over time.
        /// </summary>
        public List<long> VoteTrackAgainst { get; }

        /// <summary>
        /// Gets a list tracking the number of votes for the proposal over time.
        /// </summary>
        public List<long> VoteTrackFor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WAProposalAtVote"/> struct.
        /// </summary>
        /// <param name="id">The proposal's ID.</param>
        /// <param name="category">The proposal's category.</param>
        /// <param name="council">The council in which the proposal was submitted in.</param>
        /// <param name="created">The time at which the proposal was created.</param>
        /// <param name="delegateLog">A log of the delegates' voting activity.</param>
        /// <param name="delegateVotesAgainst">A list of delegate votes against the proposal.</param>
        /// <param name="delegateVotesFor">A list of delegate votes for the proposal.</param>
        /// <param name="description">The body of the proposal.</param>
        /// <param name="name">The proposal's name.</param>
        /// <param name="proposer">The name of the nation that proposed the proposal.</param>
        /// <param name="subCategory">The proposal's sub-category.</param>
        /// <param name="totalNationsAgainst">The number of nations against the proposal.</param>
        /// <param name="totalNationsFor">The number of natins for the proposal.</param>
        /// <param name="totalVotesAgainst">The number of votes against the proposal.</param>
        /// <param name="totalVotesFor">The number of votes for the proposal.</param>
        /// <param name="votesAgainst">A list of nations voting against the proposal.</param>
        /// <param name="votesFor">A list of nations voting for the proposal.</param>
        /// <param name="voteTrackAgainst">A list tracking the number of votes against the proposal over time.</param>
        /// <param name="voteTrackFor">A list tracking the number of votes for the proposal over time.</param>
        public WAProposalAtVote(string id, dynamic category, WACouncil council, DateTime created, HashSet<DelegateEntry> delegateLog,
            HashSet<DelegateVote> delegateVotesAgainst, HashSet<DelegateVote> delegateVotesFor, string description, string name, string proposer, dynamic subCategory,
            long totalNationsAgainst, long totalNationsFor, long totalVotesAgainst, long totalVotesFor, HashSet<string> votesAgainst, HashSet<string> votesFor,
            List<long> voteTrackAgainst, List<long> voteTrackFor)
        {
            this.ID = id;
            this.Category = category;
            this.Council = council;
            this.Created = created;
            this.DelegateLog = delegateLog;
            this.DelegateVotesAgainst = delegateVotesAgainst;
            this.DelegateVotesFor = delegateVotesFor;
            this.Description = description;
            this.Name = name;
            this.Proposer = proposer;
            this.SubCategory = subCategory;
            this.TotalNationsAgainst = totalNationsAgainst;
            this.TotalNationsFor = totalNationsFor;
            this.TotalVotesAgainst = totalVotesAgainst;
            this.TotalVotesFor = totalVotesFor;
            this.VotesAgainst = votesAgainst;
            this.VotesFor = votesFor;
            this.VoteTrackAgainst = voteTrackAgainst;
            this.VoteTrackFor = voteTrackFor;
        }
    }
}