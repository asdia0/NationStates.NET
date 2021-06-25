namespace NationStates.NET
{
    using System.Collections.Generic;
    using System.Text;
    using System.Net;

    public class Utility
    {
        private static Dictionary<char, Authority> AuthorityDict = new Dictionary<char, Authority>
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
            return word.Replace(" ", "_").Replace("-", "");
        }

        public static HashSet<Authority> ParseAuthority(string authorities)
        {
            HashSet<Authority> res = new HashSet<Authority>();
            
            foreach (char c in authorities)
            {
                res.Add(AuthorityDict[c]);
            }

            return res;
        }
    }
}
