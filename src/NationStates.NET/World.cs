namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
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
        public static Dictionary<string, Banner> GetBanner(HashSet<string> banners)
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
        /// Gets a dispatch from its ID.
        /// </summary>
        /// <param name="id">The dispatch's ID.</param>
        /// <returns>The dispatch with the specified ID.</returns>
        public static Dispatch GetDispatch(ulong id)
        {
            return new Dispatch(id);
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
