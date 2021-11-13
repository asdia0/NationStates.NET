namespace NationStates.NET
{
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Xml;

    /// <summary>
    /// A class containing useful methods.
    /// </summary>
    public class Utility
    {
        private static readonly Dictionary<char, Authority> AuthorityDict = new()
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
        /// Capitalises a string.
        /// </summary>
        /// <param name="text">The string to capitalise.</param>
        /// <returns>The capitalised string.</returns>
        public static string Capitalise(string text)
        {
            string firstChar = char.ToUpper(text[0]) + text.ToLower()[1..];

            StringBuilder sb = new(firstChar);

            for (int i = 0; i < sb.Length - 1; i++)
            {
                if (sb[i] == '-' || sb[i] == ' ')
                {
                    sb[i + 1] = char.ToUpper(sb[i + 1]);
                }
            }

            return sb.ToString();
        }

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
        /// Downloads a webpage.
        /// </summary>
        /// <param name="path">The path to the webpage.</param>
        /// <returns>The webpage contents.</returns>
        public static string DownloadPage(string path)
        {
            using (WebClient client = new())
            {
                client.Headers.Add("user-agent", "NationStates.NET (https://github.com/asdia0/NationStates.NET)");

                try
                {
                    Thread.Sleep(600);
                    return client.DownloadString(path);
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
        }

        /// <summary>
        /// Formats a string for enum parsing.
        /// </summary>
        /// <param name="text">The string to format.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatForEnum(string text)
        {
            return text.Replace(" - ", string.Empty).Replace("-", string.Empty).Replace(": ", string.Empty).Trim().Replace(" ", "_");
        }

        /// <summary>
        /// Gets the nation's name from its DBID.
        /// </summary>
        /// <param name="id">The nation's ID.</param>
        /// <returns>The nation's name.</returns>
        public static string NationNameFromID(long id)
        {
            return ParseDocument($"q=cards+info;nationid={id}")
                .SelectSingleNode("/CARDS/INFO/ID")
                .InnerText;
        }

        /// <summary>
        /// Parses a string for <see cref="Authority"/>s.
        /// </summary>
        /// <param name="authorities">The string to parse.</param>
        /// <returns>A collection of <see cref="Authority"/>s.</returns>
        public static HashSet<Authority> ParseAuthority(string authorities)
        {
            HashSet<Authority> res = new();

            foreach (char c in authorities)
            {
                res.Add(AuthorityDict[c]);
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
            doc.LoadXml(DownloadPage("https://www.nationstates.net/cgi-bin/api.cgi?" + path + "&v=11"));
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
            if (text.StartsWith("FT"))
            {
                return Enum.Parse(type, FormatForEnum(text));
            }

            return Enum.Parse(type, FormatForEnum(Capitalise(text)));
        }

        /// <summary>
        /// Parses <see cref="Event"/>s from a <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="events">The parent <see cref="XmlNode"/>.</param>
        /// <returns>A collection of <see cref="Event"/>s.</returns>
        public static HashSet<Event> ParseEvents(XmlNode events)
        {
            HashSet<Event> res = new();

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
        /// <param name="category">The category of the proposal/resolution.</param>
        /// <returns>A sub-category.</returns>
        public static dynamic ParseSubCategory(XmlNode option, dynamic category)
        {
            if (category is GACategory)
            {
                return category switch
                {
                    GACategory.Repeal => long.Parse(option.InnerText),
                    GACategory.Bookkeeping => GABookeeping.Sweeping,
                    GACategory.Regulation => (GARegulation)ParseEnum(typeof(GARegulation), option.InnerText),
                    GACategory.Health => (GAHealth)ParseEnum(typeof(GAHealth), option.InnerText),
                    GACategory.Environmental => (GAEnvironmental)ParseEnum(typeof(GAEnvironmental), option.InnerText),
                    GACategory.Education_And_Creativity => (GAEducationAndCreativity)ParseEnum(typeof(GAEducationAndCreativity), option.InnerText),
                    GACategory.Advancement_Of_Industry => (GAAdvancementOfIndustry)ParseEnum(typeof(GAAdvancementOfIndustry), option.InnerText),
                    _ => (GAStrength)ParseEnum(typeof(GAStrength), option.InnerText),
                };
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
        /// Parses <see cref="UNCategory"/>
        /// </summary>
        /// <param name="option">The option node.</param>
        /// <param name="category">The category of the proposal/resolution.</param>
        /// <returns>A sub-category.</returns>
        public static dynamic ParseUNSubCategory(HtmlNode option, dynamic category)
        {
            string formatted = option.InnerText.Replace(option.SelectSingleNode(".//span[@class='WA_leader']").InnerText, string.Empty);

            if (category is UNCategory)
            {
                return category switch
                {
                    UNCategory.Repeal => long.Parse(formatted.Trim().Replace("UN#", string.Empty)),
                    UNCategory.Bookkeeping => GABookeeping.Sweeping,
                    UNCategory.Regulation => (GARegulation)ParseEnum(typeof(GARegulation), formatted),
                    UNCategory.Health => (GAHealth)ParseEnum(typeof(GAHealth), formatted),
                    UNCategory.Environmental => (GAEnvironmental)ParseEnum(typeof(GAEnvironmental), formatted),
                    UNCategory.Education_And_Creativity => (GAEducationAndCreativity)ParseEnum(typeof(GAEducationAndCreativity), formatted),
                    UNCategory.Advancement_Of_Industry => (GAAdvancementOfIndustry)ParseEnum(typeof(GAAdvancementOfIndustry), formatted),
                    UNCategory.Recreational_Drug_Use => (UNRecreationalDrugUse)ParseEnum(typeof(UNRecreationalDrugUse), formatted),
                    _ => (GAStrength)ParseEnum(typeof(GAStrength), formatted),
                };
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
            jsonSerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            return JsonConvert.SerializeObject(value, jsonSerializerSettings);
        }
    }
}