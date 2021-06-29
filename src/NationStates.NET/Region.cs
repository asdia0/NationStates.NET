namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Represents a region.
    /// </summary>
    public class Region
    {
        /// <summary>
        /// Gets or sets the region's database ID.
        /// </summary>
        public long DBID { get; set; }

        /// <summary>
        /// Gets or sets the name of the region's World Assembly Delegate.
        /// </summary>
        public string Delegate { get; set; }

        /// <summary>
        /// Gets or sets the authorities the <see cref="Delegate"/> has.
        /// </summary>
        public HashSet<Authority> DelegateAuthorities { get; set; }

        /// <summary>
        /// Gets or sets the number of World Assembly votes the <see cref="Delegate"/> has (number of endorsements + 1).
        /// </summary>
        public int DelegateVotes { get; set; }

        /// <summary>
        /// Gets or sets the pinned dispatches.
        /// </summary>
        public HashSet<Dispatch> DispatchList { get; set; }

        /// <summary>
        /// Gets or sets the region's embassies.
        /// </summary>
        public Dictionary<EmbassyType, HashSet<string>> Embassies { get; set; }

        /// <summary>
        /// Gets or sets the region's policy with RMB posting.
        /// </summary>
        public RMBPermission EmbassyRMBPermission { get; set; }

        /// <summary>
        /// Gets or sets the region's factbook.
        /// </summary>
        public string Factbook { get; set; }

        /// <summary>
        /// Gets or sets the URL of the region's flag.
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// Gets or sets the time the region was founded.
        /// </summary>
        public DateTime FoundedTime { get; set; }

        /// <summary>
        /// Gets or sets the name of the region's founder.
        /// </summary>
        public string Founder { get; set; }

        /// <summary>
        /// Gets or sets the authorities the <see cref="Founder"/> has.
        /// </summary>
        public HashSet<Authority> FounderAuthorities { get; set; }

        /// <summary>
        /// Gets or sets the region's votes for/against the current General Assembly bill.
        /// </summary>
        public Dictionary<WAVote, int?> GAVote { get; set; }

        /// <summary>
        /// Gets or sets the latest happenings.
        /// </summary>
        public HashSet<Event> Happenings { get; set; }

        /// <summary>
        /// Gets or sets the history of the region.
        /// </summary>
        public HashSet<Event> History { get; set; }

        /// <summary>
        /// Gets or sets the time of the last update.
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Gets or sets the last 10 RMB messages.
        /// </summary>
        public HashSet<Post> Messages { get; set; }

        /// <summary>
        /// Gets or sets the region's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of nations in the region.
        /// </summary>
        public HashSet<string> Nations { get; set; }

        /// <summary>
        /// Gets or sets the list of regional officers.
        /// </summary>
        public HashSet<Officer> Officers { get; set; }

        /// <summary>
        /// Gets or sets the current poll.
        /// </summary>
        public Poll? Poll { get; set; }

        /// <summary>
        /// Gets or sets the region's power in the world.
        /// </summary>
        public Power Power { get; set; }

        /// <summary>
        /// Gets or sets the region's vote for/against the current Security Council bill.
        /// </summary>
        public Dictionary<WAVote, int?> SCVote { get; set; }

        /// <summary>
        /// Gets or sets the region's tags.
        /// </summary>
        public HashSet<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the region's World Assembly badges.
        /// </summary>
        public HashSet<WABadge> WABadges { get; set; }

        /// <summary>
        /// Gets or sets the region's Z-Day information.
        /// </summary>
        public RegionZombie Zombie { get; set; }

        /// <summary>
        /// Gets or sets the region's census data.
        /// </summary>
        public HashSet<RegionCensus> Census { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="name">The region's name.</param>
        public Region(string name)
        {
            this.Name = name;
            this.UpdateProperties();
        }

        /// <summary>
        /// Updates the region's properties.
        /// </summary>
        public void UpdateProperties()
        {
            // Normal fields
            XmlDocument normal = new XmlDocument();

            normal.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?region={this.Name.Replace(" ", "_")}&q=dbid+delegate+delegateauth+delegatevotes+dispatches+embassies+embassyrmb+factbook+flag+foundedtime+founder+founderauth+gavote+happenings+history+lastupdate+messages+name+nations+officers+poll+power+scvote+tags+wabadges+zombie"));

            foreach (XmlNode node in normal.DocumentElement.ChildNodes)
            {
                this.ParsePropertyData(node);
            }

            // Census
            XmlDocument census = new XmlDocument();

            census.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?region={this.Name.Replace(" ", "_")}&q=census;scale=all;mode=score+rank+prank"));

            this.ParseCensusData(census.DocumentElement.SelectSingleNode("CENSUS"));
        }

        /// <summary>
        /// Parses and updates the region's properties from a XmlDocument.
        /// </summary>
        /// <param name="node">The XmlNode to parse.</param>
        public void ParsePropertyData(XmlNode node)
        {
            switch (node.Name)
            {
                case "DBID":
                    this.DBID = long.Parse(node.InnerText);
                    break;
                case "LASTUPDATE":
                    this.LastUpdate = Utility.ParseUnix(node.InnerText);
                    break;
                case "FACTBOOK":
                    this.Factbook = node.InnerText;
                    break;
                case "DISPATCHES":
                    this.DispatchList = new HashSet<Dispatch>();
                    foreach (string id in node.InnerText.Split(","))
                    {
                        this.DispatchList.Add(World.GetDispatch(ulong.Parse(id)));
                    }

                    break;
                case "NATIONS":
                    this.Nations = node.InnerText.Split(":").ToHashSet();
                    break;
                case "DELEGATE":
                    this.Delegate = node.InnerText;
                    break;
                case "DELEGATEAUTH":
                    this.DelegateAuthorities = Utility.ParseAuthority(node.InnerText);
                    break;
                case "OFFICERS":
                    this.Officers = new HashSet<Officer>();
                    foreach (XmlNode officer in node.ChildNodes)
                    {
                        string nation = officer.SelectSingleNode("NATION").InnerText;
                        string office = officer.SelectSingleNode("OFFICE").InnerText;
                        HashSet<Authority> authorities = Utility.ParseAuthority(officer.SelectSingleNode("AUTHORITY").InnerText);
                        DateTime appointed = Utility.ParseUnix(officer.SelectSingleNode("TIME").InnerText);
                        string appointer = officer.SelectSingleNode("BY").InnerText;

                        this.Officers.Add(new Officer(nation, office, authorities, appointed, appointer));
                    }

                    break;
                case "DELEGATEVOTES":
                    this.DelegateVotes = int.Parse(node.InnerText);
                    break;
                case "GAVOTE":
                    this.GAVote = new Dictionary<WAVote, int?>();
                    if (node.SelectSingleNode("FOR").InnerText == string.Empty)
                    {
                        this.GAVote.Add(WAVote.For, null);
                    }
                    else
                    {
                        this.GAVote.Add(WAVote.For, int.Parse(node.SelectSingleNode("FOR").InnerText));
                    }

                    if (node.SelectSingleNode("AGAINST").InnerText == string.Empty)
                    {
                        this.GAVote.Add(WAVote.Against, 0);
                    }
                    else
                    {
                        this.GAVote.Add(WAVote.Against, int.Parse(node.SelectSingleNode("AGAINST").InnerText));
                    }

                    break;
                case "SCVOTE":
                    this.SCVote = new Dictionary<WAVote, int?>();
                    if (node.SelectSingleNode("FOR").InnerText == string.Empty)
                    {
                        this.SCVote.Add(WAVote.For, null);
                    }
                    else
                    {
                        this.SCVote.Add(WAVote.For, int.Parse(node.SelectSingleNode("FOR").InnerText));
                    }

                    if (node.SelectSingleNode("AGAINST").InnerText == string.Empty)
                    {
                        this.SCVote.Add(WAVote.Against, 0);
                    }
                    else
                    {
                        this.SCVote.Add(WAVote.Against, int.Parse(node.SelectSingleNode("AGAINST").InnerText));
                    }

                    break;
                case "FOUNDER":
                    this.Founder = node.InnerText;
                    break;
                case "FOUNDERAUTH":
                    this.FounderAuthorities = Utility.ParseAuthority(node.InnerText);
                    break;
                case "FOUNDEDTIME":
                    this.FoundedTime = Utility.ParseUnix(node.InnerText);
                    break;
                case "POWER":
                    this.Power = (Power)Enum.Parse(typeof(Power), Utility.FormatForEnum(Utility.Capitalise(node.InnerText)));
                    break;
                case "FLAG":
                    this.Flag = node.InnerText;
                    break;
                case "EMBASSIES":
                    this.Embassies = new Dictionary<EmbassyType, HashSet<string>>();
                    foreach (XmlNode embassy in node.ChildNodes)
                    {
                        EmbassyType type = EmbassyType.Constructed;

                        if (embassy.Attributes.Count != 0)
                        {
                            type = (EmbassyType)Enum.Parse(typeof(EmbassyType), Utility.Capitalise(embassy.Attributes["type"].Value));
                        }

                        string name = embassy.InnerText;

                        if (!this.Embassies.ContainsKey(type))
                        {
                            this.Embassies.Add(type, new HashSet<string>());
                        }

                        this.Embassies[type].Add(name);
                    }

                    break;
                case "EMBASSYRMB":
                    this.EmbassyRMBPermission = Utility.ParseRMBPermission(node.InnerText);
                    break;
                case "WABADGES":
                    this.WABadges = new HashSet<WABadge>();
                    foreach (XmlNode badge in node.ChildNodes)
                    {
                        WABadgeType type = (WABadgeType)Enum.Parse(typeof(WABadgeType), Utility.Capitalise(badge.Attributes["type"].Value));
                        long id = long.Parse(badge.InnerText);

                        this.WABadges.Add(new WABadge(type, id));
                    }

                    break;
                case "TAGS":
                    this.Tags = new HashSet<Tag>();
                    foreach (XmlNode tag in node.ChildNodes)
                    {
                        this.Tags.Add((Tag)Enum.Parse(typeof(Tag), Utility.FormatForEnum(tag.InnerText)));
                    }

                    break;
                case "MESSAGES":
                    this.Messages = new HashSet<Post>();
                    foreach (XmlNode message in node.ChildNodes)
                    {
                        ulong id = ulong.Parse(message.Attributes["id"].Value);
                        DateTime posted = Utility.ParseUnix(message.SelectSingleNode("TIMESTAMP").InnerText);
                        string nation = message.SelectSingleNode("NATION").InnerText;
                        PostStatus status = Utility.ParseStatus(message.SelectSingleNode("STATUS").InnerText);
                        DateTime? edited = (message.SelectNodes("EDITED").Count == 0) ? null : Utility.ParseUnix(message.SelectSingleNode("EDITED").InnerText);
                        HashSet<string>? likers = message.SelectNodes("LIKERS").Count == 0 ? null : message.SelectSingleNode("LIKERS").InnerText.Split(":").ToHashSet();
                        string content = message.SelectSingleNode("MESSAGE").InnerText;
                        string? supressor = message.SelectNodes("SUPPRESOR").Count == 0 ? null : message.SelectSingleNode("SUPPRESOR").InnerText;

                        this.Messages.Add(new Post(id, posted, nation, status, edited, likers, content, supressor));
                    }

                    break;
                case "HAPPENINGS":
                    this.Happenings = Utility.ParseEvents(node);
                    break;
                case "HISTORY":
                    this.History = new HashSet<Event>();
                    foreach (XmlNode happening in node.ChildNodes)
                    {
                        DateTime timestamp = Utility.ParseUnix(happening.SelectSingleNode("TIMESTAMP").InnerText);
                        string text = happening.SelectSingleNode("TEXT").InnerText;

                        this.Happenings.Add(new Event(timestamp, text));
                    }

                    break;
                case "ZOMBIE":
                    long survivors = long.Parse(node.SelectSingleNode("SURVIVORS").InnerText);
                    long zombies = long.Parse(node.SelectSingleNode("ZOMBIES").InnerText);
                    long dead = long.Parse(node.SelectSingleNode("DEAD").InnerText);

                    this.Zombie = new RegionZombie(survivors, zombies, dead);

                    break;
                case "POLL":
                    this.Poll = World.GetPoll(long.Parse(node.Attributes["id"].Value));
                    break;
            }
        }

        /// <summary>
        /// Parses and updates <see cref="Census"/> from a XmlDocument.
        /// </summary>
        /// <param name="census">The XmlNode to parse.</param>
        public void ParseCensusData(XmlNode census)
        { }
    }
}
