namespace NationStates.NET.Nation
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class Nation
    {
        public string Animal;

        public int Answered;

        public HashSet<string> Banners;

        public string Capital;

        public WACategory Category;

        // TODO: Census

        public string Currency;

        public long DBID;

        public HashSet<Death> Deaths;

        public string Demonym;

        public string Demonym2;

        public string Demonym2Plural;

        public List<Dispatch> DispatchList;

        public int Endorsements;

        public DateTime FirstLogin;

        public string Flag;

        public DateTime FoundedTime;

        public Freedom Freedom;

        public string FullName;

        public Vote GAVote;

        public ulong GDP;

        public Government Government;

        public List<Event> Happenings;

        public long Income;

        public Influence Influence;

        public DateTime LastLogin;

        public string Leader;

        public HashSet<string> Legislation;

        public Industry MajorIndustry;

        public string Motto;

        public string Name;

        public HashSet<Policy> Policies;

        public long Poorest;

        /// <summary>
        /// Population in millions;
        /// </summary>
        public long Population;

        public double PublicSector;

        public string Region;

        public string Religion;

        public long Richest;

        public Vote SCVote;

        public HashSet<Sector> Sectors;

        public double Tax;

        public bool TelegramCanRecruit;

        public bool TelegramCanCampaign;

        public string Type;

        public WAStatus WA;

        public HashSet<WABadge> WABadges;

        public Zombie Zombie;

        public Nation(string name)
        {
            this.Name = name;
            GetFields();
        }

        public void GetFields()
        {
            // Get normal fields
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?nation={this.Name.Replace(" ", "_")}&q=dispatchlist"));

            // Parse Data
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                this.ParseFieldsData(node);
            }

            // Get census

            // Parse Data
        }

        public void ParseFieldsData(XmlNode node)
        {
            switch (node.Name)
            {
                
            }
        }
    }
}
