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
        private string _Name;

        private bool nameSet;

        /// <summary>
        /// Gets one of the nation's admirables.
        /// </summary>
        public string Admirable
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=admirable")
                    .SelectSingleNode("/NATION/ADMIRABLE")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets a list of the nation's admirables.
        /// </summary>
        public HashSet<string> Admirables
        {
            get
            {
                XmlNodeList doc = Utility.ParseDocument($"nation={this.Name}&q=admirables")
                    .SelectNodes("/NATION/ADMIRABLES/ADMIRABLE");

                HashSet<string> admirables = new();

                foreach (XmlNode admirable in doc)
                {
                    admirables.Add(admirable.InnerText);
                }

                return admirables;
            }
        }

        /// <summary>
        /// Gets the name of national animal.
        /// </summary>
        public string Animal
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=animal")
                    .SelectSingleNode("/NATION/ANIMAL")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the national animal's trait.
        /// </summary>
        public string AnimalTrait
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=animaltrait")
                    .SelectSingleNode("/NATION/ANIMALTRAIT")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the number of issues answered.
        /// </summary>
        public int Answered
        {
            get
            {
                return int.Parse(Utility.ParseDocument($"nation={this.Name}&q=answered")
                    .SelectSingleNode("/NATION/ISSUES_ANSWERED")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets one of the nation's banners that can be displayed.
        /// </summary>
        public Banner Banner
        {
            get
            {
                return new(Utility.ParseDocument($"nation={this.Name}&q=banner")
                    .SelectSingleNode("/NATION/BANNER")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a list of the nation's banners that can be displayed.
        /// </summary>
        public HashSet<Banner> Banners
        {
            get
            {
                XmlNodeList doc = Utility.ParseDocument($"nation={this.Name}&q=banners")
                    .SelectNodes("/NATION/BANNERS/BANNER");

                HashSet<Banner> banners = new();

                foreach (XmlNode banner in doc)
                {
                    banners.Add(new(banner.InnerText));
                }

                return banners;
            }
        }

        /// <summary>
        /// Gets the name of the nation's capital city.
        /// </summary>
        public string Capital
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=capital")
                    .SelectSingleNode("/NATION/CAPITAL")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the type of nation.
        /// </summary>
        public NationCategory Category
        {
            get
            {
                return (NationCategory)Enum.Parse(typeof(NationCategory), Utility.FormatForEnum(Utility.ParseDocument($"nation={this.Name}&q=category")
                    .SelectSingleNode("NATION/CATEGORY")
                    .InnerText));
            }
        }

        /// <summary>
        /// Gets the nation's census data.
        /// </summary>
        public HashSet<NationCensus> Census
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name.Replace(" ", "_")};q=census;scale=all;mode=score+rank+rrank+prank+prrank")
                    .SelectSingleNode("/NATION/CENSUS");

                HashSet<NationCensus> census = new();

                foreach (XmlNode scale in node.ChildNodes)
                {
                    int id = int.Parse(scale.Attributes["id"].Value);
                    double score = double.Parse(scale.SelectSingleNode("SCORE").InnerText);
                    long worldRank = long.Parse(scale.SelectSingleNode("RANK").InnerText);
                    long regionRank = long.Parse(scale.SelectSingleNode("RRANK").InnerText);
                    double worldPercentage = double.Parse(scale.SelectSingleNode("PRANK").InnerText);
                    double regionPercentage = double.Parse(scale.SelectSingleNode("PRRANK").InnerText);

                    census.Add(new NationCensus(id, score, worldRank, regionRank, worldPercentage, regionPercentage));
                }

                return census;
            }
        }

        /// <summary>
        /// Gets the name of the nation's currency.
        /// </summary>
        public string Currency
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=currency")
                    .SelectSingleNode("/NATION/CURRENCY")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's custom leader.
        /// </summary>
        public string CustomLeader
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=customleader")
                    .SelectSingleNode("/NATION/LEADER")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's custom capital.
        /// </summary>
        public string CustomCapital
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=customcapital")
                    .SelectSingleNode("/NATION/CAPITAL")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's custom religion.
        /// </summary>
        public string CustomReligion
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=customreligion")
                    .SelectSingleNode("/NATION/RELIGION")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's database ID.
        /// </summary>
        public long DBID
        {
            get
            {
                return long.Parse(Utility.ParseDocument($"nation={this.Name}&q=dbid")
                    .SelectSingleNode("/NATION/DBID")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the causes of deaths and their frequency.
        /// </summary>
        public HashSet<Death> Deaths
        {
            get
            {
                XmlNodeList list = Utility.ParseDocument($"nation={this.Name}&q=deaths")
                    .SelectNodes("/NATION/DEATHS/DEATH");

                HashSet<Death> deaths = new();

                foreach (XmlNode death in list)
                {
                    deaths.Add(new Death(death.Attributes["type"].Value, double.Parse(death.InnerText)));
                }

                return deaths;
            }
        }

        /// <summary>
        /// Gets the nation's demonym (adjective).
        /// </summary>
        public string DemonymAdjective
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=demonym")
                    .SelectSingleNode("/NATION/DEMONYM")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's demonym (noun).
        /// </summary>
        public string DemonymNoun
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=demonym2")
                    .SelectSingleNode("/NATION/DEMONYM2")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's demonym (plural).
        /// </summary>
        public string DemonymPlural
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=demonym2plural")
                    .SelectSingleNode("/NATION/DEMONYM2PLURAL")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the number of dispatches authored by the nation.
        /// </summary>
        public int Dispatches
        {
            get
            {
                return int.Parse(Utility.ParseDocument($"nation={this.Name}&q=dispatches")
                    .SelectSingleNode("/NATION/DISPATCHES")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the list of dispatches authored by the nation.
        /// </summary>
        public HashSet<Dispatch> DispatchList
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=dispatchlist")
                    .SelectSingleNode("/NATION/DISPATCHLIST");

                HashSet<Dispatch> dispatchList = new();

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

                    dispatchList.Add(new Dispatch(id, title, author, category, subcategory, created, edited, vews, score));
                }

                return dispatchList;
            }
        }

        /// <summary>
        /// Gets a list of the nation's endorsements.
        /// </summary>
        public HashSet<string> Endorsements
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=endorsements")
                    .SelectSingleNode("/NATION/ENDORSEMENTS")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets the number of factbooks authored by the nation.
        /// </summary>
        public int Factbooks
        {
            get
            {
                return int.Parse(Utility.ParseDocument($"nation={this.Name}&q=factbooks")
                    .SelectSingleNode("/NATION/FACTBOOKS")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the time the nation first logged in.
        /// </summary>
        public DateTime FirstLogin
        {
            get
            {
                return Utility.ParseUnix(Utility.ParseDocument($"nation={this.Name}&q=firstlogin")
                    .SelectSingleNode("/NATION/FIRSTLOGIN")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the URL of the nation's flag.
        /// </summary>
        public string Flag
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=flag")
                    .SelectSingleNode("/NATION/FLAG")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the time the nationw as founded as natural language.
        /// </summary>
        public string Founded
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=founded")
                    .SelectSingleNode("/NATION/FOUNDED")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the time the nation was founded.
        /// </summary>
        public DateTime FoundedTime
        {
            get
            {
                return Utility.ParseUnix(Utility.ParseDocument($"nation={this.Name}&q=foundedtime")
                    .SelectSingleNode("/NATION/FOUNDEDTIME")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the level of the nation's civil rights, economy and political freedoms.
        /// </summary>
        public Freedom Freedom
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=freedom")
                    .SelectSingleNode("/NATION/FREEDOM");

                CivilRights civil = (CivilRights)Enum.Parse(typeof(CivilRights), Utility.FormatForEnum(node.SelectSingleNode("CIVILRIGHTS").InnerText));
                Economy economy = (Economy)Enum.Parse(typeof(Economy), Utility.FormatForEnum(node.SelectSingleNode("ECONOMY").InnerText));
                PoliticalFreedoms political = (PoliticalFreedoms)Enum.Parse(typeof(PoliticalFreedoms), Utility.FormatForEnum(node.SelectSingleNode("POLITICALFREEDOM").InnerText));

                return new Freedom(civil, economy, political);
            }
        }

        /// <summary>
        /// Gets the nation's full name.
        /// </summary>
        public string FullName
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=fullname")
                    .SelectSingleNode("/NATION/FULLNAME")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's vote on current General Assembly bill.
        /// </summary>
        public WAVote GAVote
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=gavote")
                    .SelectSingleNode("/NATION/GAVOTE");

                if (node.InnerText == string.Empty)
                {
                    return WAVote.Null;
                }
                else
                {
                    return (WAVote)Enum.Parse(typeof(WAVote), Utility.Capitalise(node.InnerText));
                }
            }
        }

        /// <summary>
        /// Gets the nation's GDP in standard dollars.
        /// </summary>
        public ulong GDP
        {
            get
            {
                return ulong.Parse(Utility.ParseDocument($"nation={this.Name}&q=gdp")
                    .SelectSingleNode("/NATION/GDP")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's government funding in various departments.
        /// </summary>
        public Government Government
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=govt")
                    .SelectSingleNode("/NATION/GOVT");

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

                return new Government(administration, defence, education, environment, healthcare, commerce, internationalAid, lawAndOrder, publicTransport, socialEquality, spirituality, welfare);
            }
        }

        /// <summary>
        /// Gets a description of the nation's government.
        /// </summary>
        public string GovernmentDescription
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=govtdesc")
                    .SelectSingleNode("/NATION/GOVTDESC")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the department that the government funds the most.
        /// </summary>
        public string GovernmentPriority
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=govtpriority")
                    .SelectSingleNode("/NATION/GOVTPRIORITY")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets a list of recent events in the nation.
        /// </summary>
        public HashSet<Event> Happenings
        {
            get
            {
                return Utility.ParseEvents(Utility.ParseDocument($"nation={this.Name}&q=happenings")
                    .SelectSingleNode("/NATION/HAPPENINGS"));
            }
        }

        /// <summary>
        /// Gets the income of the nation's average citizen in standard dollars.
        /// </summary>
        public long Income
        {
            get
            {
                return long.Parse(Utility.ParseDocument($"nation={this.Name}&q=income")
                    .SelectSingleNode("/NATION/INCOME")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a description of the nation's industry.
        /// </summary>
        public string IndustryDescription
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=industrydesc")
                    .SelectSingleNode("/NATION/INDUSTRYDESC")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's influence level.
        /// </summary>
        public Influence Influence
        {
            get
            {
                return (Influence)Enum.Parse(typeof(Influence), Utility.FormatForEnum(Utility.Capitalise(Utility.ParseDocument($"nation={this.Name}&q=influence")
                    .SelectSingleNode("/NATION/INFLUENCE")
                    .InnerText)));
            }
        }

        /// <summary>
        /// Gets the time the nation last logged in.
        /// </summary>
        public DateTime LastLogin
        {
            get
            {
                return Utility.ParseUnix(Utility.ParseDocument($"nation={this.Name}&q=lastlogin")
                    .SelectSingleNode("/NATION/LASTLOGIN")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the name of the nation's leader.
        /// </summary>
        public string Leader
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=leader")
                    .SelectSingleNode("/NATION/LEADER")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets a list of recent legislation passed in the nation.
        /// </summary>
        public HashSet<string> Legislation
        {
            get
            {
                XmlNodeList list = Utility.ParseDocument($"nation={this.Name}&q=legislation")
                    .SelectNodes("/NATION/LEGISLATION/LAW");

                HashSet<string> legislation = new();

                foreach (XmlNode law in list)
                {
                    legislation.Add(law.InnerText);
                }

                return legislation;
            }
        }

        /// <summary>
        /// Gets the nation's largest industry.
        /// </summary>
        public Industry MajorIndustry
        {
            get
            {
                return (Industry)Enum.Parse(typeof(Industry), Utility.FormatForEnum(Utility.Capitalise(Utility.ParseDocument($"nation={this.Name}&q=majorindustry")
                    .SelectSingleNode("/NATION/MAJORINDUSTRY")
                    .InnerText)));
            }
        }

        /// <summary>
        /// Gets the nation's motto.
        /// </summary>
        public string Motto
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=motto")
                    .SelectSingleNode("/NATION/MOTTO")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets or sets the nation's name.
        /// </summary>
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
        /// Gets one the nation's notable characteristic.
        /// </summary>
        public string Notable
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=notable")
                    .SelectSingleNode("/NATION/NOTABLE")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets a list of the nation's notable characteristics.
        /// </summary>
        public HashSet<string> Notables
        {
            get
            {
                XmlNodeList list = Utility.ParseDocument($"nation={this.Name}&q=notables")
                    .SelectNodes("/NATION/NOTABLES/NOTABLE");

                HashSet<string> notables = new();

                foreach (XmlNode notable in list)
                {
                    notables.Add(notable.InnerText);
                }

                return notables;
            }
        }

        /// <summary>
        /// Gets the list of policies implemented.
        /// </summary>
        public HashSet<Policy> Policies
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=policies")
                    .SelectSingleNode("/NATION/POLICIES");

                HashSet<Policy> policies = new();

                foreach (XmlNode policy in node.ChildNodes)
                {
                    string name = policy.SelectSingleNode("NAME").InnerText;
                    PolicyCategory category = (PolicyCategory)Enum.Parse(typeof(PolicyCategory), Utility.FormatForEnum(Utility.Capitalise(policy.SelectSingleNode("CAT").InnerText.Replace("&", "and"))));
                    string desc = policy.SelectSingleNode("DESC").InnerText;

                    policies.Add(new Policy(name, category, desc));
                }

                return policies;
            }
        }

        /// <summary>
        /// Gets the average income of the poorest 10% in standard dollars.
        /// </summary>
        public long Poorest
        {
            get
            {
                return long.Parse(Utility.ParseDocument($"nation={this.Name}&q=poorest")
                    .SelectSingleNode("/NATION/POOREST")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's population in millions.
        /// </summary>
        public long Population
        {
            get
            {
                return long.Parse(Utility.ParseDocument($"nation={this.Name}&q=population")
                    .SelectSingleNode("/NATION/POPULATION")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the percentage of the public sector in the nation's economy.
        /// </summary>
        public double PublicSector
        {
            get
            {
                return double.Parse(Utility.ParseDocument($"nation={this.Name}&q=publicsector")
                    .SelectSingleNode("/NATION/PUBLICSECTOR")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's region.
        /// </summary>
        public string Region
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=region")
                    .SelectSingleNode("/NATION/REGION")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the name of national religion.
        /// </summary>
        public string Religion
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=religion")
                    .SelectSingleNode("/NATION/RELIGION")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the average income of the richest 10% in standard dollars.
        /// </summary>
        public long Richest
        {
            get
            {
                return long.Parse(Utility.ParseDocument($"nation={this.Name}&q=richest")
                    .SelectSingleNode("/NATION/RICHEST")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's vote on current Security Council bill.
        /// </summary>
        public WAVote SCVote
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=scvote")
                    .SelectSingleNode("/NATION/SCVOTE");

                if (node.InnerText == string.Empty)
                {
                    return WAVote.Null;
                }
                else
                {
                    return (WAVote)Enum.Parse(typeof(WAVote), Utility.Capitalise(node.InnerText));
                }
            }
        }

        /// <summary>
        /// Gets the list of sectors and their share in the economy.
        /// </summary>
        public HashSet<Sector> Sectors
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=sectors")
                    .SelectSingleNode("/NATION/SECTORS");

                HashSet<Sector> sectors = new();

                foreach (XmlNode sector in node.ChildNodes)
                {
                    sectors.Add(new Sector((SectorType)Enum.Parse(typeof(SectorType), Utility.FormatForEnum(Utility.Capitalise(sector.Name))), double.Parse(sector.InnerText)));
                }

                return sectors;
            }
        }

        /// <summary>
        /// Gets the nation's sensibilities.
        /// </summary>
        public string Sensibilities
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=sensibilities")
                    .SelectSingleNode("/NATION/SENSIBILITIES")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's tax rate.
        /// </summary>
        public double Tax
        {
            get
            {
                return double.Parse(Utility.ParseDocument($"nation={this.Name}&q=tax")
                    .SelectSingleNode("/NATION/TAX")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the nation accepts recruitment telegrams.
        /// </summary>
        public bool TelegramCanRecruit
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=tgcanrecruit")
                    .SelectSingleNode("/NATION/TGCANRECRUIT")
                    .InnerText == "1";
            }
        }

        /// <summary>
        /// Gets a value indicating whether the nation accepts campaign telegrams.
        /// </summary>
        public bool TelegramCanCampaign
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=tgcancampaign")
                    .SelectSingleNode("/NATION/TGCANCAMPAIGN")
                    .InnerText == "1";
            }
        }

        /// <summary>
        /// Gets the nation's type.
        /// </summary>
        public string Type
        {
            get
            {
                return Utility.ParseDocument($"nation={this.Name}&q=type")
                    .SelectSingleNode("/NATION/TYPE")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's World Assembly status.
        /// </summary>
        public WAStatus WA
        {
            get
            {
                return (WAStatus)Enum.Parse(typeof(WAStatus), Utility.FormatForEnum(Utility.Capitalise(Utility.ParseDocument($"nation={this.Name}&q=wa")
                    .SelectSingleNode("/NATION/UNSTATUS")
                    .InnerText
                    .Replace("WA ", string.Empty))));
            }
        }

        /// <summary>
        /// Gets the list of commendations/condemnations received.
        /// </summary>
        public HashSet<WABadge> WABadges
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=wabadges")
                    .SelectSingleNode("/NATION/WABADGES");

                HashSet<WABadge> wabadges = new();

                foreach (XmlNode badge in node.ChildNodes)
                {
                    WABadgeType type = (WABadgeType)Enum.Parse(typeof(WABadgeType), Utility.Capitalise(badge.Attributes["type"].Value));
                    long id = long.Parse(badge.InnerText);

                    wabadges.Add(new WABadge(type, id));
                }

                return wabadges;
            }
        }

        /// <summary>
        /// Gets the nation's Z-Day information.
        /// </summary>
        public NationZombie Zombie
        {
            get
            {
                XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=zombie")
                    .SelectSingleNode("/NATION/ZOMBIE");

                ZombieAction action = (ZombieAction)Enum.Parse(typeof(ZombieAction), Utility.Capitalise(node.SelectSingleNode("ZACTION").InnerText));
                ZombieAction? intendedAction = (node.SelectSingleNode("ZACTIONINTENDED").InnerText == string.Empty) ? null : (ZombieAction)Enum.Parse(typeof(ZombieAction), Utility.Capitalise(node.SelectSingleNode("ZACTIONINTENDED").InnerText));
                long survivors = long.Parse(node.SelectSingleNode("SURVIVORS").InnerText);
                long zombies = long.Parse(node.SelectSingleNode("ZOMBIES").InnerText);
                long dead = long.Parse(node.SelectSingleNode("DEAD").InnerText);

                return new NationZombie(action, intendedAction, survivors, zombies, dead);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Nation"/> class.
        /// </summary>
        /// <param name="name">The name of the nation.</param>
        public Nation(string name)
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
            XmlNode node = Utility.ParseDocument($"nation={this.Name}&q=census&scale=all&mode=history{((start != null) ? "&from=" + Utility.ConvertToUnix((DateTime)start) : string.Empty)}{((end != null) ? "&to=" + Utility.ConvertToUnix((DateTime)end) : string.Empty)}")
                .SelectSingleNode("/NATION/CENSUS");

            HashSet<CensusRecord> records = new();

            foreach (XmlNode scale in node.ChildNodes)
            {
                int id = int.Parse(scale.Attributes["id"].Value);

                foreach (XmlNode point in scale.ChildNodes)
                {
                    double score = double.Parse(point.SelectSingleNode("SCORE").InnerText);
                    DateTime timeStamp = Utility.ParseUnix(point.SelectSingleNode("TIMESTAMP").InnerText);

                    records.Add(new(id, score, timeStamp));
                }
            }

            return records;
        }
    }
}
