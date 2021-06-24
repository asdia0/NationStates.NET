namespace NationStates.NET.Nation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Represents a nation.
    /// </summary>
    public class Nation
    {
        /// <summary>
        /// Name of national animal.
        /// </summary>
        public string Animal { get; set; }

        /// <summary>
        /// Number of issues answered.
        /// </summary>
        public int Answered { get; set; }

        /// <summary>
        /// List of banners that can be displayed.
        /// </summary>
        public HashSet<string> Banners { get; set; }

        /// <summary>
        /// Name of capital city.
        /// </summary>
        public string Capital { get; set; }

        /// <summary>
        /// Type of nation.
        /// </summary>
        public NationCategory Category { get; set; }

        /// <summary>
        /// Name of currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// ID of nation.
        /// </summary>
        public long DBID { get; set; }

        /// <summary>
        /// Causes of deaths and their frequency.
        /// </summary>
        public HashSet<Death> Deaths { get; set; }

        /// <summary>
        /// Demonym (adjective).
        /// </summary>
        public string DemonymAdjective { get; set; }

        /// <summary>
        /// Demonym (noun).
        /// </summary>
        public string DemonymNoun { get; set; }

        /// <summary>
        /// Demonym (noun, plural).
        /// </summary>
        public string DemonymPlural { get; set; }

        /// <summary>
        /// List of dispatches authored.
        /// </summary>
        public List<Dispatch> DispatchList { get; set; }

        /// <summary>
        /// List of endoresements.
        /// </summary>
        public HashSet<string> Endorsements { get; set; }

        /// <summary>
        /// Time of first login.
        /// </summary>
        public DateTime FirstLogin { get; set; }

        /// <summary>
        /// URL of flag.
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// Time of founding.
        /// </summary>
        public DateTime FoundedTime { get; set; }

        /// <summary>
        /// Civil rights, economy and political freedoms.
        /// </summary>
        public Freedom Freedom { get; set; }

        /// <summary>
        /// Full name of nation.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Vote on current General Assembly bill.
        /// </summary>
        public WAVote GAVote { get; set; }

        /// <summary>
        /// GDP in standard dollars.
        /// </summary>
        public ulong GDP { get; set; }

        /// <summary>
        /// Government funding frequency.
        /// </summary>
        public Government Government { get; set; }

        /// <summary>
        /// List of recent events.
        /// </summary>
        public List<Event> Happenings { get; set; }

        /// <summary>
        /// Income of average citizen in standard dollars.
        /// </summary>
        public long Income { get; set; }

        /// <summary>
        /// Influence level.
        /// </summary>
        public Influence Influence { get; set; }

        /// <summary>
        /// Time of last login.
        /// </summary>
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// Name of leader.
        /// </summary>
        public string Leader { get; set; }

        /// <summary>
        /// Largest industry.
        /// </summary>
        public Industry MajorIndustry { get; set; }

        /// <summary>
        /// Nation's motto.
        /// </summary>
        public string Motto { get; set; }

        /// <summary>
        /// Nation's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of policies implemented.
        /// </summary>
        public HashSet<Policy> Policies { get; set; }

        /// <summary>
        /// Average income of poorest 10% in standard dollars.
        /// </summary>
        public long Poorest { get; set; }

        /// <summary>
        /// Population in millions.
        /// </summary>
        public long Population { get; set; }

        /// <summary>
        /// Percentage of public sector in economy.
        /// </summary>
        public double PublicSector { get; set; }

        /// <summary>
        /// Nation's region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Name of religion.
        /// </summary>
        public string Religion { get; set; }

        /// <summary>
        /// Avergae income of richest 10% in standard dollars.
        /// </summary>
        public long Richest { get; set; }

        /// <summary>
        /// Vote on current Security Council bill.
        /// </summary>
        public WAVote SCVote { get; set; }

        /// <summary>
        /// List of sectors and their share in the economy.
        /// </summary>
        public HashSet<Sector> Sectors { get; set; }

        /// <summary>
        /// Tax rate.
        /// </summary>
        public double Tax { get; set; }

        /// <summary>
        /// Whether the nation accepts recruitment telegrams.
        /// </summary>
        public bool TelegramCanRecruit { get; set; }

        /// <summary>
        /// Whether the nation accepts campaign telegrams.
        /// </summary>
        public bool TelegramCanCampaign { get; set; }

        /// <summary>
        /// Type of nation.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Nation's World Assembly status.
        /// </summary>
        public WAStatus WA { get; set; }

        /// <summary>
        /// List of commendations/condemnations received.
        /// </summary>
        public HashSet<WABadge> WABadges { get; set; }

        /// <summary>
        /// Z-Day information.
        /// </summary>
        public Zombie Zombie { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Nation"/> class.
        /// </summary>
        /// <param name="name">The name of the nation.</param>
        public Nation(string name)
        {
            this.Name = name;
            GetFields();
        }

        /// <summary>
        /// Updates the nation's information.
        /// </summary>
        public void GetFields()
        {
            // Normal fields
            XmlDocument normal = new XmlDocument();

            normal.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?nation={this.Name.Replace(" ", "_")}&q=animal+answered+banners+capital+category+currency+dbid+deaths+demonym+demonym2+demonym2plural+dispatchlist+endorsements+firstlogin+flag+foundedtime+freedom+fullname+gavote+gdp+govt+happenings+income+influence+lastlogin+leader+majorindustry+motto+name+policies+poorest+population+publicsector+region+religion+richest+scvote+sectors+tax+tgcanrecruit+tgcancampaign+type+wa+wabadges+zombie"));

            foreach (XmlNode node in normal.DocumentElement.ChildNodes)
            {
                this.ParseFieldsData(node);
            }

            // TODO: Census
        }

        /// <summary>
        /// Parses the XML document provided in <see cref="GetFields"/>.
        /// </summary>
        /// <param name="node"></param>
        public void ParseFieldsData(XmlNode node)
        {
            switch (node.Name)
            {
                case "NAME":
                    this.Name = node.InnerText;
                    break;
                case "DBID":
                    this.DBID = long.Parse(node.InnerText);
                    break;
                case "TYPE":
                    this.Type = node.InnerText;
                    break;
                case "FULLNAME":
                    this.FullName = node.InnerText;
                    break;
                case "MOTTO":
                    this.Motto = node.InnerText;
                    break;
                case "CATEGORY":
                    this.Category = (NationCategory)Enum.Parse(typeof(NationCategory), Utility.FormatForEnum(node.InnerText));
                    break;
                case "UNSTATUS":
                    this.WA = (WAStatus)Enum.Parse(typeof(WAStatus), Utility.FormatForEnum(Utility.Capitalise(node.InnerText.Replace("WA ", ""))));
                    break;
                case "ENDORSEMENTS":
                    this.Endorsements = node.InnerText.Split(",").ToHashSet();
                    break;
                case "GAVOTE":
                    if (node.InnerText == string.Empty)
                    {
                        this.GAVote = WAVote.Null;
                    }
                    else
                    {
                        this.GAVote = (WAVote)Enum.Parse(typeof(WAVote), Utility.Capitalise(node.InnerText));
                    }
                    break;
                case "SCVOTE":
                    if (node.InnerText == string.Empty)
                    {
                        this.SCVote = WAVote.Null;
                    }
                    else
                    {
                        this.SCVote = (WAVote)Enum.Parse(typeof(WAVote), Utility.Capitalise(node.InnerText));
                    }
                    break;
                case "ISSUES_ANSWERED":
                    this.Answered = int.Parse(node.InnerText);
                    break;
                case "FREEDOM":
                    CivilRights civil = (CivilRights)Enum.Parse(typeof(CivilRights), Utility.FormatForEnum(node.SelectSingleNode("CIVILRIGHTS").InnerText));
                    Economy economy = (Economy)Enum.Parse(typeof(Economy), Utility.FormatForEnum(node.SelectSingleNode("ECONOMY").InnerText));
                    PoliticalFreedoms political = (PoliticalFreedoms)Enum.Parse(typeof(PoliticalFreedoms), Utility.FormatForEnum(node.SelectSingleNode("POLITICALFREEDOM").InnerText));

                    this.Freedom = new Freedom(civil, economy, political);
                    break;
                case "REGION":
                    this.Region = node.InnerText;
                    break;
                case "POPULATION":
                    this.Population = long.Parse(node.InnerText);
                    break;
                case "TAX":
                    this.Tax = double.Parse(node.InnerText);
                    break;
                case "ANIMAL":
                    this.Animal = node.InnerText;
                    break;
                case "CURRENCY":
                    this.Currency = node.InnerText;
                    break;
                case "FLAG":
                    this.Flag = node.InnerText;
                    break;
                case "BANNERS":
                    this.Banners = new HashSet<string>();
                    foreach (XmlNode banner in node.ChildNodes)
                    {
                        this.Banners.Add(banner.InnerText);
                    }
                    break;
                case "DEMONYM":
                    this.DemonymNoun = node.InnerText;
                    break;
                case "DEMONYM2":
                    this.DemonymNoun = node.InnerText;
                    break;
                case "DEMONYM2PLURAL":
                    this.DemonymPlural = node.InnerText;
                    break;
                case "GDP":
                    this.GDP = ulong.Parse(node.InnerText);
                    break;
                case "INCOME":
                    this.Income = long.Parse(node.InnerText);
                    break;
                case "RICHEST":
                    this.Richest = long.Parse(node.InnerText);
                    break;
                case "POOREST":
                    this.Poorest = long.Parse(node.InnerText);
                    break;
                case "MAJORINDUSTRY":
                    this.MajorIndustry = (Industry)Enum.Parse(typeof(Industry), node.InnerText.Replace(" ", "_").Replace("-", ""));
                    break;
                case "GOVT":
                    double administration = double.Parse(node.SelectSingleNode("ADMINISTRATION").InnerText);
                    double defence = double.Parse(node.SelectSingleNode("DEFENCE").InnerText);
                    double education = double.Parse(node.SelectSingleNode("EDUCATION").InnerText);
                    double environment = double.Parse(node.SelectSingleNode("ENVIRONMENT").InnerText);
                    double healthcare = double.Parse(node.SelectSingleNode("HEALTHCARE").InnerText);
                    double commerce = double.Parse(node.SelectSingleNode("COMMERCE").InnerText);
                    double internationalAid = double.Parse(node.SelectSingleNode("INTERNATIONALAID").InnerText);
                    double lawAndOrder = double.Parse(node.SelectSingleNode("LAWANDORDER").InnerText);
                    double publicTransport = double.Parse(node.SelectSingleNode("PUBLICTRANSPORT").InnerText);
                    double socialEquality = double.Parse(node.SelectSingleNode("SOCIALEQUALITY").InnerText);
                    double spirituality = double.Parse(node.SelectSingleNode("SPIRITUALITY").InnerText);
                    double welfare = double.Parse(node.SelectSingleNode("WELFARE").InnerText);
                    this.Government = new Government(administration, defence, education, environment, healthcare, commerce, internationalAid, lawAndOrder, publicTransport, socialEquality, spirituality, welfare);
                    break;
                case "SECTORS":
                    this.Sectors = new HashSet<Sector>();
                    foreach (XmlNode sector in node.ChildNodes)
                    {
                        this.Sectors.Add(new Sector((SectorType)Enum.Parse(typeof(SectorType), Utility.FormatForEnum(Utility.Capitalise(sector.Name))), double.Parse(sector.InnerText)));
                    }
                    break;
                case "FOUNDEDTIME":
                    this.FoundedTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(node.InnerText)).DateTime;
                    break;
                case "FIRSTLOGIN":
                    this.FirstLogin = DateTimeOffset.FromUnixTimeSeconds(long.Parse(node.InnerText)).DateTime;
                    break;
                case "LASTLOGIN":
                    this.LastLogin = DateTimeOffset.FromUnixTimeSeconds(long.Parse(node.InnerText)).DateTime;
                    break;
                case "INFLUENCE":
                    this.Influence = (Influence)Enum.Parse(typeof(Influence), Utility.FormatForEnum(Utility.Capitalise(node.InnerText)));
                    break;
                case "PUBLICSECTOR":
                    this.PublicSector = double.Parse(node.InnerText);
                    break;
                case "DEATHS":
                    this.Deaths = new HashSet<Death>();
                    foreach (XmlNode death in node.ChildNodes)
                    {
                        this.Deaths.Add(new Death(death.Attributes["type"].Value, double.Parse(death.InnerText)));
                    }
                    break;
                case "LEADER":
                    this.Leader = node.InnerText;
                    break;
                case "CAPITAL":
                    this.Capital = node.InnerText;
                    break;
                case "RELIGION":
                    this.Religion = node.InnerText;
                    break;
                case "POLICIES":
                    this.Policies = new HashSet<Policy>();
                    foreach (XmlNode policy in node.ChildNodes)
                    {
                        string name = policy.SelectSingleNode("NAME").InnerText;
                        PolicyCategory category = (PolicyCategory)Enum.Parse(typeof(PolicyCategory), Utility.FormatForEnum(Utility.Capitalise(policy.SelectSingleNode("CAT").InnerText.Replace("&", "and"))));
                        string desc = policy.SelectSingleNode("DESC").InnerText;

                        this.Policies.Add(new Policy(name, category, desc));
                    }
                    break;
                case "HAPPENINGS":
                    this.Happenings = new List<Event>();
                    foreach (XmlNode happening in node.ChildNodes)
                    {
                        DateTime timestamp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(happening.SelectSingleNode("TIMESTAMP").InnerText)).DateTime;
                        string text = happening.SelectSingleNode("TEXT").InnerText;

                        this.Happenings.Add(new Event(timestamp, text));
                    }
                    break;
                case "DISPATCHLIST":
                    this.DispatchList = new List<Dispatch>();
                    foreach (XmlNode dispatch in node.ChildNodes)
                    {
                        ulong id = ulong.Parse(dispatch.Attributes["id"].Value);
                        string title = dispatch.SelectSingleNode("TITLE").InnerText;
                        DispatchCategory category = (DispatchCategory)Enum.Parse(typeof(DispatchCategory), Utility.FormatForEnum(Utility.Capitalise(dispatch.SelectSingleNode("CATEGORY").InnerText)));
                        DispatchSubCategory subCategory = (DispatchSubCategory)Enum.Parse(typeof(DispatchSubCategory), Utility.FormatForEnum(Utility.Capitalise(dispatch.SelectSingleNode("SUBCATEGORY").InnerText)));
                        DateTime created = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dispatch.SelectSingleNode("CREATED").InnerText)).DateTime;
                        DateTime edited = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dispatch.SelectSingleNode("EDITED").InnerText)).DateTime;
                        long views = long.Parse(dispatch.SelectSingleNode("VIEWS").InnerText);
                        int score = int.Parse(dispatch.SelectSingleNode("SCORE").InnerText);

                        this.DispatchList.Add(new Dispatch(id, title, this, category, subCategory, created, edited, views, score));
                    }
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
                case "TGCANRECRUIT":
                    this.TelegramCanRecruit = (int.Parse(node.InnerText) == 1);
                    break;
                case "TGCANCAMPAIGN":
                    this.TelegramCanCampaign = (int.Parse(node.InnerText) == 1);
                    break;
                case "ZOMBIE":
                    ZombieAction action = (ZombieAction)Enum.Parse(typeof(ZombieAction), Utility.Capitalise(node.SelectSingleNode("ZACTION").InnerText));
                    ZombieAction? intendedAction = ((node.SelectSingleNode("ZACTIONINTENDED").InnerText == string.Empty) ? null : (ZombieAction)Enum.Parse(typeof(ZombieAction), Utility.Capitalise(node.SelectSingleNode("ZACTIONINTENDED").InnerText)));
                    long survivors = long.Parse(node.SelectSingleNode("SURVIVORS").InnerText);
                    long zombies = long.Parse(node.SelectSingleNode("ZOMBIES").InnerText);
                    long dead = long.Parse(node.SelectSingleNode("DEAD").InnerText);

                    this.Zombie = new Zombie(action, intendedAction, survivors, zombies, dead);
                    break;
            }
        }
    }
}
