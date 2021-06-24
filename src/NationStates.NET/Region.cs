namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    
    public class Region
    {
        public long DBID { get; set; }

        public string Delegate { get; set; }

        public Authority DelegateAuthorities { get; set; }

        public int DelegateVotes { get; set; }

        public HashSet<Dispatch> Dispatches { get; set; }

        public HashSet<string> Embassies { get; set; }

        public RMBPermission EmbassyRMBPermission { get; set; }

        public string Factbook { get; set; }

        public string Flag { get; set; }

        public DateTime FoundedTime { get; set; }

        public string Founder { get; set; }

        public Authority FounderAuthorities { get; set; }

        public Dictionary<WAVote, int> GAVote { get; set; }

        public HashSet<Event> Happenings { get; set; }

        public HashSet<Event> History { get; set; }

        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Last 10 RMB messages.
        /// </summary>
        public HashSet<Post> Messages { get; set; }

        public string Name { get; set; }

        public HashSet<string> Nations { get; set; }

        public HashSet<Officer> Officers { get; set; }

        public Poll Poll { get; set; }

        public Power Power { get; set; }

        public Dictionary<WAVote, int> SCVote { get; set; }

        public HashSet<Tag> Tags { get; set; }

        public HashSet<WABadge> WABadges { get; set; }

        public RegionZombie Zombie { get; set; }

        public HashSet<RegionCensus> Census { get; set; }

        public Region(string name)
        {
            this.Name = name;
            GetFields();
        }

        public void GetFields()
        {
            // Normal fields
            XmlDocument normal = new XmlDocument();

            normal.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?region={this.Name.Replace(" ", "_")}&q=dbid+delegate+delegateauth+delegatevotes+dispatches+embassies+embassyrmb+factbook+flag+foundedtime+founder+founderauth+gavote+happenings+history+lastupdate+messages+name+nations+officers+poll+power+scvote+tags+wabadges+zombie"));

            foreach (XmlNode node in normal.DocumentElement.ChildNodes)
            {
                this.ParseFieldsData(node);
            }

            // Census
            XmlDocument census = new XmlDocument();

            census.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?region={this.Name.Replace(" ", "_")}&q=census;scale=all;mode=score+rank+prank"));

            this.ParseCensusData(census.DocumentElement.SelectSingleNode("CENSUS"));
        }

        public void ParseFieldsData(XmlNode node)
        {

        }

        public void ParseCensusData(XmlNode census)
        {

        }
    }
}
