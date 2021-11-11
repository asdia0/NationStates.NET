namespace NationStates.NET
{
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
        /// Gets the census world average in each census.
        /// </summary>
        public static HashSet<CensusWorld> Census
        {
            get
            {
                HashSet<CensusWorld> censusAverages = new();

                foreach (XmlNode census in ParseDocument("q=census;scale=all").SelectSingleNode("CENSUS").ChildNodes)
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
                return int.Parse(ParseDocument("q=censusid")
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

                foreach (XmlNode faction in ParseDocument("q=factions").SelectSingleNode("/WORLD/FACTIONS").ChildNodes)
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
                return ParseDocument("q=featuredregion")
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
                return ulong.Parse(ParseDocument("q=lasteventid")
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
                return ParseDocument("q=nations")
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
                return ParseDocument("q=newnations")
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
                return long.Parse(ParseDocument("q=numnations")
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
                return long.Parse(ParseDocument("q=numregions")
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
                return ParseDocument("q=regions")
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
                XmlNode node = ParseDocument("q=tgqueue")
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

            foreach (XmlNode trade in ParseDocument(url).SelectNodes("/CARDS/TRADES/TRADE"))
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
        /// Gets the description for a census.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="entity">The type of entity to get the census description for.</param>
        /// <returns>The description for the specified census.</returns>
        public static string CensusDescription(int id, Entity entity)
        {
            XmlNode node = ParseDocument($"censusdesc;scale={id}")
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
            return ParseDocument($"q=censusname&scale={id}")
                .SelectSingleNode("/WORLD/CENSUSNAME")
                .InnerText;
        }

        /// <summary>
        /// Gets the twenty nations after a specfied rank for a census.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="start">The start rank.</param>
        /// <returns>The twenty nations after the specified rank for the specified census.</returns>
        public static List<CensusWorldRank> CensusRanks(int id, long start)
        {
            List<CensusWorldRank> res = new();

            foreach (XmlNode nation in ParseDocument($"q=censusranks;scale={id}&start={start}").SelectSingleNode("/CENSUSRANKS/NATIONS").ChildNodes)
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

                res.Add(new CensusWorldRank(id, name, score, rank));
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
            return ParseDocument($"q=censusscale;scale={id}")
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
            return ParseDocument($"q=censustitle;scale={id}")
                .SelectSingleNode("/WORLD/CENSUSTITLE")
                .InnerText;
        }

        /// <summary>
        /// Gets a list of dispatches fulfilling certain criteria.
        /// </summary>
        /// <param name="author">The author of the dispatch.</param>
        /// <param name="category">The dispatch's category.</param>
        /// <param name="subCategory">The dispatch's sub-category.</param>
        /// <param name="sort">The sort to use.</param>
        /// <returns>A list of dispatches fulfilling the above criteria.</returns>
        public static List<Dispatch> DispatchList(string? author, DispatchCategory? category, Enum subCategory, DispatchSort? sort)
        {
            List<Dispatch> dispatches = new();

            string url = "q=dispatchlist;";

            if (author != null)
            {
                url += $"dispatchauthor={author.Replace(" ", "_")}";
            }

            if (category != null)
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

            foreach (XmlNode dispatch in ParseDocument(url).SelectSingleNode("DISPATCHLIST").ChildNodes)
            {
                dispatches.Add(new(ulong.Parse(dispatch.Attributes["id"].Value)));
            }

            return dispatches;
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

            return ParseEvents(ParseDocument(url)
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

            return ParseDocument($"q=regionsbytag;tags={string.Join(",", tags)}")
                .SelectSingleNode("/WORLD/REGIONSBYTAGS")
                .InnerText
                .Split(",")
                .ToHashSet();
        }
    }
}