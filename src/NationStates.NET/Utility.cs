﻿namespace NationStates.NET
{
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
        /// Downloads a webpage.
        /// </summary>
        /// <param name="path">The path of the API request.</param>
        /// <returns>The webpage's content.</returns>
        public static string DownloadUrlString(string path)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", "NationStates.NET (https://github.com/asdia0/NationStates.NET)");

                try
                {
                    Thread.Sleep(600);
                    return client.DownloadString("https://www.nationstates.net/cgi-bin/api.cgi?" + path + "&v=11");
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
        /// Capitalises a word. The first character and every character after a dash and a space is capitalised.
        /// </summary>
        /// <param name="word">The word to capitalise.</param>
        /// <returns>The capitalised word.</returns>
        public static string Capitalise(string word)
        {
            string firstChar = char.ToUpper(word[0]) + word.ToLower().Substring(1);

            StringBuilder sb = new StringBuilder(firstChar);
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
        /// Formats a string for <see cref="Enum.Parse(Type, string)"/>.
        /// </summary>
        /// <param name="enumValue">The stringified value of the enum.</param>
        /// <returns>A formatted string.</returns>
        public static string FormatForEnum(string enumValue)
        {
            return enumValue.Replace(": ", string.Empty).Replace(" - ", string.Empty).Replace(" ", "_").Replace("-", string.Empty);
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
            doc.LoadXml(DownloadUrlString(path));
            return doc.DocumentElement;
        }

        /// <summary>
        /// Parses a string for <see cref="RMBPermission"/>.
        /// </summary>
        /// <param name="permission">The string to parse.</param>
        /// <returns>A <see cref="RMBPermission"/>.</returns>
        public static RMBPermission ParseRMBPermission(string permission)
        {
            switch (permission)
            {
                case "0":
                    return RMBPermission.None;

                case "con":
                    return RMBPermission.Delegate_Founder;

                case "off":
                    return RMBPermission.Officers;

                case "com":
                    return RMBPermission.CommunicationOfficers;

                case "all":
                    return RMBPermission.All;

                default:
                    throw new NSError("Unrecognised permission string.");
            }
        }

        /// <summary>
        /// Parses a string for <see cref="PostStatus"/>.
        /// </summary>
        /// <param name="status">The string to parse.</param>
        /// <returns>A <see cref="PostStatus"/>.</returns>
        public static PostStatus ParseStatus(string status)
        {
            switch (status)
            {
                case "0":
                    return PostStatus.Regular;

                case "1":
                    return PostStatus.SupressedViewable;

                case "2":
                    return PostStatus.Deleted;

                case "9":
                    return PostStatus.SupressedUnviewable;

                default:
                    throw new NSError("Unrecognised status string.");
            }
        }

        /// <summary>
        /// Parses <see cref="WAGACategory"/> or <see cref="WASCCategory"/>.
        /// </summary>
        /// <param name="option">The option node.</param>
        /// <param name="council">The council the proposal/resolution was submitted in.</param>
        /// <param name="category">The category of the proposal/resolution.</param>
        /// <returns>A sub-category.</returns>
        public static dynamic ParseSubCategory(XmlNode option, WACouncil council, dynamic category)
        {
            if (category is WAGACategory)
            {
                string formatted = FormatForEnum(Capitalise(option.InnerText));

                switch (category)
                {
                    case WAGACategory.Repeal:
                        return long.Parse(option.InnerText);

                    case WAGACategory.Bookkeeping:
                        return WAGABookeeping.Sweeping;

                    case WAGACategory.Regulation:
                        return (WAGARegulation)Enum.Parse(typeof(WAGARegulation), formatted);

                    case WAGACategory.Health:
                        return (WAGAHealth)Enum.Parse(typeof(WAGAHealth), formatted);

                    case WAGACategory.Environmental:
                        return (WAGAEnvironmental)Enum.Parse(typeof(WAGAEnvironmental), formatted);

                    case WAGACategory.Education_And_Creativity:
                        return (WAGAEducationAndCreativity)Enum.Parse(typeof(WAGAEducationAndCreativity), formatted);

                    case WAGACategory.Advancement_Of_Industry:
                        return (WAGAAdvancementOfIndustry)Enum.Parse(typeof(WAGAAdvancementOfIndustry), formatted);

                    default:
                        return (WAGAStrength)Enum.Parse(typeof(WAGAStrength), formatted);
                }
            }
            else if (category is WASCCategory)
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
        /// Converts <see cref="RegionTag"/> to a string.
        /// </summary>
        /// <param name="tag">The <see cref="RegionTag"/> to convert.</param>
        /// <returns>The stringified <see cref="RegionTag"/>.</returns>
        public static string RegionTagToString(RegionTag tag)
        {
            switch (tag)
            {
                case RegionTag.AntiCapitalist:
                    return "Anti-Capitalist";

                case RegionTag.AntiCommunist:
                    return "Anti-Communist";

                case RegionTag.AntiFascist:
                    return "Anti-Fascist";

                case RegionTag.AntiGeneral_Assembly:
                    return "Anti-General Assembly";

                case RegionTag.AntiSecurity_Council:
                    return "Anti-Secutiry Council";

                case RegionTag.AntiWorld_Assembly:
                    return "Anti-World Assembly";

                case RegionTag.EcoFriendly:
                    return "Eco-Friendly";

                case RegionTag.HumanOnly:
                    return "Human-Only";

                case RegionTag.MultiSpecies:
                    return "Multi-Species";

                case RegionTag.NonEnglish:
                    return "Non-English";

                case RegionTag.PostModern_Tech:
                    return "Post-Modern Tech";

                case RegionTag.FTFTL:
                    return "FT: FTL";

                case RegionTag.FTFTLi:
                    return "FT: FTLi";

                case RegionTag.FTSTL:
                    return "FT: STL";

                default:
                    return tag.ToString();
            }
        }
    }
}