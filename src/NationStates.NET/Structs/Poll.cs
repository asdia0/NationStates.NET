namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents a regional poll.
    /// </summary>
    public struct Poll
    {
        /// <summary>
        /// Gets the name of the nation that started the poll.
        /// </summary>
        [JsonProperty]
        public string Author { get; }

        /// <summary>
        /// Gets the poll's ID.
        /// </summary>
        [JsonProperty]
        public long ID { get; }

        /// <summary>
        /// Gets a list of options and results for the polls.
        /// </summary>
        [JsonProperty]
        public HashSet<PollOption> Options { get; }

        /// <summary>
        /// Gets the region the poll was held in.
        /// </summary>
        [JsonProperty]
        public string Region { get; }

        /// <summary>
        /// Gets the time at which the poll started.
        /// </summary>
        [JsonProperty]
        public DateTime Start { get; }

        /// <summary>
        /// Gets the time at which the poll ended.
        /// </summary>
        [JsonProperty]
        public DateTime Stop { get; }

        /// <summary>
        /// Gets the poll's title.
        /// </summary>
        [JsonProperty]
        public string Title { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Poll"/> struct.
        /// </summary>
        /// <param name="id">The poll's ID.</param>
        public Poll(long id)
        {
            this.ID = id;

            XmlNode node = ParseDocument($"q=poll;pollid={id}").SelectSingleNode("/WORLD/POLL");

            this.Title = node.SelectSingleNode("TITLE").InnerText;
            this.Region = node.SelectSingleNode("REGION").InnerText;
            this.Start = ParseUnix(node.SelectSingleNode("START").InnerText);
            this.Stop = ParseUnix(node.SelectSingleNode("STOP").InnerText);
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

        /// <summary>
        /// Gets a JSON string representing the poll.
        /// </summary>
        /// <returns>A JSON string representing the poll.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}