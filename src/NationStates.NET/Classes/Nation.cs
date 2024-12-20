﻿namespace NationStates.NET
{
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents a nation.
    /// </summary>
    public class Nation
    {
        private static readonly Dictionary<string, NoticeType> NoticeTypeDict = new()
        {
            { "U", NoticeType.Banner },
            { "C", NoticeType.Card },
            { "DM", NoticeType.DispatchMention },
            { "DP", NoticeType.DispatchPin },
            { "DQ", NoticeType.DispatchQuote },
            { "EMB", NoticeType.Embassy },
            { "END", NoticeType.EndorsementGained },
            { "UNEND", NoticeType.EndorsementLost },
            { "I", NoticeType.Issue },
            { "P", NoticeType.Policy },
            { "T", NoticeType.Rank },
            { "RMBL", NoticeType.RMBLike },
            { "RMBM", NoticeType.RMBMention },
            { "RMBQ", NoticeType.RMBQuote },
            { "TG", NoticeType.Telegram },
        };

        private string _Name;

        private string? _Pin;

        private bool nameSet = false;

        /// <summary>
        /// Gets a list of the nation's admirables.
        /// </summary>
        [JsonProperty]
        public HashSet<string> Admirables
        {
            get
            {
                XmlNodeList doc = ParseXMLDocument($"nation={this.Name}&q=admirables")
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
        [JsonProperty]
        public string Animal
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=animal")
                    .SelectSingleNode("/NATION/ANIMAL")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the national animal's trait.
        /// </summary>
        [JsonProperty]
        public string AnimalTrait
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=animaltrait")
                    .SelectSingleNode("/NATION/ANIMALTRAIT")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the number of issues answered.
        /// </summary>
        [JsonProperty]
        public int Answered
        {
            get
            {
                return int.Parse(ParseXMLDocument($"nation={this.Name}&q=answered")
                    .SelectSingleNode("/NATION/ISSUES_ANSWERED")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a list of asks the nation has placed.
        /// </summary>
        [JsonProperty]
        public HashSet<Market> Asks
        {
            get
            {
                HashSet<Market> asks = new();

                foreach (XmlNode ask in ParseXMLDocument($"q=cards+asksbids;nationname={this.Name}").SelectNodes("/CARDS/ASKS/ASK"))
                {
                    long id = long.Parse(ask.SelectSingleNode("CARDID").InnerText);
                    int season = int.Parse(ask.SelectSingleNode("SEASON").InnerText);
                    double price = double.Parse(ask.SelectSingleNode("ASK_PRICE").InnerText);
                    DateTime timeStamp = ParseUnix(ask.SelectSingleNode("TIME_PLACED").InnerText);

                    asks.Add(new(id, season, this.Name, price, timeStamp, MarketType.Ask));
                }

                return asks;
            }
        }

        /// <summary>
        /// Gets the nation's bank.
        /// </summary>
        [JsonProperty]
        public double Bank
        {
            get
            {
                return double.Parse(ParseXMLDocument($"q=cards+info;nationname={this.Name}")
                    .SelectSingleNode("/CARDS/INFO/BANK")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a list of the nation's banners that can be displayed.
        /// </summary>
        [JsonProperty]
        public HashSet<Banner> Banners
        {
            get
            {
                XmlNodeList doc = ParseXMLDocument($"nation={this.Name}&q=banners")
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
        /// Gets a list of bids the nation has placed.
        /// </summary>
        public HashSet<Market> Bids
        {
            get
            {
                HashSet<Market> bids = new();

                foreach (XmlNode bid in ParseXMLDocument($"q=cards+asksbids;nationname={this.Name}").SelectNodes("/CARDS/BIDS/BID"))
                {
                    long id = long.Parse(bid.SelectSingleNode("CARDID").InnerText);
                    int season = int.Parse(bid.SelectSingleNode("SEASON").InnerText);
                    double price = double.Parse(bid.SelectSingleNode("BID_PRICE").InnerText);
                    DateTime timeStamp = ParseUnix(bid.SelectSingleNode("TIME_PLACED").InnerText);

                    bids.Add(new(id, season, this.Name, price, timeStamp, MarketType.Bid));
                }

                return bids;
            }
        }

        /// <summary>
        /// Gets the name of the nation's capital city.
        /// </summary>
        [JsonProperty]
        public string Capital
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=capital")
                    .SelectSingleNode("/NATION/CAPITAL")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the type of nation.
        /// </summary>
        [JsonProperty]
        public NationCategory Category
        {
            get
            {
                return (NationCategory)ParseEnum(typeof(NationCategory), ParseXMLDocument($"nation={this.Name}&q=category")
                    .SelectSingleNode("/NATION/CATEGORY")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's census data.
        /// </summary>
        [JsonProperty]
        public HashSet<CensusNation> Census
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name.Replace(" ", "_")};q=census;scale=all;mode=score+rank+rrank+prank+prrank")
                    .SelectSingleNode("/NATION/CENSUS");

                HashSet<CensusNation> census = new();

                foreach (XmlNode scale in node.ChildNodes)
                {
                    int id = int.Parse(scale.Attributes["id"].Value);
                    double score = double.Parse(scale.SelectSingleNode("SCORE").InnerText);
                    long worldRank = long.Parse(scale.SelectSingleNode("RANK").InnerText);
                    long regionRank = long.Parse(scale.SelectSingleNode("RRANK").InnerText);
                    double worldPercentage = double.Parse(scale.SelectSingleNode("PRANK").InnerText);
                    double regionPercentage = double.Parse(scale.SelectSingleNode("PRRANK").InnerText);

                    census.Add(new CensusNation(id, this.Name, score, worldRank, regionRank, worldPercentage, regionPercentage));
                }

                return census;
            }
        }

        /// <summary>
        /// Gets a list of collections the nation has created.
        /// </summary>
        [JsonProperty]
        public HashSet<Collection> Collections
        {
            get
            {
                HashSet<Collection> collections = new();

                foreach (XmlNode collection in ParseXMLDocument($"q=cards+collections;nationname={this.Name}").SelectNodes("/CARDS/COLLECTIONS/COLLECTION"))
                {
                    long id = long.Parse(collection.SelectSingleNode("COLLECTIONID").InnerText);
                    DateTime lastUpdated = ParseUnix(collection.SelectSingleNode("LAST_UPDATED").InnerText);
                    string name = collection.SelectSingleNode("NAME").InnerText;

                    collections.Add(new(this.Name, id, lastUpdated, name));
                }

                return collections;
            }
        }

        /// <summary>
        /// Gets the name of the nation's currency.
        /// </summary>
        [JsonProperty]
        public string Currency
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=currency")
                    .SelectSingleNode("/NATION/CURRENCY")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's custom capital.
        /// </summary>
        [JsonProperty]
        public string CustomCapital
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=customcapital")
                    .SelectSingleNode("/NATION/CAPITAL")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's custom leader.
        /// </summary>
        [JsonProperty]
        public string CustomLeader
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=customleader")
                    .SelectSingleNode("/NATION/LEADER")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's custom religion.
        /// </summary>
        [JsonProperty]
        public string CustomReligion
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=customreligion")
                    .SelectSingleNode("/NATION/RELIGION")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's database ID.
        /// </summary>
        [JsonProperty]
        public long DBID
        {
            get
            {
                return long.Parse(ParseXMLDocument($"nation={this.Name}&q=dbid")
                    .SelectSingleNode("/NATION/DBID")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the causes of deaths and their frequency.
        /// </summary>
        [JsonProperty]
        public HashSet<Death> Deaths
        {
            get
            {
                XmlNodeList list = ParseXMLDocument($"nation={this.Name}&q=deaths")
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
        /// Gets the nation's deck as a tuple (card, number of copies). Will take pretty darn long to complete.
        /// </summary>
        [JsonProperty]
        public HashSet<(Card, int)> Deck
        {
            get
            {
                // (ID, season), copies
                Dictionary<(long, int), int> count = new();

                foreach (XmlNode card in ParseXMLDocument($"q=cards+deck;nationname={this.Name}").SelectNodes("/CARDS/DECK/CARD"))
                {
                    long id = long.Parse(card.SelectSingleNode("CARDID").InnerText);
                    int season = int.Parse(card.SelectSingleNode("SEASON").InnerText);

                    (long, int) info = (id, season);

                    if (count.ContainsKey(info))
                    {
                        count[info]++;
                    }
                    else
                    {
                        count.Add(info, 1);
                    }
                }

                return count.Select(i => (new Card(i.Key.Item1, i.Key.Item2), i.Value)).ToHashSet();
            }
        }

        /// <summary>
        /// Gets the nation's deck capacity.
        /// </summary>
        [JsonProperty]
        public int DeckCapacity
        {
            get
            {
                return int.Parse(ParseXMLDocument($"q=cards+info;nationname={this.Name}")
                    .SelectSingleNode("/CARDS/INFO/DECK_CAPACITY_RAW")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's deck value.
        /// </summary>
        [JsonProperty]
        public double DeckValue
        {
            get
            {
                return double.Parse(ParseXMLDocument($"q=cards+info;nationname={this.Name}")
                    .SelectSingleNode("/CARDS/INFO/DECK_VALUE")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's demonym (adjective).
        /// </summary>
        [JsonProperty]
        public string DemonymAdjective
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=demonym")
                    .SelectSingleNode("/NATION/DEMONYM")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's demonym (noun).
        /// </summary>
        [JsonProperty]
        public string DemonymNoun
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=demonym2")
                    .SelectSingleNode("/NATION/DEMONYM2")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's demonym (plural).
        /// </summary>
        [JsonProperty]
        public string DemonymPlural
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=demonym2plural")
                    .SelectSingleNode("/NATION/DEMONYM2PLURAL")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the number of dispatches authored by the nation.
        /// </summary>
        [JsonProperty]
        public int Dispatches
        {
            get
            {
                return int.Parse(ParseXMLDocument($"nation={this.Name}&q=dispatches")
                    .SelectSingleNode("/NATION/DISPATCHES")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the list of dispatches authored by the nation.
        /// </summary>
        [JsonProperty]
        public HashSet<Dispatch> DispatchList
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=dispatchlist")
                    .SelectSingleNode("/NATION/DISPATCHLIST");

                HashSet<Dispatch> dispatchList = new();

                foreach (XmlNode dispatch in node.ChildNodes)
                {
                    ulong id = ulong.Parse(dispatch.Attributes["id"].Value);
                    string title = dispatch.SelectSingleNode("TITLE").InnerText;
                    DispatchCategory category = (DispatchCategory)ParseEnum(typeof(DispatchCategory), dispatch.SelectSingleNode("CATEGORY").InnerText);
                    string author = dispatch.SelectSingleNode("AUTHOR").InnerText;
                    dynamic subcategory = null;

                    subcategory = category switch
                    {
                        DispatchCategory.Account => (DispatchAccount)ParseEnum(typeof(DispatchAccount), dispatch.SelectSingleNode("SUBCATEGORY").InnerText),
                        DispatchCategory.Bulletin => (DispatchBulletin)ParseEnum(typeof(DispatchBulletin), dispatch.SelectSingleNode("SUBCATEGORY").InnerText),
                        DispatchCategory.Factbook => (DispatchFactbook)ParseEnum(typeof(DispatchFactbook), dispatch.SelectSingleNode("SUBCATEGORY").InnerText),
                        DispatchCategory.Meta => (DispatchMeta)ParseEnum(typeof(DispatchMeta), dispatch.SelectSingleNode("SUBCATEGORY").InnerText),
                        _ => throw new NSError("Dispatch subcategory does not exist."),
                    };
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
        [JsonProperty]
        public HashSet<string> Endorsements
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=endorsements")
                    .SelectSingleNode("/NATION/ENDORSEMENTS")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets the number of factbooks authored by the nation.
        /// </summary>
        [JsonProperty]
        public int Factbooks
        {
            get
            {
                return int.Parse(ParseXMLDocument($"nation={this.Name}&q=factbooks")
                    .SelectSingleNode("/NATION/FACTBOOKS")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the time the nation first logged in.
        /// </summary>
        [JsonProperty]
        public DateTime FirstLogin
        {
            get
            {
                return ParseUnix(ParseXMLDocument($"nation={this.Name}&q=firstlogin")
                    .SelectSingleNode("/NATION/FIRSTLOGIN")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the URL of the nation's flag.
        /// </summary>
        [JsonProperty]
        public string Flag
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=flag")
                    .SelectSingleNode("/NATION/FLAG")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the time the nation was founded.
        /// </summary>
        [JsonProperty]
        public DateTime Founded
        {
            get
            {
                return ParseUnix(ParseXMLDocument($"nation={this.Name}&q=foundedtime")
                    .SelectSingleNode("/NATION/FOUNDEDTIME")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the level of the nation's civil rights, economy and political freedoms.
        /// </summary>
        [JsonProperty]
        public Freedom Freedom
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=freedom")
                    .SelectSingleNode("/NATION/FREEDOM");

                CivilRights civil = (CivilRights)ParseEnum(typeof(CivilRights), node.SelectSingleNode("CIVILRIGHTS").InnerText);
                Economy economy = (Economy)ParseEnum(typeof(Economy), node.SelectSingleNode("ECONOMY").InnerText);
                PoliticalFreedoms political = (PoliticalFreedoms)ParseEnum(typeof(PoliticalFreedoms), node.SelectSingleNode("POLITICALFREEDOM").InnerText);

                return new Freedom(civil, economy, political);
            }
        }

        /// <summary>
        /// Gets the nation's full name.
        /// </summary>
        [JsonProperty]
        public string FullName
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=fullname")
                    .SelectSingleNode("/NATION/FULLNAME")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's vote on current General Assembly bill.
        /// </summary>
        [JsonProperty]
        public Vote? GAVote
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=gavote")
                    .SelectSingleNode("/NATION/GAVOTE");

                if (node.InnerText == string.Empty)
                {
                    return null;
                }
                else
                {
                    return (Vote)ParseEnum(typeof(Vote), node.InnerText);
                }
            }
        }

        /// <summary>
        /// Gets the nation's GDP in standard dollars.
        /// </summary>
        [JsonProperty]
        public ulong GDP
        {
            get
            {
                return ulong.Parse(ParseXMLDocument($"nation={this.Name}&q=gdp")
                    .SelectSingleNode("/NATION/GDP")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's government funding in various departments.
        /// </summary>
        [JsonProperty]
        public Government Government
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=govt")
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
        [JsonProperty]
        public string GovernmentDescription
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=govtdesc")
                    .SelectSingleNode("/NATION/GOVTDESC")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the department that the government funds the most.
        /// </summary>
        [JsonProperty]
        public string GovernmentPriority
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=govtpriority")
                    .SelectSingleNode("/NATION/GOVTPRIORITY")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets a list of recent events in the nation.
        /// </summary>
        [JsonProperty]
        public HashSet<Event> Happenings
        {
            get
            {
                return ParseEvents(ParseXMLDocument($"nation={this.Name}&q=happenings")
                    .SelectSingleNode("/NATION/HAPPENINGS"));
            }
        }

        /// <summary>
        /// Gets the income of the nation's average citizen in standard dollars.
        /// </summary>
        [JsonProperty]
        public long Income
        {
            get
            {
                return long.Parse(ParseXMLDocument($"nation={this.Name}&q=income")
                    .SelectSingleNode("/NATION/INCOME")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a description of the nation's industry.
        /// </summary>
        [JsonProperty]
        public string IndustryDescription
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=industrydesc")
                    .SelectSingleNode("/NATION/INDUSTRYDESC")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's influence level.
        /// </summary>
        [JsonProperty]
        public Influence Influence
        {
            get
            {
                return (Influence)ParseEnum(typeof(Influence), ParseXMLDocument($"nation={this.Name}&q=influence")
                    .SelectSingleNode("/NATION/INFLUENCE")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the issues that the nation faces.
        /// </summary>
        public HashSet<Issue> Issues
        {
            get
            {
                HashSet<Issue> issues = new();

                foreach (XmlElement issue in ParseXMLDocument($"nation={this.Name}&q=issues", this.Pin).SelectNodes("/NATION/ISSUES/ISSUE"))
                {
                    int id = int.Parse(issue.Attributes["id"].Value);
                    string title = issue.SelectSingleNode("TITLE").InnerText;
                    string text = issue.SelectSingleNode("TEXT").InnerText;
                    string author = issue.SelectSingleNode("AUTHOR").InnerText;
                    string editor = issue.SelectSingleNode("EDITOR").InnerText;
                    List<string> options = new();

                    foreach (XmlElement option in issue.SelectNodes("OPTION"))
                    {
                        options.Add(option.InnerText);
                    }

                    issues.Add(new(id, title, text, options, author, editor));
                }

                return issues;
            }
        }

        /// <summary>
        /// Gets the time the nation last logged in.
        /// </summary>
        [JsonProperty]
        public DateTime LastLogin
        {
            get
            {
                return ParseUnix(ParseXMLDocument($"nation={this.Name}&q=lastlogin")
                    .SelectSingleNode("/NATION/LASTLOGIN")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the time when the nation last opened a card pack.
        /// </summary>
        [JsonProperty]
        public DateTime LastPackOpened
        {
            get
            {
                return ParseUnix(ParseXMLDocument($"q=cards+info;nationname={this.Name}")
                    .SelectSingleNode("/CARDS/INFO/LAST_PACK_OPENED")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the time when the nation's stats were last updated.
        /// </summary>
        [JsonProperty]
        public DateTime LastUpdated
        {
            get
            {
                return ParseUnix(ParseXMLDocument($"q=cards+info;nationname={this.Name}")
                    .SelectSingleNode("/CARDS/INFO/LAST_VALUED")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the name of the nation's leader.
        /// </summary>
        [JsonProperty]
        public string Leader
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=leader")
                    .SelectSingleNode("/NATION/LEADER")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets a list of recent legislation passed in the nation.
        /// </summary>
        [JsonProperty]
        public HashSet<string> Legislation
        {
            get
            {
                XmlNodeList list = ParseXMLDocument($"nation={this.Name}&q=legislation")
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
        [JsonProperty]
        public Industry MajorIndustry
        {
            get
            {
                return (Industry)ParseEnum(typeof(Industry), ParseXMLDocument($"nation={this.Name}&q=majorindustry")
                    .SelectSingleNode("/NATION/MAJORINDUSTRY")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's motto.
        /// </summary>
        [JsonProperty]
        public string Motto
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=motto")
                    .SelectSingleNode("/NATION/MOTTO")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets or sets the nation's name.
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

                    // Check if nation exists.
                    try
                    {
                        _ = this.Animal;
                    }
                    catch
                    {
                        throw new NSError($"Nation \"{this._Name}\" does not exist.");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the list of the nations in the nation's dossier.
        /// </summary>
        public HashSet<string> NationDossier
        {
            get
            {
                HashSet<string> dossier = new();

                foreach (XmlElement nation in ParseXMLDocument($"nation={this.Name}&q=dossier", this.Pin).SelectNodes("/NATION/DOSSIER/NATION"))
                {
                    dossier.Add(nation.InnerText);
                }

                return dossier;
            }
        }

        /// <summary>
        /// Gets the time at which the nation will encounter the next issue.
        /// </summary>
        public DateTime NextIssueTime
        {
            get
            {
                return ParseUnix(ParseXMLDocument($"nation={this.Name}&q=nextissuetime", this.Pin).SelectSingleNode("/NATION/NEXTISSUETIME").InnerText);
            }
        }

        /// <summary>
        /// Gets a list of the nation's notable characteristics.
        /// </summary>
        [JsonProperty]
        public HashSet<string> Notables
        {
            get
            {
                XmlNodeList list = ParseXMLDocument($"nation={this.Name}&q=notables")
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
        /// Gets the nation's N-Day information.
        /// </summary>
        [JsonProperty]
        public NDayNation Nuke
        {
            get
            {
                HtmlNode node = Utility.ParseHTMLDocument($"https://www.nationstates.net/nation={this.Name}/page=nukes");
                HtmlNode factionNode = node.SelectSingleNode(".//div[@class='nukenfaction']/p/a[@class='factionname']");
                string? faction = factionNode?.InnerText;
                long incoming = long.Parse(node.SelectSingleNode(".//a[@title='Incoming']").InnerText.Replace("INCOMING", string.Empty).Replace(",", string.Empty));
                long intercepts = long.Parse(node.SelectSingleNode(".//a[@title='Intercepts']").InnerText.Replace("INTERCEPTS", string.Empty).Replace(",", string.Empty));
                long launches = long.Parse(node.SelectSingleNode(".//a[@title='Launches']").InnerText.Replace("LAUNCHES", string.Empty).Replace(",", string.Empty));
                long nukes = long.Parse(node.SelectSingleNode(".//a[@title='Nukes']").InnerText.Replace("NUKES", string.Empty).Replace(",", string.Empty));
                int production = int.Parse(node.SelectSingleNode(".//a[@title='Production']").InnerText.Replace("PRODUCTION", string.Empty).Replace(",", string.Empty));
                int radiation = int.Parse(node.SelectSingleNode(".//a[@title='Radiation']").InnerText.Replace("RADIATION", string.Empty).Replace("%", string.Empty));
                long shields = long.Parse(node.SelectSingleNode(".//a[@title='Shield']").InnerText.Replace("SHIELD", string.Empty).Replace(",", string.Empty));
                long strikes = long.Parse(node.SelectSingleNode(".//a[@title='Strikes']").InnerText.Replace("STRIKES", string.Empty).Replace(",", string.Empty));
                long targeted = long.Parse(node.SelectSingleNode(".//a[@title='Targeted']").InnerText.Replace("TARGETED", string.Empty).Replace(",", string.Empty));
                long targets = long.Parse(node.SelectSingleNode(".//a[@title='Targets']").InnerText.Replace("TARGETS", string.Empty).Replace(",", string.Empty));
                Specialty specialty = ParseEnum(typeof(Specialty), node.SelectSingleNode(".//h1[@class='nukeh1']/span[@class='fancylike']").InnerText.Replace(" Specialist", string.Empty));
                return new(new(incoming, intercepts, launches, nukes, production, radiation, shields, strikes, targeted, targets), faction, specialty);
            }
        }

        /// <summary>
        /// Gets the number of cards the nation owns.
        /// </summary>
        [JsonProperty]
        public int NumberOfCards
        {
            get
            {
                return int.Parse(ParseXMLDocument($"q=cards+info;nationname={this.Name}")
                    .SelectSingleNode("/CARDS/INFO/NUM_CARDS")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the number of unopened trading card packs the nation has.
        /// </summary>
        public int Packs
        {
            get
            {
                return int.Parse(ParseXMLDocument($"nation={this.Name}&q=packs", this.Pin).SelectSingleNode("NATION/PACKS").InnerText);
            }
        }

        /// <summary>
        /// Gets or sets the nation's pin code for safer use of private shards and commands. To set it, input the nation's password. The password will not be stored.
        /// </summary>
        public string? Pin
        {
            get
            {
                return this._Pin;
            }

            set
            {
                Dictionary<string, string> headers = new()
                {
                    { "X-Password", value },
                };

                this._Pin = GetResponseHeaders($"nation={this.Name}&q=unread", headers).GetValues("X-Pin").First();
            }
        }

        /// <summary>
        /// Gets the list of policies implemented.
        /// </summary>
        [JsonProperty]
        public HashSet<Policy> Policies
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=policies")
                    .SelectSingleNode("/NATION/POLICIES");

                HashSet<Policy> policies = new();

                foreach (XmlNode policy in node.ChildNodes)
                {
                    string name = policy.SelectSingleNode("NAME").InnerText;
                    PolicyCategory category = (PolicyCategory)ParseEnum(typeof(PolicyCategory), policy.SelectSingleNode("CAT").InnerText.Replace("&", "and"));
                    string desc = policy.SelectSingleNode("DESC").InnerText;

                    policies.Add(new Policy(name, category, desc));
                }

                return policies;
            }
        }

        /// <summary>
        /// Gets the average income of the poorest 10% in standard dollars.
        /// </summary>
        [JsonProperty]
        public long Poorest
        {
            get
            {
                return long.Parse(ParseXMLDocument($"nation={this.Name}&q=poorest")
                    .SelectSingleNode("/NATION/POOREST")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's population in millions.
        /// </summary>
        [JsonProperty]
        public long Population
        {
            get
            {
                return long.Parse(ParseXMLDocument($"nation={this.Name}&q=population")
                    .SelectSingleNode("/NATION/POPULATION")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the percentage of the public sector in the nation's economy.
        /// </summary>
        [JsonProperty]
        public double PublicSector
        {
            get
            {
                return double.Parse(ParseXMLDocument($"nation={this.Name}&q=publicsector")
                    .SelectSingleNode("/NATION/PUBLICSECTOR")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's region.
        /// </summary>
        [JsonProperty]
        public string Region
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=region")
                    .SelectSingleNode("/NATION/REGION")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the list of the regions in the nation's dossier.
        /// </summary>
        public HashSet<string> RegionDossier
        {
            get
            {
                HashSet<string> dossier = new();

                foreach (XmlElement region in ParseXMLDocument($"nation={this.Name}&q=rdossier", this.Pin).SelectNodes("/NATION/RDOSSIER/REGION"))
                {
                    dossier.Add(region.InnerText);
                }

                return dossier;
            }
        }

        /// <summary>
        /// Gets the name of national religion.
        /// </summary>
        [JsonProperty]
        public string Religion
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=religion")
                    .SelectSingleNode("/NATION/RELIGION")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the average income of the richest 10% in standard dollars.
        /// </summary>
        [JsonProperty]
        public long Richest
        {
            get
            {
                return long.Parse(ParseXMLDocument($"nation={this.Name}&q=richest")
                    .SelectSingleNode("/NATION/RICHEST")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the nation's vote on current Security Council bill.
        /// </summary>
        [JsonProperty]
        public Vote? SCVote
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=scvote")
                    .SelectSingleNode("/NATION/SCVOTE");

                if (node.InnerText == string.Empty)
                {
                    return null;
                }
                else
                {
                    return (Vote)ParseEnum(typeof(Vote), node.InnerText);
                }
            }
        }

        /// <summary>
        /// Gets the list of sectors and their share in the economy.
        /// </summary>
        [JsonProperty]
        public HashSet<Sector> Sectors
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=sectors")
                    .SelectSingleNode("/NATION/SECTORS");

                HashSet<Sector> sectors = new();

                foreach (XmlNode sector in node.ChildNodes)
                {
                    sectors.Add(new Sector((SectorType)ParseEnum(typeof(SectorType), sector.Name), double.Parse(sector.InnerText)));
                }

                return sectors;
            }
        }

        /// <summary>
        /// Gets the nation's sensibilities.
        /// </summary>
        [JsonProperty]
        public string Sensibilities
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=sensibilities")
                    .SelectSingleNode("/NATION/SENSIBILITIES")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's tax rate.
        /// </summary>
        [JsonProperty]
        public double Tax
        {
            get
            {
                return double.Parse(ParseXMLDocument($"nation={this.Name}&q=tax")
                    .SelectSingleNode("/NATION/TAX")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the nation accepts campaign telegrams.
        /// </summary>
        [JsonProperty]
        public bool TelegramCanCampaign
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=tgcancampaign")
                    .SelectSingleNode("/NATION/TGCANCAMPAIGN")
                    .InnerText == "1";
            }
        }

        /// <summary>
        /// Gets a value indicating whether the nation accepts recruitment telegrams.
        /// </summary>
        [JsonProperty]
        public bool TelegramCanRecruit
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=tgcanrecruit")
                    .SelectSingleNode("/NATION/TGCANRECRUIT")
                    .InnerText == "1";
            }
        }

        /// <summary>
        /// Gets the nation's type.
        /// </summary>
        [JsonProperty]
        public string Type
        {
            get
            {
                return ParseXMLDocument($"nation={this.Name}&q=type")
                    .SelectSingleNode("/NATION/TYPE")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the nation's unread stuff.
        /// </summary>
        public Unread Unread
        {
            get
            {
                XmlElement node = ParseXMLDocument($"nation={this.Name}&q=unread", this.Pin);

                int issues = int.Parse(node.SelectSingleNode("/NATION/UNREAD/ISSUES").InnerText);
                int telegrams = int.Parse(node.SelectSingleNode("/NATION/UNREAD/TELEGRAMS").InnerText);
                int notices = int.Parse(node.SelectSingleNode("/NATION/UNREAD/NOTICES").InnerText);
                int messages = int.Parse(node.SelectSingleNode("/NATION/UNREAD/RMB").InnerText);
                int worldAssembly = int.Parse(node.SelectSingleNode("/NATION/UNREAD/WA").InnerText);
                int news = int.Parse(node.SelectSingleNode("/NATION/UNREAD/NEWS").InnerText);

                return new(issues, telegrams, notices, messages, worldAssembly, news);
            }
        }

        /// <summary>
        /// Gets the nation's World Assembly status.
        /// </summary>
        [JsonProperty]
        public Membership WA
        {
            get
            {
                return (Membership)ParseEnum(typeof(Membership), ParseXMLDocument($"nation={this.Name}&q=wa")
                    .SelectSingleNode("/NATION/UNSTATUS")
                    .InnerText
                    .Replace("WA ", string.Empty));
            }
        }

        /// <summary>
        /// Gets the list of commendations/condemnations received.
        /// </summary>
        [JsonProperty]
        public HashSet<Badge> WABadges
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=wabadges")
                    .SelectSingleNode("/NATION/WABADGES");

                HashSet<Badge> wabadges = new();

                foreach (XmlNode badge in node.ChildNodes)
                {
                    BadgeType type = (BadgeType)ParseEnum(typeof(BadgeType), badge.Attributes["type"].Value);
                    long id = long.Parse(badge.InnerText);

                    wabadges.Add(new Badge(this.Name, type, id));
                }

                return wabadges;
            }
        }

        /// <summary>
        /// Gets the nation's Z-Day information.
        /// </summary>
        [JsonProperty]
        public ZDayNation Zombie
        {
            get
            {
                XmlNode node = ParseXMLDocument($"nation={this.Name}&q=zombie")
                    .SelectSingleNode("/NATION/ZOMBIE");

                ZombieAction action = (ZombieAction)ParseEnum(typeof(ZombieAction), node.SelectSingleNode("ZACTION").InnerText);
                ZombieAction? intendedAction = (node.SelectSingleNode("ZACTIONINTENDED").InnerText == string.Empty) ? null : (ZombieAction)ParseEnum(typeof(ZombieAction), node.SelectSingleNode("ZACTIONINTENDED").InnerText);
                long survivors = long.Parse(node.SelectSingleNode("SURVIVORS").InnerText);
                long zombies = long.Parse(node.SelectSingleNode("ZOMBIES").InnerText);
                long dead = long.Parse(node.SelectSingleNode("DEAD").InnerText);

                return new ZDayNation(action, intendedAction, survivors, zombies, dead);
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
        /// Initializes a new instance of the <see cref="Nation"/> class.
        /// </summary>
        /// <param name="id">The DBID of the nation.</param>
        public Nation(long id)
        {
            this.Name = NationNameFromID(id);
        }

        /// <summary>
        /// Add a dispatch.
        /// </summary>
        /// <param name="title">The title of the dispatch.</param>
        /// <param name="text">The content of the dispatch.</param>
        /// <param name="category">The dispatch's category.</param>
        /// <param name="subCategory">The dispatch's sub-category. The sub-category should be of the type <see cref="DispatchAccount"/>,
        /// <see cref="DispatchBulletin"/>, <see cref="DispatchFactbook"/> or <see cref="DispatchMeta"/>.</param>
        public void AddDispatch(string title, string text, DispatchCategory category, Enum subCategory)
        {
            CheckCategoryAndSubCategory(category, subCategory);

            string categoryS = $"{(int)category}";
            string subCategoryS = $"{categoryS}{AddZeroIfSingleDigitNumber((int)ParseEnum(GetDispatchCategoryFromSubCategory(subCategory), subCategory.ToString()))}";

            string token = ParseXMLDocument($"nation={this.Name}&c=dispatch&dispatch=add&title={title}&text={text}&category={categoryS}&subcategory={subCategoryS}&mode=prepare", this.Pin).SelectSingleNode("/NATION/SUCCESS").InnerText;
            ParseXMLDocument($"nation={this.Name}&c=dispatch&dispatch=add&title={title}&text={text}&category={categoryS}&subcategory={subCategoryS}&mode=execute&token={token}", this.Pin);
        }

        /// <summary>
        /// Addresses an issue.
        /// </summary>
        /// <param name="id">The issue's ID.</param>
        /// <param name="option">The option to select. Option IDs begin from 0. To dismiss the issue, input `-1`.</param>
        public void AddressIssue(int id, int option)
        {
            // TODO: Add IssueResults
            ParseXMLDocument($"nation={this.Name}&c=issue&issue={id}&option={option}", this.Pin);
        }

        /// <summary>
        /// Gets census data from a time period.
        /// </summary>
        /// <param name="start">The start of the time period as a UNIX timestamp.</param>
        /// <param name="end">The end of the time period as a UNIX timestamp.</param>
        /// <returns>A list of all census data recorded during the time period.</returns>
        public HashSet<CensusRecord> CensusHistory(DateTime? start = null, DateTime? end = null)
        {
            XmlNode node = ParseXMLDocument($"nation={this.Name}&q=census&scale=all&mode=history{((start != null) ? "&from=" + ConvertToUnix((DateTime)start) : string.Empty)}{((end != null) ? "&to=" + ConvertToUnix((DateTime)end) : string.Empty)}")
                .SelectSingleNode("/NATION/CENSUS");

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
        /// Edit a dispatch.
        /// </summary>
        /// <param name="id">The ID of the dispatch.</param>
        public void DeleteDispatch(long id)
        {
            string token = ParseXMLDocument($"nation={this.Name}&c=dispatch&dispatch=remove&dispatchid={id}&mode=prepare", this.Pin).SelectSingleNode("/NATION/SUCCESS").InnerText;
            ParseXMLDocument($"nation={this.Name}&c=dispatch&dispatch=remove&dispatchid={id}&mode=execute&token={token}", this.Pin);
        }

        /// <summary>
        /// Edit a dispatch.
        /// </summary>
        /// <param name="id">The ID of the dispatch.</param>
        /// <param name="title">The title of the dispatch.</param>
        /// <param name="text">The content of the dispatch.</param>
        /// <param name="category">The dispatch's category.</param>
        /// <param name="subCategory">The dispatch's sub-category. The sub-category should be of the type <see cref="DispatchAccount"/>,
        /// <see cref="DispatchBulletin"/>, <see cref="DispatchFactbook"/> or <see cref="DispatchMeta"/>.</param>
        public void EditDispatch(long id, string title, string text, DispatchCategory category, Enum subCategory)
        {
            CheckCategoryAndSubCategory(category, subCategory);

            string categoryS = $"{(int)category}";
            string subCategoryS = $"{categoryS}{AddZeroIfSingleDigitNumber((int)ParseEnum(GetDispatchCategoryFromSubCategory(subCategory), subCategory.ToString()))}";

            string token = ParseXMLDocument($"nation={this.Name}&c=dispatch&dispatch=edit&title={title}&text={text}&category={categoryS}&subcategory={subCategoryS}&dispatchid={id}&mode=prepare", this.Pin).SelectSingleNode("/NATION/SUCCESS").InnerText;
            ParseXMLDocument($"nation={this.Name}&c=dispatch&dispatch=edit&title={title}&text={text}&category={categoryS}&subcategory={subCategoryS}&dispatchid={id}&mode=execute&token={token}", this.Pin);
        }

        /// <summary>
        /// Gifts a trading card.
        /// </summary>
        /// <param name="cardID">The ID of the card.</param>
        /// <param name="season">The card's season.</param>
        /// <param name="receiver">The name of the nation receiving the card.</param>
        public void GiftCard(long cardID, int season, string receiver)
        {
            string token = ParseXMLDocument($"nation={this.Name}&c=giftcard&cardid={cardID}&season={season}&to={receiver}&mode=prepare", this.Pin).SelectSingleNode("/NATION/SUCCESS").InnerText;
            ParseXMLDocument($"nation={this.Name}&c=giftcard&cardid={cardID}&season={season}&to={receiver}&mode=execute&token={token}", this.Pin);
        }

        /// <summary>
        /// Gets a list of notices.
        /// </summary>
        /// <param name="from">The time to start getting notices from.</param>
        /// <returns>A list of notices.</returns>
        public HashSet<Notice> Notices(DateTime? from = null)
        {
            HashSet<Notice> notices = new();

            foreach (XmlElement notice in ParseXMLDocument($"nation={this.Name}&q=notices{(from == null ? string.Empty : "&from=" + ConvertToUnix((DateTime)from).ToString())}", this.Pin).SelectNodes("/NATION/NOTICES/NOTICE"))
            {
                string title = notice.SelectSingleNode("TITLE").InnerText;
                NoticeType type = NoticeTypeDict[notice.SelectSingleNode("TYPE").InnerText];
                string text = notice.SelectSingleNode("TEXT").InnerText;
                string url = notice.SelectSingleNode("URL").InnerText;
                DateTime timestamp = ParseUnix(notice.SelectSingleNode("TIMESTAMP").InnerText);
                string who = notice.SelectSingleNode("WHO").InnerText;
                string? whoURL = null;

                if (notice.SelectSingleNode("WHO_URL") != null)
                {
                    whoURL = notice.SelectSingleNode("WHO_URL").InnerText;
                }

                bool isNew = false;

                if (notice.SelectSingleNode("NEW") != null && notice.SelectSingleNode("NEW").InnerText == "1")
                {
                    isNew = true;
                }

                notices.Add(new(title, type, text, url, timestamp, who, whoURL, isNew));
            }

            return notices;
        }

        /// <summary>
        /// Register a login.
        /// </summary>
        /// <returns>A boolean indicating whether the ping was successful.</returns>
        public bool Ping()
        {
            return ParseXMLDocument($"nation={this.Name}&q=ping", this.Pin).SelectSingleNode("/NATION/PING").InnerText == "1";
        }

        /// <summary>
        /// Gets a JSON string representing the nation.
        /// </summary>
        /// <returns>A JSON string representing the nation.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }

        /// <summary>
        /// Verifies if the user owns the nation by comparing the checksum code from https://www.nationstates.net/page=verify_login.
        /// </summary>
        /// <param name="checksum">The checksum code to compare.</param>
        /// <returns>A boolean verifying the user's ownership over the nation.</returns>
        public bool Verify(string checksum)
        {
            return DownloadPage($"https://www.nationstates.net/cgi-bin/api.cgi?a=verify&nation={this.Name}&checksum={checksum}").Trim() == "1";
        }
    }
}