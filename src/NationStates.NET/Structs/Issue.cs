namespace NationStates.NET
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents an issue.
    /// </summary>
    public struct Issue
    {
        /// <summary>
        /// Gets the issue's ID.
        /// </summary>
        [JsonProperty]
        public int ID { get; }

        /// <summary>
        /// Gets the issue's title.
        /// </summary>
        [JsonProperty]
        public string Title { get; }

        /// <summary>
        /// Gets the issue's description.
        /// </summary>
        [JsonProperty]
        public string Text { get; }

        /// <summary>
        /// Gets a list of options that can be taken.
        /// </summary>
        [JsonProperty]
        public List<string> Options { get; }

        /// <summary>
        /// Gets the issue's author.
        /// </summary>
        [JsonProperty]
        public string Author { get; }

        /// <summary>
        /// Gets the issue's editor.
        /// </summary>
        [JsonProperty]
        public string Editor { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Issue"/> struct.
        /// </summary>
        /// <param name="id">The issue's ID.</param>
        /// <param name="title">The issue's title.</param>
        /// <param name="text">The issue's description.</param>
        /// <param name="options">A list of options that can be taken.</param>
        /// <param name="author">The issue's author.</param>
        /// <param name="editor">The issue's editor.</param>
        public Issue(int id, string title, string text, List<string> options, string author, string editor)
        {
            this.ID = id;
            this.Title = title;
            this.Text = text;
            this.Options = options;
            this.Author = author;
            this.Editor = editor;
        }

        /// <summary>
        /// Gets a JSON string representing the issue.
        /// </summary>
        /// <returns>A JSON string representing the issue.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}
