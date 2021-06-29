﻿namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
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
        /// <param name="url">The webpage to download.</param>
        /// <returns>The webpage's content.</returns>
        public static string DownloadUrlString(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36");

                return client.DownloadString(url);
            }
        }

        /// <summary>
        /// Capitalises a word. The first character and every character after a dash is capitalised.
        /// </summary>
        /// <param name="word">The word to capitalise.</param>
        /// <returns>The capitalised word.</returns>
        public static string Capitalise(string word)
        {
            string firstChar = char.ToUpper(word[0]) + word.ToLower().Substring(1);

            StringBuilder sb = new StringBuilder(firstChar);
            for (int i = 0; i < sb.Length - 1; i++)
            {
                if (sb[i] == '-')
                {
                    sb[i + 1] = char.ToUpper(sb[i + 1]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Formats a string for <see cref="Enum.Parse(Type, string)"/>.
        /// </summary>
        /// <param name="enumValue">The stringified value of the enum.</param>
        /// <returns>A formatted string.</returns>
        public static string FormatForEnum(string enumValue)
        {
            return enumValue.Replace(": ", string.Empty).Replace(" ", "_").Replace("-", string.Empty);
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
                DateTime timestamp = Utility.ParseUnix(ev.SelectSingleNode("TIMESTAMP").InnerText);
                string text = ev.SelectSingleNode("TEXT").InnerText;

                res.Add(new Event(timestamp, text));
            }

            return res;
        }
    }
}
