namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a RMB post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// The post's ID.
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// Time of postage.
        /// </summary>
        public DateTime Posted { get; set; }

        /// <summary>
        /// The post's sender.
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// The post's status.
        /// </summary>
        public PostStatus Status { get; set; }

        /// <summary>
        /// Time at which the post was last edited.
        /// </summary>
        public DateTime? Edited { get; set; }

        /// <summary>
        /// Nations that liked the post.
        /// </summary>
        public HashSet<string>? Likers { get; set; }

        /// <summary>
        /// The post's message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Name of the moderator that supressed the post.
        /// </summary>
        public string? Supressor { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Post"/> class.
        /// </summary>
        /// <param name="id">The post's ID.</param>
        /// <param name="posted">Time of postage.</param>
        /// <param name="nation">The post's sender.</param>
        /// <param name="status">The post's status.</param>
        /// <param name="edited">Time at which the post was last edited.</param>
        /// <param name="likers">Nations that liked the post.</param>
        /// <param name="message">The post's message.</param>
        /// <param name="supressor">Name of the moderator that supressed the post.</param>
        public Post(ulong id, DateTime posted, string nation, PostStatus status, DateTime? edited, HashSet<string>? likers, string message, string? supressor)
        {
            this.ID = id;
            this.Posted = posted;
            this.Nation = nation;
            this.Status = status;
            this.Edited = edited;
            this.Likers = likers;
            this.Message = message;
            this.Supressor = supressor;
        }
    }
}
