namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using static NationStates.NET.Utility;

    /// <summary>
    /// Represents the NationStates world.
    /// </summary>
    public static class World
    {
        /// <summary>
        /// Gets the census world average in each census.
        /// </summary>
        public static HashSet<CensusWorld> CensusAverages
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
                List<Faction> factions = new List<Faction>();

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
        /// Gets the description for a census.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <param name="entity">The type of entity to get the census description for.</param>
        /// <returns>The description for the specified census.</returns>
        public static string CensusDescription(int id, Entity entity)
        {
            XmlNode node = Utility.ParseDocument($"censusdesc;scale={id}")
                .SelectSingleNode("/WORLD/CENSUSDESC");

            switch (entity)
            {
                case Entity.Nation:
                    return node.SelectSingleNode("NDESC")
                        .InnerText;

                case Entity.Region:
                    return node.SelectSingleNode("RDESC").InnerText;

                default:
                    throw new NSError("Invalid entity.");
            }
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
        public static List<WorldCensus> CensusRanks(int id, long start)
        {
            List<WorldCensus> res = new List<WorldCensus>();

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

                res.Add(new WorldCensus(id, name, score, rank));
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
            List<Dispatch> dispatches = new List<Dispatch>();

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
            HashSet<string> tags = new HashSet<string>();

            if (with != null)
            {
                foreach (RegionTag tag in with)
                {
                    tags.Add(RegionTagToString(tag).ToLower());
                }
            }

            if (without != null)
            {
                foreach (RegionTag tag in without)
                {
                    tags.Add("-" + RegionTagToString(tag).ToLower());
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