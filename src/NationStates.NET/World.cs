namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Xml;

    /// <summary>
    /// Represents the NationStates world.
    /// </summary>
    public static class World
    {
        /// <summary>
        /// Gets the name and validity of banners from their ID.
        /// </summary>
        /// <param name="banners">A collection of the banner IDs to search for.</param>
        /// <returns>The name and validty of specified banners.</returns>
        public static Dictionary<string, Banner> GetBanners(HashSet<string> banners)
        {
            Dictionary<string, Banner> res = new Dictionary<string, Banner>();

            foreach (string banner in banners)
            {
                res.Add(banner, new Banner(banner));
            }

            return res;
        }

        /// <summary>
        /// Returns the census world average in each census.
        /// </summary>
        /// <returns>A dictionary. Key: Census ID. Value: Census score.</returns>
        public static Dictionary<int, double> GetCensusAverage()
        {
            Dictionary<int, double> res = new Dictionary<int, double>();

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString("https://www.nationstates.net/cgi-bin/api.cgi?q=census;scale=all"));

            XmlNode node = doc.DocumentElement.SelectSingleNode("CENSUS");

            foreach (XmlNode census in node.ChildNodes)
            {
                int id = int.Parse(census.Attributes["id"].Value);
                double score = double.Parse(census.SelectSingleNode("SCORE").InnerText);

                res.Add(id, score);
            }

            return res;
        }

        /// <summary>
        /// Gets today's census.
        /// </summary>
        /// <returns>The ID of today's census.</returns>
        public static int GetCensusOfTheDay()
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString("https://www.nationstates.net/cgi-bin/api.cgi?q=censusid"));

            return int.Parse(doc.DocumentElement.SelectSingleNode("CENSUSID").InnerText);
        }

        /// <summary>
        /// Gets the <see cref="CensusDescription"/> for a census.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <returns>The <see cref="CensusDescription"/> for the specified census.</returns>
        public static CensusDescription GetCensusDescription(int id)
        {
            return new CensusDescription(id);
        }

        /// <summary>
        /// Gets the name of the census.
        /// </summary>
        /// <param name="id">The ID of the census. </param>
        /// <returns>The name of the specified census.</returns>
        public static string GetCensusName(int id)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=censusname;scale={id}"));

            return doc.DocumentElement.FirstChild.InnerText;
        }

        /// <summary>
        /// Gets the top twenty nations in the world for a census.
        /// </summary>
        /// <param name="id">The census ID.</param>
        /// <returns>The top twenty nations in the world for the specified census.</returns>
        public static List<WorldCensus> GetCensusRanks(int id)
        {
            List<WorldCensus> res = new List<WorldCensus>();

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=censusranks;scale={id}"));

            XmlNode nations = doc.DocumentElement.SelectSingleNode("CENSUSRANKS/NATIONS");

            foreach (XmlNode nation in nations.ChildNodes)
            {
                string name = nation.SelectSingleNode("NAME").InnerText;
                double score = double.Parse(nation.SelectSingleNode("SCORE").InnerText);

                res.Add(new WorldCensus(id, name, score));
            }

            return res;
        }

        /// <summary>
        /// Gets the scale of the census.
        /// </summary>
        /// <param name="id">The ID of the census. </param>
        /// <returns>The scale of the specified census.</returns>
        public static string GetCensusScale(int id)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=censusscale;scale={id}"));

            return doc.DocumentElement.FirstChild.InnerText;
        }

        /// <summary>
        /// Gets the title of the census.
        /// </summary>
        /// <param name="id">The ID of the census. </param>
        /// <returns>The title of the specified census.</returns>
        public static string GetCensusTitle(int id)
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=censustitle;scale={id}"));

            return doc.DocumentElement.FirstChild.InnerText;
        }

        /// <summary>
        /// Gets a dispatch from its ID.
        /// </summary>
        /// <param name="id">The dispatch's ID.</param>
        /// <returns>The dispatch with the specified ID.</returns>
        public static Dispatch GetDispatch(ulong id)
        {
            return new Dispatch(id);
        }

        /// <summary>
        /// Gets a list of dispatches fulfilling certain criteria.
        /// </summary>
        /// <param name="author">The author of the dispatch.</param>
        /// <param name="category">The dispatch's category.</param>
        /// <param name="subCategory">The dispatch's sub-category.</param>
        /// <param name="sort">The sort to use.</param>
        /// <returns>A list of dispatches fulfilling the above criteria.</returns>
        public static List<Dispatch> GetDispatchList(string? author, DispatchCategory? category, Enum subCategory, DispatchSort? sort)
        {
            List<Dispatch> res = new List<Dispatch>();

            string url = "https://www.nationstates.net/cgi-bin/api.cgi?q=dispatchlist;";

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

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString(url));

            XmlNode node = doc.DocumentElement.SelectSingleNode("DISPATCHLIST");

            foreach (XmlNode dispatch in node.ChildNodes)
            {
                res.Add(World.GetDispatch(ulong.Parse(dispatch.Attributes["id"].Value)));
            }

            return res;
        }

        /// <summary>
        /// Gets a faction from its ID.
        /// </summary>
        /// <param name="id">The faction's ID.</param>
        /// <returns>The faction with the specified ID.</returns>
        public static Faction GetFaction(long id)
        {
            return new Faction(id);
        }

        /// <summary>
        /// Gets a list of all factions sorted by their score.
        /// </summary>
        /// <returns>A list of all factions.</returns>
        public static List<Faction> GetFactions()
        {
            List<Faction> res = new List<Faction>();

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString("https://www.nationstates.net/cgi-bin/api.cgi?q=factions"));

            XmlNode node = doc.DocumentElement.FirstChild;

            foreach (XmlNode faction in node.ChildNodes)
            {
                res.Add(new Faction(long.Parse(faction.Attributes["id"].Value)));
                Thread.Sleep(600);
            }

            return res;
        }

        /// <summary>
        /// Gets today's featured region.
        /// </summary>
        /// <returns>Today's featured region.</returns>
        public static string GetFeaturedRegion()
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString("https://www.nationstates.net/cgi-bin/api.cgi?q=featuredregion"));

            return doc.DocumentElement.FirstChild.InnerText;
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
        public static HashSet<Event> GetHappenings(HashSet<string>? entities, EntityType? entityType, HashSet<EventType>? eventTypes, int? limit, ulong? sinceID, ulong? beforeID, DateTime? sinceTime, DateTime? beforeTime)
        {
            string url = "https://www.nationstates.net/cgi-bin/api.cgi?q=happenings;";

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

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString(url));

            return Utility.ParseEvents(doc.DocumentElement.FirstChild);
        }

        /// <summary>
        /// Gets the ID of the latest event.
        /// </summary>
        /// <returns>The ID of the latest event.</returns>
        public static ulong GetLastEventID()
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString("https://www.nationstates.net/cgi-bin/api.cgi?q=lasteventid"));

            return ulong.Parse(doc.DocumentElement.FirstChild.InnerText);
        }

        /// <summary>
        /// Gets a poll from its ID.
        /// </summary>
        /// <param name="id">The poll's ID.</param>
        /// <returns>The poll with the specified ID.</returns>
        public static Poll GetPoll(long id)
        {
            return new Poll(id);
        }
    }
}
