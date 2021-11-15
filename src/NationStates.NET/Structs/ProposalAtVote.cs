namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents a <see cref="Proposal"/> that is currently at vote.
    /// </summary>
    public struct ProposalAtVote
    {
        /// <summary>
        /// Gets the proposal's category.
        /// </summary>
        [JsonProperty]
        public dynamic Category { get; }

        /// <summary>
        /// Gets the council the proposal was submitted in.
        /// </summary>
        [JsonProperty]
        public Council Council { get; }

        /// <summary>
        /// Gets the time at which the proposal was created.
        /// </summary>
        [JsonProperty]
        public DateTime Created { get; }

        /// <summary>
        /// Gets a log of the delegates' voting activity.
        /// </summary>
        [JsonProperty]
        public HashSet<DelegateEntry> DelegateLog { get; }

        /// <summary>
        /// Gets a list of delegate votes against the proposal.
        /// </summary>
        [JsonProperty]
        public HashSet<DelegateVote> DelegateVotesAgainst { get; }

        /// <summary>
        /// Gets a list of delegate votes for the proposal.
        /// </summary>
        [JsonProperty]
        public HashSet<DelegateVote> DelegateVotesFor { get; }

        /// <summary>
        /// Gets the body of the proposal.
        /// </summary>
        [JsonProperty]
        public string Description { get; }

        /// <summary>
        /// Gets the proposal's ID.
        /// </summary>
        [JsonProperty]
        public string ID { get; }

        /// <summary>
        /// Gets the proposal's title.
        /// </summary>
        [JsonProperty]
        public string Title { get; }

        /// <summary>
        /// Gets the name of the nation that proposed the proposal.
        /// </summary>
        [JsonProperty]
        public string Proposer { get; }

        /// <summary>
        /// Gets the proposal's sub-category.
        /// </summary>
        [JsonProperty]
        public dynamic SubCategory { get; }

        /// <summary>
        /// Gets the number of nations against the proposal.
        /// </summary>
        [JsonProperty]
        public long TotalNationsAgainst { get; }

        /// <summary>
        /// Gets the number of nations for the proposal.
        /// </summary>
        [JsonProperty]
        public long TotalNationsFor { get; }

        /// <summary>
        /// Gets the number of votes against the proposal.
        /// </summary>
        [JsonProperty]
        public long TotalVotesAgainst { get; }

        /// <summary>
        /// Gets the number of votes for the proposal.
        /// </summary>
        [JsonProperty]
        public long TotalVotesFor { get; }

        /// <summary>
        /// Gets a list of nations voting against the proposal.
        /// </summary>
        [JsonProperty]
        public HashSet<string> VotesAgainst { get; }

        /// <summary>
        /// Gets a list of nations voting for the proposal.
        /// </summary>
        [JsonProperty]
        public HashSet<string> VotesFor { get; }

        /// <summary>
        /// Gets a list tracking the number of votes against the proposal over time.
        /// </summary>
        [JsonProperty]
        public List<long> VoteTrackAgainst { get; }

        /// <summary>
        /// Gets a list tracking the number of votes for the proposal over time.
        /// </summary>
        [JsonProperty]
        public List<long> VoteTrackFor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProposalAtVote"/> struct.
        /// </summary>
        /// <param name="council">The council in which the proposal was submitted in.</param>
        public ProposalAtVote(Council council)
        {
            this.Council = council;

            XmlNode node = ParseXMLDocument($"wa={(int)council + 1}&q=resolution+votetrack+voters+dellog+delvotes").SelectSingleNode("/WA/RESOLUTION");

            this.ID = node.SelectSingleNode("ID").InnerText;

            switch (council)
            {
                case Council.General_Assembly:
                    this.Category = (GACategory)ParseEnum(typeof(GACategory), node.SelectSingleNode("CATEGORY").InnerText);
                    this.SubCategory = ParseWASubCategory(node.SelectSingleNode("OPTION"), this.Category);
                    break;

                case Council.Security_Council:
                    this.Category = (SCCategory)ParseEnum(typeof(SCCategory), node.SelectSingleNode("CATEGORY").InnerText);
                    this.SubCategory = ParseWASubCategory(node.SelectSingleNode("OPTION"), this.Category);
                    break;

                default:
                    throw new NSError("Invalid council.");
            }

            this.Created = ParseUnix(node.SelectSingleNode("CREATED").InnerText);

            HashSet<DelegateEntry> delegateLog = new();
            foreach (XmlNode delegateEntry in node.SelectNodes("DELLOG/ENTRY"))
            {
                DelegateAction action = (DelegateAction)ParseEnum(typeof(DelegateAction), delegateEntry.SelectSingleNode("ACTION").InnerText);
                string nation = delegateEntry.SelectSingleNode("NATION").InnerText;
                DateTime timeStamp = ParseUnix(delegateEntry.SelectSingleNode("TIMESTAMP").InnerText);
                int votes = int.Parse(delegateEntry.SelectSingleNode("VOTES").InnerText);

                delegateLog.Add(new(action, nation, timeStamp, votes));
            }

            this.DelegateLog = delegateLog;

            HashSet<DelegateVote> delegateVotesAgainst = new();
            foreach (XmlNode delegateVoteAgainst in node.SelectNodes("DELVOTES_FOR/DELEGATE"))
            {
                string nation = delegateVoteAgainst.SelectSingleNode("NATION").InnerText;
                DateTime timeStamp = ParseUnix(delegateVoteAgainst.SelectSingleNode("TIMESTAMP").InnerText);
                int votes = int.Parse(delegateVoteAgainst.SelectSingleNode("VOTES").InnerText);

                delegateVotesAgainst.Add(new(nation, timeStamp, votes));
            }

            this.DelegateVotesAgainst = delegateVotesAgainst;

            HashSet<DelegateVote> delegateVotesFor = new();
            foreach (XmlNode delegateVoteFor in node.SelectNodes("DELVOTES_FOR/DELEGATE"))
            {
                string nation = delegateVoteFor.SelectSingleNode("NATION").InnerText;
                DateTime timeStamp = ParseUnix(delegateVoteFor.SelectSingleNode("TIMESTAMP").InnerText);
                int votes = int.Parse(delegateVoteFor.SelectSingleNode("VOTES").InnerText);

                delegateVotesFor.Add(new(nation, timeStamp, votes));
            }

            this.DelegateVotesFor = delegateVotesFor;

            this.Description = node.SelectSingleNode("DESC").InnerText;
            this.Title = node.SelectSingleNode("NAME").InnerText;
            this.Proposer = node.SelectSingleNode("PROPOSED_BY").InnerText;
            this.TotalNationsAgainst = long.Parse(node.SelectSingleNode("TOTAL_NATIONS_AGAINST").InnerText);
            this.TotalNationsFor = long.Parse(node.SelectSingleNode("TOTAL_NATIONS_FOR").InnerText);
            this.TotalVotesAgainst = long.Parse(node.SelectSingleNode("TOTAL_VOTES_AGAINST").InnerText);
            this.TotalVotesFor = long.Parse(node.SelectSingleNode("TOTAL_VOTES_FOR").InnerText);

            HashSet<string> votesAgainst = new();
            foreach (XmlNode voteAgainst in node.SelectNodes("VOTES_AGAINST/N"))
            {
                votesAgainst.Add(voteAgainst.InnerText);
            }

            this.VotesAgainst = votesAgainst;

            HashSet<string> votesFor = new();
            foreach (XmlNode voteFor in node.SelectNodes("VOTES_FOR/N"))
            {
                votesFor.Add(voteFor.InnerText);
            }

            this.VotesFor = votesFor;

            List<long> voteTrackAgainst = new();
            foreach (XmlNode votes in node.SelectNodes("VOTE_TRACK_AGAINST/N"))
            {
                voteTrackAgainst.Add(long.Parse(votes.InnerText));
            }

            this.VoteTrackAgainst = voteTrackAgainst;

            List<long> voteTrackFor = new();
            foreach (XmlNode votes in node.SelectNodes("VOTE_TRACK_FOR/N"))
            {
                voteTrackFor.Add(long.Parse(votes.InnerText));
            }

            this.VoteTrackFor = voteTrackFor;
        }

        /// <summary>
        /// Gets a JSON string representing the proposal at vote.
        /// </summary>
        /// <returns>A JSON string representing the proposal at vote.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}