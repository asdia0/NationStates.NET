namespace NationStates.NET
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
        /// Gets or sets the name of national animal.
        /// </summary>
        public string Animal { get; set; }

        /// <summary>
        /// Gets or sets the number of issues answered.
        /// </summary>
        public int Answered { get; set; }

        /// <summary>
        /// Gets or sets the list of banners that can be displayed.
        /// </summary>
        public HashSet<string> Banners { get; set; }

        /// <summary>
        /// Gets or sets the name of the nation's capital city.
        /// </summary>
        public string Capital { get; set; }

        /// <summary>
        /// Gets or sets the type of nation.
        /// </summary>
        public NationCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the name of currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the nation's database ID.
        /// </summary>
        public long DBID { get; set; }

        /// <summary>
        /// Gets or sets the causes of deaths and their frequency.
        /// </summary>
        public HashSet<Death> Deaths { get; set; }

        /// <summary>
        /// Gets or sets the nation's demonym (adjective).
        /// </summary>
        public string DemonymAdjective { get; set; }

        /// <summary>
        /// Gets or sets the nation's demonym (noun).
        /// </summary>
        public string DemonymNoun { get; set; }

        /// <summary>
        /// Gets or sets the nation's demonym (plural).
        /// </summary>
        public string DemonymPlural { get; set; }

        /// <summary>
        /// Gets or sets the list of dispatches authored by the nation.
        /// </summary>
        public HashSet<Dispatch> DispatchList { get; set; }

        /// <summary>
        /// Gets or sets the list of endoresements.
        /// </summary>
        public HashSet<string> Endorsements { get; set; }

        /// <summary>
        /// Gets or sets the time of first login.
        /// </summary>
        public DateTime FirstLogin { get; set; }

        /// <summary>
        /// Gets or sets the URL of the nation's flag.
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// Gets or sets the time of founding.
        /// </summary>
        public DateTime FoundedTime { get; set; }

        /// <summary>
        /// Gets or sets the level of the nation's civil rights, economy and political freedoms.
        /// </summary>
        public Freedom Freedom { get; set; }

        /// <summary>
        /// Gets or sets the nation's vote on current General Assembly bill.
        /// </summary>
        public WAVote GAVote { get; set; }

        /// <summary>
        /// Gets or sets the nation's GDP in standard dollars.
        /// </summary>
        public ulong GDP { get; set; }

        /// <summary>
        /// Gets or sets the nation's government funding in various departments.
        /// </summary>
        public Government Government { get; set; }

        /// <summary>
        /// Gets or sets the list of recent events.
        /// </summary>
        public HashSet<Event> Happenings { get; set; }

        /// <summary>
        /// Gets or sets the income of an average citizen in standard dollars.
        /// </summary>
        public long Income { get; set; }

        /// <summary>
        /// Gets or sets the nation's influence level.
        /// </summary>
        public Influence Influence { get; set; }

        /// <summary>
        /// Gets or sets the time of last login.
        /// </summary>
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// Gets or sets the name of the nation's leader.
        /// </summary>
        public string Leader { get; set; }

        /// <summary>
        /// Gets or sets the nation's largest industry.
        /// </summary>
        public Industry MajorIndustry { get; set; }

        /// <summary>
        /// Gets or sets the nation's motto.
        /// </summary>
        public string Motto { get; set; }

        /// <summary>
        /// Gets or sets the nation's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of policies implemented.
        /// </summary>
        public HashSet<Policy> Policies { get; set; }

        /// <summary>
        /// Gets or sets the average income of the poorest 10% in standard dollars.
        /// </summary>
        public long Poorest { get; set; }

        /// <summary>
        /// Gets or sets the nation's population in millions.
        /// </summary>
        public long Population { get; set; }

        /// <summary>
        /// Gets or sets the percentage of the public sector in the nation's economy.
        /// </summary>
        public double PublicSector { get; set; }

        /// <summary>
        /// Gets or sets the nation's region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the name of national religion.
        /// </summary>
        public string Religion { get; set; }

        /// <summary>
        /// Gets or sets the avergae income of the richest 10% in standard dollars.
        /// </summary>
        public long Richest { get; set; }

        /// <summary>
        /// Gets or sets the nation's vote on current Security Council bill.
        /// </summary>
        public WAVote SCVote { get; set; }

        /// <summary>
        /// Gets or sets the list of sectors and their share in the economy.
        /// </summary>
        public HashSet<Sector> Sectors { get; set; }

        /// <summary>
        /// Gets or sets the nation's tax rate.
        /// </summary>
        public double Tax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the nation accepts recruitment telegrams.
        /// </summary>
        public bool TelegramCanRecruit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the nation accepts campaign telegrams.
        /// </summary>
        public bool TelegramCanCampaign { get; set; }

        /// <summary>
        /// Gets or sets the nation's type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the nation's World Assembly status.
        /// </summary>
        public WAStatus WA { get; set; }

        /// <summary>
        /// Gets or sets the list of commendations/condemnations received.
        /// </summary>
        public HashSet<WABadge> WABadges { get; set; }

        /// <summary>
        /// Gets or sets the nation's Z-Day information.
        /// </summary>
        public NationZombie Zombie { get; set; }

        /// <summary>
        /// Gets or sets the nation's census data.
        /// </summary>
        public HashSet<NationCensus> Census { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Nation"/> class.
        /// </summary>
        /// <param name="name">The name of the nation.</param>
        public Nation(string name)
        {
            this.Name = name;
            this.UpdateProperties();
        }

        /// <summary>
        /// Updates the nation's properties.
        /// </summary>
        public void UpdateProperties()
        {
            // Normal fields
            XmlDocument normal = new XmlDocument();

            normal.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?nation={this.Name.Replace(" ", "_")}&q=animal+answered+banners+capital+category+currency+dbid+deaths+demonym+demonym2+demonym2plural+dispatchlist+endorsements+firstlogin+flag+foundedtime+freedom+fullname+gavote+gdp+govt+happenings+income+influence+lastlogin+leader+majorindustry+motto+name+policies+poorest+population+publicsector+region+religion+richest+scvote+sectors+tax+tgcanrecruit+tgcancampaign+type+wa+wabadges+zombie"));

            foreach (XmlNode node in normal.DocumentElement.ChildNodes)
            {
                this.ParsePropertyData(node);
            }

            // Census
            XmlDocument census = new XmlDocument();

            census.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?nation={this.Name.Replace(" ", "_")};q=census;scale=all;mode=score+rank+rrank+prank+prrank"));

            this.ParseCensusData(census.DocumentElement.SelectSingleNode("CENSUS"));
        }

        /// <summary>
        /// Parses and updates the nation's properties from a XmlDocument.
        /// </summary>
        /// <param name="node">The XmlNode to parse.</param>
        public void ParsePropertyData(XmlNode node)
        {
            switch (node.Name)
            {
                case "DBID":
                    this.DBID = long.Parse(node.InnerText);
                    break;
                case "TYPE":
                    this.Type = node.InnerText;
                    break;
                case "MOTTO":
                    this.Motto = node.InnerText;
                    break;
                case "CATEGORY":
                    this.Category = (NationCategory)Enum.Parse(typeof(NationCategory), Utility.FormatForEnum(node.InnerText));
                    break;
                case "UNSTATUS":
                    this.WA = (WAStatus)Enum.Parse(typeof(WAStatus), Utility.FormatForEnum(Utility.Capitalise(node.InnerText.Replace("WA ", string.Empty))));
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
                    this.MajorIndustry = (Industry)Enum.Parse(typeof(Industry), node.InnerText.Replace(" ", "_").Replace("-", string.Empty));
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
                    this.FoundedTime = Utility.ParseUnix(node.InnerText);
                    break;
                case "FIRSTLOGIN":
                    this.FirstLogin = Utility.ParseUnix(node.InnerText);
                    break;
                case "LASTLOGIN":
                    this.LastLogin = Utility.ParseUnix(node.InnerText);
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
                    this.Happenings = Utility.ParseEvents(node);
                    break;
                case "DISPATCHLIST":
                    this.DispatchList = new HashSet<Dispatch>();
                    foreach (XmlNode dispatch in node.ChildNodes)
                    {
                        ulong id = ulong.Parse(dispatch.Attributes["id"].Value);
                        string title = dispatch.SelectSingleNode("TITLE").InnerText;
                        DispatchCategory category = (DispatchCategory)Enum.Parse(typeof(DispatchCategory), Utility.FormatForEnum(Utility.Capitalise(dispatch.SelectSingleNode("CATEGORY").InnerText)));
                        string author = dispatch.SelectSingleNode("AUTHOR").InnerText;
                        dynamic subcategory = null;

                        switch (category)
                        {
                            case DispatchCategory.Account:
                                subcategory = (DispatchAccount)Enum.Parse(typeof(DispatchAccount), dispatch.SelectSingleNode("SUBCATEGORY").InnerText);
                                break;
                            case DispatchCategory.Bulletin:
                                subcategory = (DispatchBulletin)Enum.Parse(typeof(DispatchBulletin), dispatch.SelectSingleNode("SUBCATEGORY").InnerText);
                                break;
                            case DispatchCategory.Factbook:
                                subcategory = (DispatchFactbook)Enum.Parse(typeof(DispatchFactbook), dispatch.SelectSingleNode("SUBCATEGORY").InnerText);
                                break;
                            case DispatchCategory.Meta:
                                subcategory = (DispatchMeta)Enum.Parse(typeof(DispatchMeta), dispatch.SelectSingleNode("SUBCATEGORY").InnerText);
                                break;
                            default:
                                throw new NSError("Dispatch subcategory does not exist.");
                        }

                        DateTime created = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dispatch.SelectSingleNode("CREATED").InnerText)).DateTime;
                        DateTime edited = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dispatch.SelectSingleNode("EDITED").InnerText)).DateTime;
                        long vews = long.Parse(dispatch.SelectSingleNode("VIEWS").InnerText);
                        int score = int.Parse(dispatch.SelectSingleNode("SCORE").InnerText);

                        this.DispatchList.Add(new Dispatch(id, title, author, category, subcategory, created, edited, vews, score));
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
                    this.TelegramCanRecruit = int.Parse(node.InnerText) == 1;
                    break;
                case "TGCANCAMPAIGN":
                    this.TelegramCanCampaign = int.Parse(node.InnerText) == 1;
                    break;
                case "ZOMBIE":
                    ZombieAction action = (ZombieAction)Enum.Parse(typeof(ZombieAction), Utility.Capitalise(node.SelectSingleNode("ZACTION").InnerText));
                    ZombieAction? intendedAction = (node.SelectSingleNode("ZACTIONINTENDED").InnerText == string.Empty) ? null : (ZombieAction)Enum.Parse(typeof(ZombieAction), Utility.Capitalise(node.SelectSingleNode("ZACTIONINTENDED").InnerText));
                    long survivors = long.Parse(node.SelectSingleNode("SURVIVORS").InnerText);
                    long zombies = long.Parse(node.SelectSingleNode("ZOMBIES").InnerText);
                    long dead = long.Parse(node.SelectSingleNode("DEAD").InnerText);

                    this.Zombie = new NationZombie(action, intendedAction, survivors, zombies, dead);
                    break;
            }
        }

        /// <summary>
        /// Parses and updates <see cref="Census"/> from a XmlDocument.
        /// </summary>
        /// <param name="census">The XmlNode to parse.</param>
        public void ParseCensusData(XmlNode census)
        {
            this.Census = new HashSet<NationCensus>();

            foreach (XmlNode scale in census.ChildNodes)
            {
                int id = int.Parse(scale.Attributes["id"].Value);
                double score = double.Parse(scale.SelectSingleNode("SCORE").InnerText);
                long worldRank = long.Parse(scale.SelectSingleNode("RANK").InnerText);
                long regionRank = long.Parse(scale.SelectSingleNode("RRANK").InnerText);
                double worldPercentage = double.Parse(scale.SelectSingleNode("PRANK").InnerText);
                double regionPercentage = double.Parse(scale.SelectSingleNode("PRRANK").InnerText);

                this.Census.Add(new NationCensus(id, score, worldRank, regionRank, worldPercentage, regionPercentage));
            }
        }
    }
}
