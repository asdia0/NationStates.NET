namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Xml;
    using Newtonsoft.Json;

    /// <summary>
    /// A class containing useful methods.
    /// </summary>
    public class Utility
    {
        private static Dictionary<char, Authority> authorityDict = new Dictionary<char, Authority>
        {
            { 'X', Authority.Executive },
            { 'W', Authority.World_Assembly },
            { 'A', Authority.Appearance },
            { 'B', Authority.Border_Control },
            { 'C', Authority.Communications },
            { 'E', Authority.Embassies },
            { 'P', Authority.Polls },
        };

        /// <summary>
        /// Converts a <see cref="DateTime"/> object to its UNIX timestamp.
        /// </summary>
        /// <param name="dateTime">The object to convert.</param>
        /// <returns>The <see cref="DateTime"/> as a UNIX timestamp.</returns>
        public static long ConvertToUnix(DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }

        /// <summary>
        /// Parses a string for <see cref="Authority"/>s.
        /// </summary>
        /// <param name="authorities">The string to parse.</param>
        /// <returns>A collection of <see cref="Authority"/>s.</returns>
        public static HashSet<Authority> ParseAuthority(string authorities)
        {
            HashSet<Authority> res = new HashSet<Authority>();

            foreach (char c in authorities)
            {
                res.Add(authorityDict[c]);
            }

            return res;
        }

        /// <summary>
        /// Parses an XML document from a webpage.
        /// </summary>
        /// <param name="path">The webpage to parse the XML document from.</param>
        /// <returns>An <see cref="XmlDocument"/>.</returns>
        public static XmlElement ParseDocument(string path)
        {
            XmlDocument doc = new();

            string document = string.Empty;

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", "NationStates.NET (https://github.com/asdia0/NationStates.NET)");

                try
                {
                    Thread.Sleep(600);
                    document = client.DownloadString("https://www.nationstates.net/cgi-bin/api.cgi?" + path + "&v=11");
                }
                catch (WebException e)
                {
                    if (e.Message.Contains("(429)"))
                    {
                        throw new NSError("Too many requests. Try again in 15 minutes.");
                    }

                    throw new NSError(e.Message);
                }
            }

            doc.LoadXml(document);
            return doc.DocumentElement;
        }

        /// <summary>
        /// Parses an enum from text.
        /// </summary>
        /// <param name="type">The type of enum to parse to.</param>
        /// <param name="text">The enum in words.</param>
        /// <returns>An enum.</returns>
        public static dynamic ParseEnum(Type type, string text)
        {
            string firstChar = char.ToUpper(text[0]) + text.ToLower().Substring(1);

            StringBuilder sb = new StringBuilder(firstChar);
            for (int i = 0; i < sb.Length - 1; i++)
            {
                if (sb[i] == '-' || sb[i] == ' ')
                {
                    sb[i + 1] = char.ToUpper(sb[i + 1]);
                }
            }

            string text2 = sb.ToString();

            text2.Replace(": ", string.Empty).Replace(" - ", string.Empty).Replace(" ", "_").Replace("-", string.Empty);

            return Enum.Parse(type, text2);
        }

        /// <summary>
        /// Parses <see cref="Event"/>s from a <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="events">The parent <see cref="XmlNode"/>.</param>
        /// <returns>A collection of <see cref="Event"/>s.</returns>
        public static HashSet<Event> ParseEvents(XmlNode events)
        {
            HashSet<Event> res = new HashSet<Event>();

            foreach (XmlNode ev in events.ChildNodes)
            {
                DateTime timestamp = ParseUnix(ev.SelectSingleNode("TIMESTAMP").InnerText);
                string text = ev.SelectSingleNode("TEXT").InnerText;

                res.Add(new Event(timestamp, text));
            }

            return res;
        }

        /// <summary>
        /// Parses <see cref="GACategory"/> or <see cref="SCCategory"/>.
        /// </summary>
        /// <param name="option">The option node.</param>
        /// <param name="council">The council the proposal/resolution was submitted in.</param>
        /// <param name="category">The category of the proposal/resolution.</param>
        /// <returns>A sub-category.</returns>
        public static dynamic ParseSubCategory(XmlNode option, Council council, dynamic category)
        {
            if (category is GACategory)
            {
                switch (category)
                {
                    case GACategory.Repeal:
                        return long.Parse(option.InnerText);

                    case GACategory.Bookkeeping:
                        return GABookeeping.Sweeping;

                    case GACategory.Regulation:
                        return (GARegulation)ParseEnum(typeof(GARegulation), option.InnerText);

                    case GACategory.Health:
                        return (GAHealth)ParseEnum(typeof(GAHealth), option.InnerText);

                    case GACategory.Environmental:
                        return (GAEnvironmental)ParseEnum(typeof(GAEnvironmental), option.InnerText);

                    case GACategory.Education_And_Creativity:
                        return (GAEducationAndCreativity)ParseEnum(typeof(GAEducationAndCreativity), option.InnerText);

                    case GACategory.Advancement_Of_Industry:
                        return (GAAdvancementOfIndustry)ParseEnum(typeof(GAAdvancementOfIndustry), option.InnerText);

                    default:
                        return (GAStrength)ParseEnum(typeof(GAStrength), option.InnerText);
                }
            }
            else if (category is SCCategory)
            {
                if (option.InnerText.StartsWith("N:"))
                {
                    return (Entity.Nation, option.InnerText.Replace("N:", string.Empty));
                }

                if (option.InnerText.StartsWith("R:"))
                {
                    return (Entity.Region, option.InnerText.Replace("R:", string.Empty));
                }
                else
                {
                    // Identical to RepealedID
                    return long.Parse(option.InnerText);
                }
            }
            else
            {
                throw new NSError("Invalid category type.");
            }
        }

        /// <summary>
        /// Parses a string for <see cref="DateTime"/>.
        /// </summary>
        /// <param name="unix">The string to parse.</param>
        /// <returns>A <see cref="DateTime"/>.</returns>
        public static DateTime ParseUnix(string unix)
        {
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(unix)).DateTime;
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A JSON string of the object.</returns>
        public static string Serialize(object? value)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            return JsonConvert.SerializeObject(value, jsonSerializerSettings);
        }
    }
}