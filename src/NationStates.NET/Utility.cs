namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

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

        public static string DownloadUrlString(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36");

                return client.DownloadString(url);
            }
        }

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

        public static string FormatForEnum(string word)
        {
            return word.Replace(": ", string.Empty).Replace(" ", "_").Replace("-", string.Empty);
        }

        public static HashSet<Authority> ParseAuthority(string authorities)
        {
            HashSet<Authority> res = new HashSet<Authority>();

            foreach (char c in authorities)
            {
                res.Add(authorityDict[c]);
            }

            return res;
        }

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

        public static DateTime ParseUnix(string unix)
        {
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(unix)).DateTime;
        }
    }
}
