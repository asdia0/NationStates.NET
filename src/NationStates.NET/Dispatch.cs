namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a dispatch.
    /// </summary>
    public class Dispatch
    {
        private dynamic _SubCategory;

        /// <summary>
        /// Dispatch's ID.
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// Dispatch's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Dispatch's author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Dispatch's category.
        /// </summary>
        public DispatchCategory Category { get; set; }

        /// <summary>
        /// Dispatch's sub-category.
        /// </summary>
        public dynamic SubCategory
        {
            get
            {
                return this._SubCategory;
            }

            set
            {
                switch (this.Category)
                {
                    case (DispatchCategory.Account):
                        if (Enum.IsDefined(typeof(DispatchAccount), value))
                        {
                            this._SubCategory = value;
                        }
                        else
                        {
                            throw new NSError("Sub-category type must be DispatchAccount.");
                        }
                        break;
                    case (DispatchCategory.Bulletin):
                        if (Enum.IsDefined(typeof(DispatchBulletin), value))
                        {
                            this._SubCategory = value;
                        }
                        else
                        {
                            throw new NSError("Sub-category type must be DispatchBulletin.");
                        }
                        break;
                    case (DispatchCategory.Factbook):
                        if (Enum.IsDefined(typeof(DispatchFactbook), value))
                        {
                            this._SubCategory = value;
                        }
                        else
                        {
                            throw new NSError("Sub-category type must be DispatchFactbook.");
                        }
                        break;
                    case (DispatchCategory.Meta):
                        if (Enum.IsDefined(typeof(DispatchMeta), value))
                        {
                            this._SubCategory = value;
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
        /// Time of creation.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Time of last edit.
        /// </summary>
        public DateTime Edited { get; set; }

        /// <summary>
        /// Number of views.
        /// </summary>
        public long Views { get; set; }

        /// <summary>
        /// Dispatch's score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Dispatch"/> class.
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
    }
}
