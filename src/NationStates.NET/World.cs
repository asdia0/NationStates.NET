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
        public static Dictionary<string, (string Name, string Validity)> GetBanner(HashSet<string> banners)
        {
            Dictionary<string, (string Name, string Validity)> res = new Dictionary<string, (string Name, string Validity)>();

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=banner;banner={string.Join(",", banners)}"));

            XmlNode node = doc.DocumentElement.SelectSingleNode("BANNERS");

            foreach (XmlNode banner in node.ChildNodes)
            {
                string id = banner.Attributes["id"].Value;
                string name = banner.SelectSingleNode("NAME").InnerText;
                string validity = banner.SelectSingleNode("VALIDITY").InnerText;

                res.Add(id, (name, validity));
            }

            return res;
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
