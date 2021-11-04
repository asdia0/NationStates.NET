namespace NationStates.NET
{
    using System;
    using System.Xml;

    /// <summary>
    /// Defines a dispatch.
    /// </summary>
    public struct Dispatch
    {
        /// <summary>
        /// Gets the dispatch's ID.
        /// </summary>
        public ulong ID { get; }

        /// <summary>
        /// Gets the dispatch's title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the dispatch's author.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets the dispatch's category.
        /// </summary>
        public DispatchCategory Category { get; }

        /// <summary>
        /// Gets the dispatch's sub-category.
        /// </summary>
        public dynamic SubCategory { get; }

        /// <summary>
        /// Gets the time of creation.
        /// </summary>
        public DateTime Created { get; }

        /// <summary>
        /// Gets the time of last edit.
        /// </summary>
        public DateTime Edited { get; }

        /// <summary>
        /// Gets the number of views.
        /// </summary>
        public long Views { get; }

        /// <summary>
        /// Gets the dispatch's score.
        /// </summary>
        public int Score { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dispatch"/> struct.
        /// </summary>
        /// <param name="id">Dispatch's ID.</param>
        /// <param name="title">Dispatch's title.</param>
        /// <param name="author">Dispatch's author.</param>
        /// <param name="category">Dispatch's category.</param>
        /// <param name="subCategory">Dispatch's sub-category.</param>
        /// <param name="created">Time of creation.</param>
        /// <param name="edited">Time of last edit.</param>
        /// <param name="views">Number of views.</param>
        /// <param name="score">Dispatch's score.</param>
        public Dispatch(ulong id, string title, string author, DispatchCategory category, dynamic subCategory, DateTime created, DateTime edited, long views, int score)
        {
            this.ID = id;
            this.Title = title;
            this.Author = author;
            this.Category = category;
            this.SubCategory = subCategory;
            this.Created = created;
            this.Edited = edited;
            this.Views = views;
            this.Score = score;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dispatch"/> struct.
        /// </summary>
        /// <param name="id">The dispatch's ID.</param>
        public Dispatch(ulong id)
        {
            this.ID = id;

            XmlNode dispatch = Utility.ParseDocument($"q=dispatch;dispatchid={id}")
                .SelectSingleNode("/WORLD/DISPATCH");

            this.Title = dispatch.SelectSingleNode("TITLE").InnerText;
            this.Category = (DispatchCategory)Enum.Parse(typeof(DispatchCategory), Utility.FormatForEnum(Utility.Capitalise(dispatch.SelectSingleNode("CATEGORY").InnerText)));
            this.Author = dispatch.SelectSingleNode("AUTHOR").InnerText;

            switch (this.Category)
            {
                case DispatchCategory.Account:
                    this.SubCategory = (DispatchAccount)Enum.Parse(typeof(DispatchAccount), dispatch.SelectSingleNode("SUBCATEGORY").InnerText);
                    break;

                case DispatchCategory.Bulletin:
                    this.SubCategory = (DispatchBulletin)Enum.Parse(typeof(DispatchBulletin), dispatch.SelectSingleNode("SUBCATEGORY").InnerText);
                    break;

                case DispatchCategory.Factbook:
                    this.SubCategory = (DispatchFactbook)Enum.Parse(typeof(DispatchFactbook), dispatch.SelectSingleNode("SUBCATEGORY").InnerText);
                    break;

                case DispatchCategory.Meta:
                    this.SubCategory = (DispatchMeta)Enum.Parse(typeof(DispatchMeta), dispatch.SelectSingleNode("SUBCATEGORY").InnerText);
                    break;

                default:
                    throw new NSError("Dispatch subcategory does not exist.");
            }

            this.Created = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dispatch.SelectSingleNode("CREATED").InnerText)).DateTime;
            this.Edited = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dispatch.SelectSingleNode("EDITED").InnerText)).DateTime;
            this.Views = long.Parse(dispatch.SelectSingleNode("VIEWS").InnerText);
            this.Score = int.Parse(dispatch.SelectSingleNode("SCORE").InnerText);
        }
    }
}