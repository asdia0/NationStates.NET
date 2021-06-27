namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Represents a dispatch.
    /// </summary>
    public class Dispatch
    {
        private dynamic _subCategory;

        /// <summary>
        /// Gets or sets the dispatch's ID.
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// Gets or sets the dispatch's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the dispatch's author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the dispatch's category.
        /// </summary>
        public DispatchCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the dispatch's sub-category.
        /// </summary>
        public dynamic SubCategory
        {
            get
            {
                return this._subCategory;
            }

            set
            {
                switch (this.Category)
                {
                    case DispatchCategory.Account:
                        if (Enum.IsDefined(typeof(DispatchAccount), value))
                        {
                            this._subCategory = value;
                        }
                        else
                        {
                            throw new NSError("Sub-category type must be DispatchAccount.");
                        }

                        break;
                    case DispatchCategory.Bulletin:
                        if (Enum.IsDefined(typeof(DispatchBulletin), value))
                        {
                            this._subCategory = value;
                        }
                        else
                        {
                            throw new NSError("Sub-category type must be DispatchBulletin.");
                        }

                        break;
                    case DispatchCategory.Factbook:
                        if (Enum.IsDefined(typeof(DispatchFactbook), value))
                        {
                            this._subCategory = value;
                        }
                        else
                        {
                            throw new NSError("Sub-category type must be DispatchFactbook.");
                        }

                        break;
                    case DispatchCategory.Meta:
                        if (Enum.IsDefined(typeof(DispatchMeta), value))
                        {
                            this._subCategory = value;
                        }
                        else
                        {
                            throw new NSError("Sub-category type must be DispatchMeta.");
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the time of creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the time of last edit.
        /// </summary>
        public DateTime Edited { get; set; }

        /// <summary>
        /// Gets or sets the number of views.
        /// </summary>
        public long Views { get; set; }

        /// <summary>
        /// Gets or sets the dispatch's score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dispatch"/> class.
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
        /// Initializes a new instance of the <see cref="Dispatch"/> class.
        /// </summary>
        /// <param name="id">The dispatch's ID.</param>
        public Dispatch(ulong id)
        {
            this.ID = id;

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=dispatch;dispatchid={id}"));

            XmlNode dispatch = doc.DocumentElement.SelectSingleNode("DISPATCH");

            this.Title = dispatch.SelectSingleNode("TITLE").InnerText;
            this.Category = (DispatchCategory)Enum.Parse(typeof(DispatchCategory), Utility.FormatForEnum(Utility.Capitalise(dispatch.SelectSingleNode("CATEGORY").InnerText)));

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
            }

            this.Created = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dispatch.SelectSingleNode("CREATED").InnerText)).DateTime;
            this.Edited = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dispatch.SelectSingleNode("EDITED").InnerText)).DateTime;
            this.Views = long.Parse(dispatch.SelectSingleNode("VIEWS").InnerText);
            this.Score = int.Parse(dispatch.SelectSingleNode("SCORE").InnerText);
        }
    }
}
