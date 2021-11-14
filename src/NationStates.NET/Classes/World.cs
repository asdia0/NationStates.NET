namespace NationStates.NET
{
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents the NationStates world.
    /// </summary>
    public static class World
    {
        private static readonly Dictionary<RegionTag, string> RegionTagDict = new()
        {
            { RegionTag.AntiCapitalist, "Anti-Capitalist" },
            { RegionTag.AntiCommunist, "Anti-Communist" },
            { RegionTag.AntiFascist, "Anti-Fascist" },
            { RegionTag.AntiGeneral_Assembly, "Anti-General Assembly" },
            { RegionTag.AntiSecurity_Council, "Anti-Secutiry Council" },
            { RegionTag.AntiWorld_Assembly, "Anti-World Assembly" },
            { RegionTag.EcoFriendly, "Eco-Friendly" },
            { RegionTag.HumanOnly, "Human-Only" },
            { RegionTag.MultiSpecies, "Multi-Species" },
            { RegionTag.NonEnglish, "Non-English" },
            { RegionTag.PostModern_Tech, "Post-Modern Tech" },
            { RegionTag.FTFTL, "FT: FTL" },
            { RegionTag.FTFTLi, "FT: FTLi" },
            { RegionTag.FTSTL, "FT: STL" },
        };

        /// <summary>
        /// Gets the auctions currently active.
        /// </summary>
        public static HashSet<Card> Auctions
        {
            get
            {
                HashSet<Card> auctions = new();

                foreach (XmlNode auction in ParseXMLDocument("q=cards+auctions").SelectNodes("/CARDS/AUCTIONS/AUCTION"))
                {
                    long id = long.Parse(auction.SelectSingleNode("CARDID").InnerText);
                    int season = int.Parse(auction.SelectSingleNode("SEASON").InnerText);

                    auctions.Add(new(id, season));
                }

                return auctions;
            }
        }

        /// <summary>
        /// Gets the census world average in each census.
        /// </summary>
        public static HashSet<CensusWorld> Census
        {
            get
            {
                HashSet<CensusWorld> censusAverages = new();

                foreach (XmlNode census in ParseXMLDocument("q=census;scale=all").SelectSingleNode("CENSUS").ChildNodes)
                {
                    int id = int.Parse(census.Attributes["id"].Value);
                    double score = double.Parse(census
                        .SelectSingleNode("SCORE")
                        .InnerText);

                    censusAverages.Add(new(id, score));
                }

                return censusAverages;
            }
        }

        /// <summary>
        /// Gets today's census ID.
        /// </summary>
        public static int CensusOfTheDay
        {
            get
            {
                return int.Parse(ParseXMLDocument("q=censusid")
                    .SelectSingleNode("CENSUSID")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a list of all factions sorted by their score.
        /// </summary>
        public static List<Faction> Factions
        {
            get
            {
                List<Faction> factions = new();

                foreach (XmlNode faction in ParseXMLDocument("q=factions").SelectSingleNode("/WORLD/FACTIONS").ChildNodes)
                {
                    factions.Add(new Faction(long.Parse(faction.Attributes["id"].Value)));
                }

                return factions;
            }
        }

        /// <summary>
        /// Gets today's featured region.
        /// </summary>
        /// <returns>Today's featured region.</returns>
        public static string FeaturedRegion
        {
            get
            {
                return ParseXMLDocument("q=featuredregion")
                    .SelectSingleNode("/WORLD/FEATUREDREGION")
                    .InnerText;
            }
        }

        /// <summary>
        /// Gets the ID of the latest event.
        /// </summary>
        public static ulong LastEventID
        {
            get
            {
                return ulong.Parse(ParseXMLDocument("q=lasteventid")
                    .SelectSingleNode("/WORLD/LASTEVENTID")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a list of all nations.
        /// </summary>
        public static HashSet<string> Nations
        {
            get
            {
                return ParseXMLDocument("q=nations")
                    .SelectSingleNode("/WORLD/NATIONS")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets the names of newly founded nations.
        /// </summary>
        public static HashSet<string> NewNations
        {
            get
            {
                return ParseXMLDocument("q=newnations")
                    .SelectSingleNode("/WORLD/NEWNATIONS")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets the total number of nations.
        /// </summary>
        public static long NumNations
        {
            get
            {
                return long.Parse(ParseXMLDocument("q=numnations")
                    .SelectSingleNode("/WORLD/NUMNATIONS")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the total number of regions.
        /// </summary>
        public static long NumRegions
        {
            get
            {
                return long.Parse(ParseXMLDocument("q=numregions")
                    .SelectSingleNode("/WORLD/NUMREGIONS")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a list of all regions.
        /// </summary>
        public static HashSet<string> Regions
        {
            get
            {
                return ParseXMLDocument("q=regions")
                    .SelectSingleNode("/WORLD/REGIONS")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets the current telegram queue.
        /// </summary>
        public static TelegramQueue TelegramQueue
        {
            get
            {
                XmlNode node = ParseXMLDocument("q=tgqueue")
                    .SelectSingleNode("/WORLD/TGQUEUE");

                long manual = long.Parse(node.SelectSingleNode("MANUAL").InnerText);
                long mass = long.Parse(node.SelectSingleNode("MASS").InnerText);
                long api = long.Parse(node.SelectSingleNode("API").InnerText);

                return new TelegramQueue(manual, mass, api);
            }
        }

        /// <summary>
        /// Gets a list of recent trades.
        /// </summary>
        /// <param name="limit">The maximum amount of trades to get.</param>
        /// <param name="sinceTime">Get trades that occurred after this time.</param>
        /// <param name="beforeTime">Get trades that occurred before this time.</param>
        /// <returns>A list of recent trades.</returns>
        public static HashSet<Trade> CardTrades(int limit = 50, DateTime? sinceTime = null, DateTime? beforeTime = null)
        {
            HashSet<Trade> trades = new();

            string url = $"q=cards+trades;limit={limit}";

            if (sinceTime != null)
            {
                url += $";sincetime={ConvertToUnix((DateTime)sinceTime)}";
            }

            if (beforeTime != null)
            {
                url += $";beforetime={ConvertToUnix((DateTime)beforeTime)}";
            }

            foreach (XmlNode trade in ParseXMLDocument(url).SelectNodes("/CARDS/TRADES/TRADE"))
            {
                string buyer = trade.SelectSingleNode("BUYER").InnerText;
                string seller = trade.SelectSingleNode("SELLER").InnerText;

                string priceS = trade.SelectSingleNode("PRICE").InnerText;
                double price = (priceS != string.Empty) ? double.Parse(priceS) : 0;

                DateTime timeStamp = ParseUnix(trade.SelectSingleNode("TIMESTAMP").InnerText);
                long id = long.Parse(trade.SelectSingleNode("CARDID").InnerText);
                int season = int.Parse(trade.SelectSingleNode("SEASON").InnerText);
                Rarity rarity = (Rarity)ParseEnum(typeof(Rarity), trade.SelectSingleNode("CATEGORY").InnerText);

                trades.Add(new(id, season, rarity, buyer, seller, price, timeStamp));
            }

            return trades;
        }

        /// <summary>
        /// Gets fifty cards in order of their value. To filter cards, <paramref name="rarity"/> should be set.
        /// </summary>
        /// <param name="start">The starting rank of the first card.</param>
        /// <param name="rarity">The sole rarity to get. Set as `null` to get cards of any rarity.</param>
        /// <returns>A list of fifty cards.</returns>
        public static HashSet<CardRank> CardsByMarketValue(int start = 1, Rarity? rarity = null)
        {
            HashSet<CardRank> ranks = new();

            bool firstDone = false;

            foreach (HtmlNode node in ParseHTMLDocument($"https://www.nationstates.net/page=deck/show_market=cards{(rarity == null ? string.Empty : "/filter=" + rarity.ToString().ToLower())}?start={start - 1}").SelectNodes("//tbody"))
            {
                foreach (HtmlNode row in node.SelectNodes("tr"))
                {
                    HtmlNodeCollection cells = row.SelectNodes("td");

                    if (firstDone)
                    {
                        long rank = long.Parse(cells[0].InnerText.Replace(".", string.Empty));

                        string info = cells[2].SelectSingleNode(".//a[@class='nref cardnameblock']").Attributes["href"].Value;

                        long id = long.Parse(info.Split("/")[2].Replace("card=", string.Empty));
                        int season = int.Parse(info.Split("/")[3].Replace("season=", string.Empty));

                        ranks.Add(new(id, season, rank));
                    }
                    else
                    {
                        firstDone = true;
                    }
                }
            }

            return ranks;
        }

        /// <summary>
        /// Gets the description for a census.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="entity">The type of entity to get the census description for.</param>
        /// <returns>The description for the specified census.</returns>
        public static string CensusDescription(int id, Entity entity)
        {
            XmlNode node = ParseXMLDocument($"censusdesc;scale={id}")
                .SelectSingleNode("/WORLD/CENSUSDESC");

            return entity switch
            {
                Entity.Nation => node.SelectSingleNode("NDESC").InnerText,
                Entity.Region => node.SelectSingleNode("RDESC").InnerText,
                _ => throw new NSError("Invalid entity."),
            };
        }

        /// <summary>
        /// Gets the name of the census.
        /// </summary>
        /// <param name="id">The ID of the census. </param>
        /// <returns>The name of the specified census.</returns>
        public static string CensusName(int id)
        {
            return ParseXMLDocument($"q=censusname&scale={id}")
                .SelectSingleNode("/WORLD/CENSUSNAME")
                .InnerText;
        }

        /// <summary>
        /// Gets the twenty nations after a specfied rank for a census.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="start">The start rank.</param>
        /// <returns>The twenty nations after the specified rank for the specified census.</returns>
        public static HashSet<CensusRank> NationsByCensusScore(int id, long start)
        {
            HashSet<CensusRank> res = new();

            foreach (XmlNode nation in ParseXMLDocument($"q=censusranks;scale={id}&start={start}").SelectSingleNode("/CENSUSRANKS/NATIONS").ChildNodes)
            {
                string name = nation
                    .SelectSingleNode("NAME")
                    .InnerText;
                double score = double.Parse(nation
                    .SelectSingleNode("SCORE")
                    .InnerText);
                long rank = long.Parse(nation
                    .SelectSingleNode("RANK")
                    .InnerText);

                res.Add(new CensusRank(id, name, score, rank));
            }

            return res;
        }

        /// <summary>
        /// Gets the scale of the census.
        /// </summary>
        /// <param name="id">The ID of the census. </param>
        /// <returns>The scale of the specified census.</returns>
        public static string CensusScale(int id)
        {
            return ParseXMLDocument($"q=censusscale;scale={id}")
                .SelectSingleNode("/WORLD/CENSUSSCALE")
                .InnerText;
        }

        /// <summary>
        /// Gets the title of the census.
        /// </summary>
        /// <param name="id">The ID of the census. </param>
        /// <returns>The title of the specified census.</returns>
        public static string CensusTitle(int id)
        {
            return ParseXMLDocument($"q=censustitle;scale={id}")
                .SelectSingleNode("/WORLD/CENSUSTITLE")
                .InnerText;
        }

        /// <summary>
        /// Gets twenty nations in order of their challenge rankings.
        /// </summary>
        /// <param name="start">The start rank.</param>
        /// <returns>A list of twenty nations with their challenge rank and relevant information.</returns>
        public static HashSet<Challenge> NationsByChallengeScore(long start = 1)
        {
            HashSet<Challenge> ranks = new();

            foreach (HtmlNode node in ParseHTMLDocument($"https://www.nationstates.net/page=challenge/ladder=1/start={start - 1}").SelectNodes("//table"))
            {
                foreach (HtmlNode row in node.SelectNodes("tr"))
                {
                    HtmlNodeCollection cells = row.SelectNodes("td");

                    if (cells != null)
                    {
                        long rank = long.Parse(cells[0].InnerText);
                        string name = cells[1].SelectSingleNode(".//span[@class='nname']").InnerText;
                        int level = int.Parse(cells[2].InnerText.Replace(",", string.Empty));
                        long score = long.Parse(cells[3].InnerText.Replace(",", string.Empty));
                        string speciality = cells[4].InnerText;
                        long wins = long.Parse(cells[5].InnerText.Replace(",", string.Empty));
                        long losses = long.Parse(cells[6].InnerText.Replace(",", string.Empty));
                        double winRate = double.Parse(cells[7].InnerText.Replace("%", string.Empty));

                        ranks.Add(new(rank, name, level, score, wins, losses, winRate, speciality));
                    }
                }
            }

            return ranks;
        }

        /// <summary>
        /// Gets a list of dispatches fulfilling certain criteria.
        /// </summary>
        /// <param name="author">The author of the dispatch.</param>
        /// <param name="category">The dispatch's category.</param>
        /// <param name="subCategory">The dispatch's sub-category.</param>
        /// <param name="sort">The sort to use.</param>
        /// <returns>A list of dispatches fulfilling the above criteria.</returns>
        public static List<Dispatch> Dispatches(string? author = null, DispatchCategory? category = null, Enum? subCategory = null, DispatchSort? sort = null)
        {
            List<Dispatch> dispatches = new();

            string url = "q=dispatchlist;";

            if (author != null)
            {
                url += $"dispatchauthor={author.Replace(" ", "_")}";
            }

            if (category != null && subCategory != null)
            {
                switch (category)
                {
                    case DispatchCategory.Account:
                        if (!Enum.IsDefined(typeof(DispatchAccount), subCategory))
                        {
                            throw new NSError("Sub-category type must be DispatchAccount.");
                        }

                        break;

                    case DispatchCategory.Bulletin:
                        if (!Enum.IsDefined(typeof(DispatchBulletin), subCategory))
                        {
                            throw new NSError("Sub-category type must be DispatchBulletin.");
                        }

                        break;

                    case DispatchCategory.Factbook:
                        if (!Enum.IsDefined(typeof(DispatchFactbook), subCategory))
                        {
                            throw new NSError("Sub-category type must be DispatchFactbook.");
                        }

                        break;

                    case DispatchCategory.Meta:
                        if (!Enum.IsDefined(typeof(DispatchMeta), subCategory))
                        {
                            throw new NSError("Sub-category type must be DispatchMeta.");
                        }

                        break;
                }

                url += $"dispatchcategory={category}:{subCategory}";
            }

            if (sort != null)
            {
                url += $"dispatchsort={sort}";
            }

            foreach (XmlNode dispatch in ParseXMLDocument(url).SelectSingleNode("DISPATCHLIST").ChildNodes)
            {
                dispatches.Add(new(ulong.Parse(dispatch.Attributes["id"].Value)));
            }

            return dispatches;
        }

        /// <summary>
        /// Gets fifty factions in order of their score.
        /// </summary>
        /// <param name="page">The page to search. Each page contains fifty factions.</param>
        /// <returns>A list of fifty factions.</returns>
        public static List<Faction> FactionsByScore(int page = 1)
        {
            List<Faction> factions = new();

            foreach (HtmlNode node in ParseHTMLDocument($"https://www.nationstates.net/page=factions?start={page - 1}").SelectNodes("//table/tbody"))
            {
                foreach (HtmlNode row in node.SelectNodes("tr"))
                {
                    HtmlNodeCollection cells = row.SelectNodes("td");

                    if (cells != null)
                    {
                        factions.Add(new(long.Parse(cells[3].SelectSingleNode("./a").Attributes["href"].Value.Split("/")[2].Replace("fid=", string.Empty))));
                    }
                }
            }

            return factions;
        }

        /// <summary>
        /// Gets fifty factions in order of their number of nations.
        /// </summary>
        /// <param name="page">The page to search. Each page contains fifty factions.</param>
        /// <returns>A list of fifty factions.</returns>
        public static List<Faction> FactionsByNations(int page = 1)
        {
            List<Faction> factions = new();

            foreach (HtmlNode node in ParseHTMLDocument($"https://www.nationstates.net/page=factions/view=nations/start={page - 1}").SelectNodes("//table/tbody"))
            {
                foreach (HtmlNode row in node.SelectNodes("tr"))
                {
                    HtmlNodeCollection cells = row.SelectNodes("td");

                    if (cells != null)
                    {
                        factions.Add(new(long.Parse(cells[3].SelectSingleNode("./a").Attributes["href"].Value.Split("/")[2].Replace("fid=", string.Empty))));
                    }
                }
            }

            return factions;
        }

        /// <summary>
        /// Gets the happenings that satisfy the given criteria.
        /// </summary>
        /// <param name="entities">The names of entities where the event occurred.</param>
        /// <param name="entityType">The type of entities given.</param>
        /// <param name="eventTypes">The events to filter through. Null if no sort.</param>
        /// <param name="limit">Number of events to get.</param>
        /// <param name="sinceID">Get events since an ID.</param>
        /// <param name="beforeID">Get events before an ID.</param>
        /// <param name="sinceTime">Get events since a certain time.</param>
        /// <param name="beforeTime">Get events before a certain time.</param>
        /// <returns>A collection of events satisfying the above criteria.</returns>
        public static HashSet<Event> Happenings(HashSet<string>? entities, Entity? entityType, HashSet<EventType>? eventTypes, int? limit, ulong? sinceID, ulong? beforeID, DateTime? sinceTime, DateTime? beforeTime)
        {
            string url = "q=happenings;";

            if (entities != null && entityType != null)
            {
                url += $"view={entityType.ToString().ToLower()}.{string.Join(",", entities).Replace(" ", "_")};";
            }

            if (eventTypes != null)
            {
                url += $"filter={string.Join("+", eventTypes)};";
            }

            if (limit != null)
            {
                url += $"limit={limit};";
            }

            if (sinceID != null)
            {
                url += $"sinceid={sinceID};";
            }

            if (beforeID != null)
            {
                url += $"beforeid={beforeID};";
            }

            if (sinceTime != null)
            {
                url += $"sincetime={((DateTimeOffset)sinceTime).ToUnixTimeSeconds()};";
            }

            if (beforeTime != null)
            {
                url += $"beforeTime={((DateTimeOffset)beforeTime).ToUnixTimeSeconds()};";
            }

            return ParseEvents(ParseXMLDocument(url)
                .SelectSingleNode("/WORLD/HAPPENINGS"));
        }

        /// <summary>
        /// Gets a collection of regions with and without the specified tags.
        /// </summary>
        /// <param name="with">Get regions with the <see cref="RegionTag"/>s.</param>
        /// <param name="without">Get regions without the <see cref="RegionTag"/>s.</param>
        /// <returns>A collection of regions that satisfy the above criteria.</returns>
        public static HashSet<string> RegionsByTags(HashSet<RegionTag>? with, HashSet<RegionTag>? without)
        {
            HashSet<string> tags = new();

            if (with != null)
            {
                foreach (RegionTag tag in with)
                {
                    tags.Add((RegionTagDict.Keys.Contains(tag) ? RegionTagDict[tag] : tag.ToString()).ToLower());
                }
            }

            if (without != null)
            {
                foreach (RegionTag tag in without)
                {
                    tags.Add("-" + (RegionTagDict.Keys.Contains(tag) ? RegionTagDict[tag] : tag.ToString()).ToLower());
                }
            }

            return ParseXMLDocument($"q=regionsbytag;tags={string.Join(",", tags)}")
                .SelectSingleNode("/WORLD/REGIONSBYTAGS")
                .InnerText
                .Split(",")
                .ToHashSet();
        }
    }
}