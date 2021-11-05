namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents the WorldAssembly.
    /// </summary>
    public static class WorldAssembly
    {
        /// <summary>
        /// Gets the number of nations in the world assembly.
        /// </summary>
        public static long NumNations
        {
            get
            {
                return long.Parse(ParseDocument("wa=1&q=numnations")
                    .SelectSingleNode("/WA/NUMNATIONS")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the number of delegates.
        /// </summary>
        public static int NumDelegates
        {
            get
            {
                return int.Parse(ParseDocument("wa=1&q=numdelegates")
                    .SelectSingleNode("/WA/NUMDELEGATES")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a list of the names of all the delegates.
        /// </summary>
        public static HashSet<string> Delegates
        {
            get
            {
                return ParseDocument("wa=1&q=delegates")
                    .SelectSingleNode("/WA/DELEGATES")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets a list of the names of all the nations in the world assembly.
        /// </summary>
        public static HashSet<string> Members
        {
            get
            {
                return ParseDocument("wa=1&q=members")
                    .SelectSingleNode("/WA/MEMBERS")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets information about the last resolution that was voted upon in the General Assembly.
        /// </summary>
        public static string GALastResolution
        {
            get
            {
                return ParseDocument("wa=1&q=lastresolution").SelectSingleNode("/WA/LASTRESOLUTION").InnerText;
            }
        }

        /// <summary>
        /// Gets a list of proposals in the General Assembly.
        /// </summary>
        public static HashSet<WAProposal> GAProposals
        {
            get
            {
                HashSet<WAProposal> proposals = new();

                foreach (XmlNode node in ParseDocument("wa=1&q=proposals").SelectNodes("/WA/PROPOSALS/PROPOSAL"))
                {
                    string id = node.Attributes["id"].Value;
                    HashSet<string> approvals = node.SelectSingleNode("APPROVALS").InnerText.Split(":").ToHashSet();
                    WAGACategory category = (WAGACategory)Enum.Parse(typeof(WAGACategory), FormatForEnum(Capitalise(node.SelectSingleNode("CATEGORY").InnerText)));
                    DateTime created = ParseUnix(node.SelectSingleNode("CREATED").InnerText);
                    string description = node.SelectSingleNode("DESC").InnerText;
                    string name = node.SelectSingleNode("NAME").InnerText;
                    string proposer = node.SelectSingleNode("PROPOSED_BY").InnerText;
                    dynamic subCategory = ParseSubCategory(node.SelectSingleNode("OPTION"), WACouncil.General_Assembly, category);

                    proposals.Add(new(id, approvals, category, WACouncil.General_Assembly, created, description, name, proposer, subCategory));
                }

                return proposals;
            }
        }

        /// <summary>
        /// Gets the current proposal at vote in the General Assembly.
        /// </summary>
        public static WAProposalAtVote GAProposalAtVote
        {
            get
            {
                XmlNode node = ParseDocument("wa=1&q=resolution+votetrack+voters+dellog+delvotes")
                    .SelectSingleNode("/WA/RESOLUTION");

                string id = node.SelectSingleNode("ID").InnerText;
                WAGACategory category = (WAGACategory)Enum.Parse(typeof(WAGACategory), FormatForEnum(Capitalise(node.SelectSingleNode("CATEGORY").InnerText)));
                DateTime created = ParseUnix(node.SelectSingleNode("CREATED").InnerText);

                HashSet<DelegateEntry> delegateLog = new();
                foreach (XmlNode delegateEntry in node.SelectNodes("DELLOG/ENTRY"))
                {
                    WAVote action = (WAVote)Enum.Parse(typeof(WAVote), FormatForEnum(Capitalise(delegateEntry.SelectSingleNode("ACTION").InnerText)));
                    string nation = delegateEntry.SelectSingleNode("NATION").InnerText;
                    DateTime timeStamp = ParseUnix(delegateEntry.SelectSingleNode("TIMESTAMP").InnerText);
                    int votes = int.Parse(delegateEntry.SelectSingleNode("VOTES").InnerText);

                    delegateLog.Add(new(action, nation, timeStamp, votes));
                }

                HashSet<DelegateVote> delegateVotesAgainst = new();
                foreach (XmlNode delegateVoteAgainst in node.SelectNodes("DELVOTES_FOR/DELEGATE"))
                {
                    string nation = delegateVoteAgainst.SelectSingleNode("NATION").InnerText;
                    DateTime timeStamp = ParseUnix(delegateVoteAgainst.SelectSingleNode("TIMESTAMP").InnerText);
                    int votes = int.Parse(delegateVoteAgainst.SelectSingleNode("VOTES").InnerText);

                    delegateVotesAgainst.Add(new(nation, timeStamp, votes));
                }

                HashSet<DelegateVote> delegateVotesFor = new();
                foreach (XmlNode delegateVoteFor in node.SelectNodes("DELVOTES_FOR/DELEGATE"))
                {
                    string nation = delegateVoteFor.SelectSingleNode("NATION").InnerText;
                    DateTime timeStamp = ParseUnix(delegateVoteFor.SelectSingleNode("TIMESTAMP").InnerText);
                    int votes = int.Parse(delegateVoteFor.SelectSingleNode("VOTES").InnerText);

                    delegateVotesAgainst.Add(new(nation, timeStamp, votes));
                }

                string description = node.SelectSingleNode("DESC").InnerText;
                string name = node.SelectSingleNode("NAME").InnerText;
                string proposer = node.SelectSingleNode("PROPOSED_BY").InnerText;
                dynamic subCategory = ParseSubCategory(node.SelectSingleNode("OPTION"), WACouncil.General_Assembly, category);
                long totalNationsAgainst = long.Parse(node.SelectSingleNode("TOTAL_NATIONS_AGAINST").InnerText);
                long totalNationsFor = long.Parse(node.SelectSingleNode("TOTAL_NATIONS_FOR").InnerText);
                long totalVotesAgainst = long.Parse(node.SelectSingleNode("TOTAL_VOTES_AGAINST").InnerText);
                long totalVotesFor = long.Parse(node.SelectSingleNode("TOTAL_VOTES_FOR").InnerText);

                HashSet<string> votesAgainst = new();
                foreach (XmlNode voteAgainst in node.SelectNodes("VOTES_AGAINST/N"))
                {
                    votesAgainst.Add(voteAgainst.InnerText);
                }

                HashSet<string> votesFor = new();
                foreach (XmlNode voteFor in node.SelectNodes("VOTES_FOR/N"))
                {
                    votesFor.Add(voteFor.InnerText);
                }

                List<long> voteTrackAgainst = new();
                foreach (XmlNode votes in node.SelectNodes("VOTE_TRACK_AGAINST/N"))
                {
                    voteTrackAgainst.Add(long.Parse(votes.InnerText));
                }

                List<long> voteTrackFor = new();
                foreach (XmlNode votes in node.SelectNodes("VOTE_TRACK_FOR/N"))
                {
                    voteTrackFor.Add(long.Parse(votes.InnerText));
                }

                return new(id, category, WACouncil.General_Assembly, created, delegateLog, delegateVotesAgainst, delegateVotesFor, description, name, proposer, subCategory,
                    totalNationsAgainst, totalNationsFor, totalVotesAgainst, totalVotesFor, votesAgainst, votesFor, voteTrackAgainst, voteTrackFor);
            }
        }

        /// <summary>
        /// Gets a list of recent events in the world assembly.
        /// </summary>
        public static HashSet<Event> Happenings
        {
            get
            {
                return ParseEvents(ParseDocument($"wa=1&q=happenings")
                    .SelectSingleNode("/WA/HAPPENINGS"));
            }
        }

        /// <summary>
        /// Gets a list of proposals in the Security Council.
        /// </summary>
        public static HashSet<WAProposal> SCProposals
        {
            get
            {
                HashSet<WAProposal> proposals = new();

                foreach (XmlNode node in ParseDocument("wa=2&q=proposals").SelectNodes("/WA/PROPOSALS/PROPOSAL"))
                {
                    string id = node.Attributes["id"].Value;
                    HashSet<string> approvals = node.SelectSingleNode("APPROVALS").InnerText.Split(":").ToHashSet();
                    WASCCategory category = (WASCCategory)Enum.Parse(typeof(WASCCategory), FormatForEnum(Capitalise(node.SelectSingleNode("CATEGORY").InnerText)));
                    DateTime created = ParseUnix(node.SelectSingleNode("CREATED").InnerText);
                    string description = node.SelectSingleNode("DESC").InnerText;
                    string name = node.SelectSingleNode("NAME").InnerText;
                    string proposer = node.SelectSingleNode("PROPOSED_BY").InnerText;
                    dynamic subCategory = ParseSubCategory(node.SelectSingleNode("OPTION"), WACouncil.Security_Council, category);

                    proposals.Add(new(id, approvals, category, WACouncil.Security_Council, created, description, name, proposer, subCategory));
                }

                return proposals;
            }
        }

        /// <summary>
        /// Gets the current proposal at vote in the Security Council.
        /// </summary>
        public static WAProposalAtVote SCProposalAtVote
        {
            get
            {
                XmlNode node = ParseDocument("wa=2&q=resolution+votetrack+voters+dellog+delvotes")
                    .SelectSingleNode("/WA/RESOLUTION");

                string id = node.SelectSingleNode("ID").InnerText;
                WASCCategory category = (WASCCategory)Enum.Parse(typeof(WASCCategory), FormatForEnum(Capitalise(node.SelectSingleNode("CATEGORY").InnerText)));
                DateTime created = ParseUnix(node.SelectSingleNode("CREATED").InnerText);

                HashSet<DelegateEntry> delegateLog = new();
                foreach (XmlNode delegateEntry in node.SelectNodes("DELLOG/ENTRY"))
                {
                    WAVote action = (WAVote)Enum.Parse(typeof(WAVote), FormatForEnum(Capitalise(delegateEntry.SelectSingleNode("ACTION").InnerText)));
                    string nation = delegateEntry.SelectSingleNode("NATION").InnerText;
                    DateTime timeStamp = ParseUnix(delegateEntry.SelectSingleNode("TIMESTAMP").InnerText);
                    int votes = int.Parse(delegateEntry.SelectSingleNode("VOTES").InnerText);

                    delegateLog.Add(new(action, nation, timeStamp, votes));
                }

                HashSet<DelegateVote> delegateVotesAgainst = new();
                foreach (XmlNode delegateVoteAgainst in node.SelectNodes("DELVOTES_FOR/DELEGATE"))
                {
                    string nation = delegateVoteAgainst.SelectSingleNode("NATION").InnerText;
                    DateTime timeStamp = ParseUnix(delegateVoteAgainst.SelectSingleNode("TIMESTAMP").InnerText);
                    int votes = int.Parse(delegateVoteAgainst.SelectSingleNode("VOTES").InnerText);

                    delegateVotesAgainst.Add(new(nation, timeStamp, votes));
                }

                HashSet<DelegateVote> delegateVotesFor = new();
                foreach (XmlNode delegateVoteFor in node.SelectNodes("DELVOTES_FOR/DELEGATE"))
                {
                    string nation = delegateVoteFor.SelectSingleNode("NATION").InnerText;
                    DateTime timeStamp = ParseUnix(delegateVoteFor.SelectSingleNode("TIMESTAMP").InnerText);
                    int votes = int.Parse(delegateVoteFor.SelectSingleNode("VOTES").InnerText);

                    delegateVotesAgainst.Add(new(nation, timeStamp, votes));
                }

                string description = node.SelectSingleNode("DESC").InnerText;
                string name = node.SelectSingleNode("NAME").InnerText;
                string proposer = node.SelectSingleNode("PROPOSED_BY").InnerText;
                dynamic subCategory = ParseSubCategory(node.SelectSingleNode("OPTION"), WACouncil.Security_Council, category);
                long totalNationsAgainst = long.Parse(node.SelectSingleNode("TOTAL_NATIONS_AGAINST").InnerText);
                long totalNationsFor = long.Parse(node.SelectSingleNode("TOTAL_NATIONS_FOR").InnerText);
                long totalVotesAgainst = long.Parse(node.SelectSingleNode("TOTAL_VOTES_AGAINST").InnerText);
                long totalVotesFor = long.Parse(node.SelectSingleNode("TOTAL_VOTES_FOR").InnerText);

                HashSet<string> votesAgainst = new();
                foreach (XmlNode voteAgainst in node.SelectNodes("VOTES_AGAINST/N"))
                {
                    votesAgainst.Add(voteAgainst.InnerText);
                }

                HashSet<string> votesFor = new();
                foreach (XmlNode voteFor in node.SelectNodes("VOTES_FOR/N"))
                {
                    votesFor.Add(voteFor.InnerText);
                }

                List<long> voteTrackAgainst = new();
                foreach (XmlNode votes in node.SelectNodes("VOTE_TRACK_AGAINST/N"))
                {
                    voteTrackAgainst.Add(long.Parse(votes.InnerText));
                }

                List<long> voteTrackFor = new();
                foreach (XmlNode votes in node.SelectNodes("VOTE_TRACK_FOR/N"))
                {
                    voteTrackFor.Add(long.Parse(votes.InnerText));
                }

                return new(id, category, WACouncil.Security_Council, created, delegateLog, delegateVotesAgainst, delegateVotesFor, description, name, proposer, subCategory,
                    totalNationsAgainst, totalNationsFor, totalVotesAgainst, totalVotesFor, votesAgainst, votesFor, voteTrackAgainst, voteTrackFor);
            }
        }

        /// <summary>
        /// Gets informatin about the last resolution that was voted upon in the Security Council.
        /// </summary>
        public static string SCLastResolution
        {
            get
            {
                return ParseDocument("wa=2&q=lastresolution").SelectSingleNode("/WA/LASTRESOLUTION").InnerText;
            }
        }
    }
}