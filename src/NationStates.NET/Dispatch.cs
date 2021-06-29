namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
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
        /// <param name="id">The dispatch's ID.</param>
        public Dispatch(ulong id)
        {
            this.ID = id;

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Utility.DownloadUrlString($"https://www.nationstates.net/cgi-bin/api.cgi?q=dispatch;dispatchid={id}"));

            XmlNode dispatch = doc.DocumentElement.SelectSingleNode("DISPATCH");

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
