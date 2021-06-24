namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a regional poll.
    /// </summary>
    public class Poll
    {
        /// <summary>
        /// The poll's ID.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// The poll's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The region the poll was held in.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The time at which the poll started.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The time at which the poll ended.
        /// </summary>
        public DateTime Stop { get; set; }

        /// <summary>
        /// The name of the nation that started the poll.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// A list of options and results for the polls.
        /// </summary>
        public HashSet<PollOption> Options { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Poll"/> class.
        /// </summary>
        /// <param name="id">The poll's ID.</param>
        /// <param name="title">The poll's title.</param>
        /// <param name="region">The region the poll was held in.</param>
        /// <param name="start">The time at which the poll started.</param>
        /// <param name="stop">The time at which the poll ended.</param>
        /// <param name="author">The name of the nation that started the poll.</param>
        /// <param name="options">A list of options and results for the polls.</param>
        public Poll(long id, string title, string region, DateTime start, DateTime stop, string author, HashSet<PollOption> options)
        {
            this.ID = id;
            this.Title = title;
            this.Region = region;
            this.Start = start;
            this.Stop = stop;
            this.Author = author;
            this.Options = options;
        }
    }
}
