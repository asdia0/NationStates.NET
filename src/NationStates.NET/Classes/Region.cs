namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a region.
    /// </summary>
    public class Region
    {
        private string _Name;

        private bool nameSet = false;

        private Dictionary<string, PostStatus> statusDict = new()
        {
            { "0", PostStatus.Regular },
            { "1", PostStatus.SupressedViewable },
            { "2", PostStatus.Deleted },
            { "9", PostStatus.SupressedUnviewable },
        };

        /// <summary>
        /// Gets the region's census data.
        /// </summary>
        [JsonProperty]
        public HashSet<CensusRegion> Census
        {
            get
            {
                XmlNode node = ParseDocument($"region={this.Name}&q=census&scale=all&mode=score+rank+prank")
                    .SelectSingleNode("/REGION/CENSUS");

                HashSet<CensusRegion> regionCensus = new();

                foreach (XmlNode census in node.ChildNodes)
                {
                    int id = int.Parse(census.Attributes["id"].Value);
                    double score = double.Parse(census.SelectSingleNode("SCORE").InnerText);
                    long worldRank = long.Parse(census.SelectSingleNode("RANK").InnerText);
                    double worldPercentage = double.Parse(census.SelectSingleNode("PRANK").InnerText);

                    regionCensus.Add(new(id, this.Name, score, worldRank, worldPercentage));
                }

                return regionCensus;
            }
        }

        /// <summary>
        /// Gets the region's database ID.
        /// </summary>
        [JsonProperty]
        public long DBID
        {
            get
            {
                return long.Parse(ParseDocument($"region={this.Name}&q=dbid")
                    .SelectSingleNode("/REGION/DBID")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the name of the region's World Assembly Delegate.
        /// </summary>
        [JsonProperty]
        public string Delegate
        {
            get
            {
                return ParseDocument($"region={this.Name}&q=delegate")
                    .SelectSingleNode("/REGION/DELEGATE")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the authorities the <see cref="Delegate"/> has.
        /// </summary>
        [JsonProperty]
        public HashSet<Authority> DelegateAuthorities
        {
            get
            {
                return ParseAuthority(ParseDocument($"region={this.Name}&q=delegateauth")
                    .SelectSingleNode("/REGION/DELEGATEAUTH")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the number of World Assembly votes the <see cref="Delegate"/> has (number of endorsements + 1).
        /// </summary>
        [JsonProperty]
        public int DelegateVotes
        {
            get
            {
                return int.Parse(ParseDocument($"region={this.Name}&q=delegatevotes")
                    .SelectSingleNode("/REGION/DELEGATEVOTES")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the pinned dispatches.
        /// </summary>
        [JsonProperty]
        public HashSet<Dispatch> DispatchList
        {
            get
            {
                HashSet<Dispatch> dispatchList = new();

                foreach (string dispatchID in ParseDocument($"region={this.Name}&q=dispatches").SelectSingleNode("/REGION/DISPATCHES").InnerText.Split(","))
                {
                    dispatchList.Add(new(ulong.Parse(dispatchID)));
                }

                return dispatchList;
            }
        }

        /// <summary>
        /// Gets the region's embassies.
        /// </summary>
        [JsonProperty]
        public Dictionary<EmbassyType, HashSet<string>> Embassies
        {
            get
            {
                XmlNode node = ParseDocument($"region={this.Name}&q=embassies")
                    .SelectSingleNode("/REGION/EMBASSIES");

                Dictionary<EmbassyType, HashSet<string>> embassies = new()
                {
                    { EmbassyType.Closing, new() },
                    { EmbassyType.Constructed, new() },
                    { EmbassyType.Invited, new() },
                    { EmbassyType.Pending, new() },
                };

                foreach (XmlNode embassy in node.ChildNodes)
                {
                    EmbassyType type = EmbassyType.Constructed;

                    if (embassy.Attributes.Count != 0)
                    {
                        type = (EmbassyType)ParseEnum(typeof(EmbassyType), embassy.Attributes["type"].Value);
                    }

                    string name = embassy.InnerText;

                    embassies[type].Add(name);
                }

                return embassies;
            }
        }

        /// <summary>
        /// Gets the region's policy with RMB posting.
        /// </summary>
        [JsonProperty]
        public RMBPermission EmbassyRMBPermission
        {
            get
            {
                switch (ParseDocument($"region={this.Name}&q=embassyrmb").SelectSingleNode("/REGION/EMBASSYRMB").InnerText)
                {
                    case "0":
                        return RMBPermission.None;

                    case "con":
                        return RMBPermission.Delegate_Founder;

                    case "off":
                        return RMBPermission.Officers;

                    case "com":
                        return RMBPermission.CommunicationOfficers;

                    case "all":
                        return RMBPermission.All;

                    default:
                        throw new NSError("Unrecognised permission string.");
                }
            }
        }

        /// <summary>
        /// Gets the region's factbook.
        /// </summary>
        [JsonProperty]
        public string Factbook
        {
            get
            {
                return ParseDocument($"region={this.Name}&q=factbook")
                    .SelectSingleNode("/REGION/FACTBOOK")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the URL of the region's flag.
        /// </summary>
        [JsonProperty]
        public string Flag
        {
            get
            {
                return ParseDocument($"region={this.Name}&q=flag")
                    .SelectSingleNode("/REGION/FLAG")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the time the region was founded in natural language.
        /// </summary>
        [JsonProperty]
        public string Founded
        {
            get
            {
                return ParseDocument($"region={this.Name}&q=founded")
                    .SelectSingleNode("/REGION/FOUNDED")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the time the region was founded.
        /// </summary>
        [JsonProperty]
        public DateTime FoundedTime
        {
            get
            {
                return ParseUnix(ParseDocument($"region={this.Name}&q=foundedtime")
                    .SelectSingleNode("/REGION/FOUNDEDTIME")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the name of the region's founder.
        /// </summary>
        [JsonProperty]
        public string Founder
        {
            get
            {
                return ParseDocument($"region={this.Name}&q=founder")
                    .SelectSingleNode("/REGION/FOUNDER")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the authorities the <see cref="Founder"/> has.
        /// </summary>
        [JsonProperty]
        public HashSet<Authority> FounderAuthorities
        {
            get
            {
                return ParseAuthority(ParseDocument($"region={this.Name}&q=founderauth")
                    .SelectSingleNode("/REGION/FOUNDERAUTH")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the region's votes for/against the current General Assembly bill.
        /// </summary>
        [JsonProperty]
        public Dictionary<Vote, int>? GAVote
        {
            get
            {
                XmlNode node = ParseDocument($"region={this.Name}&q=gavote")
                    .SelectSingleNode("/REGION/GAVOTE");

                Dictionary<Vote, int> gaVote = new();

                if (node.SelectSingleNode("FOR").InnerText == string.Empty || node.SelectSingleNode("AGAINST").InnerText == string.Empty)
                {
                    return null;
                }
                else
                {
                    gaVote.Add(Vote.For, int.Parse(node.SelectSingleNode("FOR").InnerText));
                    gaVote.Add(Vote.Against, int.Parse(node.SelectSingleNode("AGAINST").InnerText));
                }

                return gaVote;
            }
        }

        /// <summary>
        /// Gets the latest happenings.
        /// </summary>
        [JsonProperty]
        public HashSet<Event> Happenings
        {
            get
            {
                return ParseEvents(ParseDocument($"region={this.Name}&q=happenings")
                    .SelectSingleNode("/REGION/HAPPENINGS"));
            }
        }

        /// <summary>
        /// Gets the history of the region.
        /// </summary>
        [JsonProperty]
        public HashSet<Event> History
        {
            get
            {
                return ParseEvents(ParseDocument($"region={this.Name}&q=history")
                    .SelectSingleNode("/REGION/HISTORY"));
            }
        }

        /// <summary>
        /// Gets the time of the last update.
        /// </summary>
        [JsonProperty]
        public DateTime LastUpdate
        {
            get
            {
                return ParseUnix(ParseDocument($"region={this.Name}&q=lastupdate")
                    .SelectSingleNode("/REGION/LASTUPDATE")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets or sets the region's name.
        /// </summary>
        [JsonProperty]
        public string Name
        {
            get
            {
                return this._Name;
            }

            set
            {
                if (!this.nameSet)
                {
                    this._Name = value;
                    this.nameSet = true;
                }
            }
        }

        /// <summary>
        /// Gets the list of nations in the region.
        /// </summary>
        [JsonProperty]
        public HashSet<string> Nations
        {
            get
            {
                return ParseDocument($"region={this.Name}&q=nations")
                    .SelectSingleNode("/REGION/NATIONS")
                    .InnerText
                    .Split(":")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets the number of nations in the region.
        /// </summary>
        [JsonProperty]
        public int NumberOfNations
        {
            get
            {
                return int.Parse(ParseDocument($"region={this.Name}&q=numnations")
                    .SelectSingleNode("/REGION/NUMNATIONS")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the list of regional officers.
        /// </summary>
        [JsonProperty]
        public HashSet<Officer> Officers
        {
            get
            {
                XmlNode node = ParseDocument($"region={this.Name}&q=officers")
                    .SelectSingleNode("/REGION/OFFICERS");

                HashSet<Officer> officers = new();

                foreach (XmlNode officer in node.ChildNodes)
                {
                    string nation = officer.SelectSingleNode("NATION").InnerText;
                    string office = officer.SelectSingleNode("OFFICE").InnerText;
                    HashSet<Authority> authorities = ParseAuthority(officer.SelectSingleNode("AUTHORITY").InnerText);
                    DateTime appointed = ParseUnix(officer.SelectSingleNode("TIME").InnerText);
                    string appointer = officer.SelectSingleNode("BY").InnerText;

                    officers.Add(new Officer(nation, office, authorities, appointed, appointer));
                }

                return officers;
            }
        }

        /// <summary>
        /// Gets the current poll.
        /// </summary>
        [JsonProperty]
        public Poll? Poll
        {
            get
            {
                XmlNode? node = ParseDocument($"region={this.Name}&q=poll")
                    .SelectSingleNode("/REGION/POLL");

                if (node == null)
                {
                    return null;
                }

                long pollID = long.Parse(node.Attributes["id"].Value);
                string title = node.SelectSingleNode("TITLE").InnerText;
                string region = node.SelectSingleNode("REGION").InnerText;
                DateTime start = ParseUnix(node.SelectSingleNode("START").InnerText);
                DateTime stop = ParseUnix(node.SelectSingleNode("STOP").InnerText);
                string author = node.SelectSingleNode("AUTHOR").InnerText;
                HashSet<PollOption> options = new HashSet<PollOption>();

                foreach (XmlNode option in node.SelectSingleNode("OPTIONS").ChildNodes)
                {
                    int optionID = int.Parse(option.Attributes["id"].Value);
                    string text = option.SelectSingleNode("OPTIONTEXT").InnerText;
                    int votes = int.Parse(option.SelectSingleNode("VOTES").InnerText);
                    HashSet<string> voters = option.SelectSingleNode("VOTERS").InnerText.Split(":").ToHashSet();

                    options.Add(new PollOption(optionID, text, votes, voters));
                }

                return new Poll(pollID, title, region, start, stop, author, options);
            }
        }

        /// <summary>
        /// Gets the region's power in the world.
        /// </summary>
        [JsonProperty]
        public Power Power
        {
            get
            {
                return (Power)ParseEnum(typeof(Power), ParseDocument($"region={this.Name}&q=power")
                    .SelectSingleNode("/REGION/POWER")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the region's vote for/against the current Security Council bill.
        /// </summary>
        [JsonProperty]
        public Dictionary<Vote, int>? SCVote
        {
            get
            {
                XmlNode node = ParseDocument($"region={this.Name}&q=scvote")
                    .SelectSingleNode("/REGION/SCVOTE");

                Dictionary<Vote, int> scVote = new();

                if (node.SelectSingleNode("FOR").InnerText == string.Empty || node.SelectSingleNode("AGAINST").InnerText == string.Empty)
                {
                    return null;
                }
                else
                {
                    scVote.Add(Vote.For, int.Parse(node.SelectSingleNode("FOR").InnerText));
                    scVote.Add(Vote.Against, int.Parse(node.SelectSingleNode("AGAINST").InnerText));
                }

                return scVote;
            }
        }

        /// <summary>
        /// Gets the region's tags.
        /// </summary>
        [JsonProperty]
        public HashSet<RegionTag> Tags
        {
            get
            {
                XmlNode node = ParseDocument($"region={this.Name}&q=tags")
                    .SelectSingleNode("/REGION/TAGS");

                HashSet<RegionTag> tags = new();

                foreach (XmlNode tag in node.ChildNodes)
                {
                    tags.Add((RegionTag)ParseEnum(typeof(RegionTag), tag.InnerText));
                }

                return tags;
            }
        }

        /// <summary>
        /// Gets the region's World Assembly badges.
        /// </summary>
        [JsonProperty]
        public HashSet<Badge> WABadges
        {
            get
            {
                XmlNode node = ParseDocument($"region={this.Name}&q=wabadges")
                    .SelectSingleNode("/REGION/WABADGES");

                HashSet<Badge> waBadges = new();

                foreach (XmlNode badge in node.ChildNodes)
                {
                    BadgeType type = (BadgeType)ParseEnum(typeof(BadgeType), badge.Attributes["type"].Value);
                    long id = long.Parse(badge.InnerText);

                    waBadges.Add(new Badge(this.Name, type, id));
                }

                return waBadges;
            }
        }

        /// <summary>
        /// Gets the region's Z-Day information.
        /// </summary>
        [JsonProperty]
        public ZombieStats Zombie
        {
            get
            {
                XmlNode node = ParseDocument($"region={this.Name}&q=zombie")
                    .SelectSingleNode("/REGION/ZOMBIE");

                long survivors = long.Parse(node.SelectSingleNode("SURVIVORS").InnerText);
                long zombies = long.Parse(node.SelectSingleNode("ZOMBIES").InnerText);
                long dead = long.Parse(node.SelectSingleNode("DEAD").InnerText);

                return new ZombieStats(survivors, zombies, dead);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="name">The region's name.</param>
        public Region(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets census data from a time period.
        /// </summary>
        /// <param name="start">The start of the time period as a UNIX timestamp.</param>
        /// <param name="end">The end of the time period as a UNIX timestamp.</param>
        /// <returns>A list of all census data recorded during the time period.</returns>
        public HashSet<CensusRecord> CensusHistory(DateTime? start, DateTime? end)
        {
            XmlNode node = ParseDocument($"region={this.Name}&q=census&scale=all&mode=history{((start != null) ? "&from=" + ConvertToUnix((DateTime)start) : string.Empty)}{((end != null) ? "&to=" + ConvertToUnix((DateTime)end) : string.Empty)}")
                .SelectSingleNode("/REGION/CENSUS");

            HashSet<CensusRecord> records = new();

            foreach (XmlNode scale in node.ChildNodes)
            {
                int id = int.Parse(scale.Attributes["id"].Value);

                foreach (XmlNode point in scale.ChildNodes)
                {
                    double score = double.Parse(point.SelectSingleNode("SCORE").InnerText);
                    DateTime timeStamp = ParseUnix(point.SelectSingleNode("TIMESTAMP").InnerText);

                    records.Add(new(id, this.Name, score, timeStamp));
                }
            }

            return records;
        }

        /// <summary>
        /// Gets twenty nations in order of their census rankings.
        /// </summary>
        /// <param name="id">The census to compare.</param>
        /// <param name="start">The starting rank.</param>
        /// <returns>A list of twenty nations with their census rank and score.</returns>
        public HashSet<CensusRegionRank> CensusRank(int id, int start = 1)
        {
            XmlNode node = ParseDocument($"region={this.Name}&q=censusranks;scale={id};start={start}")
                .SelectSingleNode("/REGION/CENSUSRANK/NATIONS");

            HashSet<CensusRegionRank> censusRanks = new();

            foreach (XmlNode nation in node.ChildNodes)
            {
                string name = nation.SelectSingleNode("NAME").InnerText;
                long rank = long.Parse(nation.SelectSingleNode("RANK").InnerText);
                double score = double.Parse(nation.SelectSingleNode("SCORE").InnerText);

                censusRanks.Add(new(id, name, rank, score));
            }

            return censusRanks;
        }

        /// <summary>
        /// Gets RMB messages.
        /// </summary>
        /// <param name="limit">The number of messages to return. Must be between 1 and 100. Is 10 by default.</param>
        /// <param name="offset">Number of latest messages to skip.</param>
        /// <param name="fromID">Return messages from the message with the given ID.</param>
        /// <returns>A list of RMB messages.</returns>
        public HashSet<Post> Messages(int? limit, int? offset, ulong? fromID)
        {
            XmlNode node = ParseDocument($"region={this.Name}&q=messages{((limit != null) ? ("&limit=" + limit.ToString()) : string.Empty)}{((offset != null) ? ("&offset=" + offset.ToString()) : string.Empty)}{((fromID != null) ? ("&fromid=" + fromID.ToString()) : string.Empty)}")
                .SelectSingleNode("/REGION/MESSAGES");

            HashSet<Post> messages = new();

            foreach (XmlNode message in node.ChildNodes)
            {
                ulong id = ulong.Parse(message.Attributes["id"].Value);
                DateTime posted = ParseUnix(message.SelectSingleNode("TIMESTAMP").InnerText);
                string nation = message.SelectSingleNode("NATION").InnerText;
                PostStatus status = this.statusDict[message.SelectSingleNode("STATUS").InnerText];
                DateTime? edited = (message.SelectNodes("EDITED").Count == 0) ? null : ParseUnix(message.SelectSingleNode("EDITED").InnerText);
                HashSet<string>? likers = message.SelectNodes("LIKERS").Count == 0 ? null : message.SelectSingleNode("LIKERS").InnerText.Split(":").ToHashSet();
                string content = message.SelectSingleNode("MESSAGE").InnerText;
                string? supressor = message.SelectNodes("SUPPRESOR").Count == 0 ? null : message.SelectSingleNode("SUPPRESOR").InnerText;

                messages.Add(new Post(id, posted, nation, status, edited, likers, content, supressor));
            }

            return messages;
        }

        /// <summary>
        /// Gets a JSON string representing the region.
        /// </summary>
        /// <returns>A JSON string representing the region.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}