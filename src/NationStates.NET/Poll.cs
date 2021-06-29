namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Defines a regional poll.
    /// </summary>
    public struct Poll
    {
        /// <summary>
        /// Gets the poll's ID.
        /// </summary>
        public long ID { get; }

        /// <summary>
        /// Gets the poll's title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the region the poll was held in.
        /// </summary>
        public string Region { get; }

        /// <summary>
        /// Gets the time at which the poll started.
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// Gets the time at which the poll ended.
        /// </summary>
        public DateTime Stop { get; }

        /// <summary>
        /// Gets the name of the nation that started the poll.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets a list of options and results for the polls.
        /// </summary>
        public HashSet<PollOption> Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Poll"/> struct.
        /// </summary>
        /// <param name="id">The poll's ID.</param>
        public Poll(long id)
        {
            this.ID = id;

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=poll;pollid={id}"));

            XmlNode node = doc.DocumentElement.SelectSingleNode("POLL");

            this.Title = node.SelectSingleNode("TITLE").InnerText;
            this.Region = node.SelectSingleNode("REGION").InnerText;
            this.Start = Utility.ParseUnix(node.SelectSingleNode("START").InnerText);
            this.Stop = Utility.ParseUnix(node.SelectSingleNode("STOP").InnerText);
            this.Author = node.SelectSingleNode("AUTHOR").InnerText;
            this.Options = new HashSet<PollOption>();

            foreach (XmlNode option in node.SelectSingleNode("OPTIONS").ChildNodes)
            {
                int optionID = int.Parse(option.Attributes["id"].Value);
                string text = option.SelectSingleNode("OPTIONTEXT").InnerText;
                int votes = int.Parse(option.SelectSingleNode("VOTES").InnerText);
                HashSet<string> voters = option.SelectSingleNode("VOTERS").InnerText.Split(":").ToHashSet();

                this.Options.Add(new PollOption(optionID, text, votes, voters));
            }
        }
    }
}
