namespace NationStates.NET
{
    using System.Net;
    public class Utility
    {
        public static string DownloadUrlString(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36");

                return client.DownloadString(url);
            }
        }
    }
}
