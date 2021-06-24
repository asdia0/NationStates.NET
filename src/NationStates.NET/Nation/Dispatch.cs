namespace NationStates.NET.Nation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a dispatch.
    /// </summary>
    public class Dispatch
    {
        private DispatchSubCategory _SubCategory;

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
        public Nation Author { get; set; }

        /// <summary>
        /// Dispatch's category.
        /// </summary>
        public DispatchCategory Category { get; set; }

        /// <summary>
        /// Dispatch's sub-category.
        /// </summary>
        public DispatchSubCategory SubCategory
        {
            get
            {
                return this._SubCategory;
            }

            set
            {
                List<DispatchSubCategory> accepted;

                switch (this.Category)
                {
                    case (DispatchCategory.Account):
                        accepted = new List<DispatchSubCategory>
                        {
                            DispatchSubCategory.Culture,
                            DispatchSubCategory.Diplomacy,
                            DispatchSubCategory.Drama,
                            DispatchSubCategory.Other,
                            DispatchSubCategory.Military,
                            DispatchSubCategory.Science,
                            DispatchSubCategory.Sport,
                            DispatchSubCategory.Trade,
                        };

                        if (accepted.Contains(value))
                        {
                            this._SubCategory = value;
                        }
                        else
                        {
                            throw new NSError("SubCategory must match the Category.");
                        }

                        break;

                    case (DispatchCategory.Bulletin):
                        accepted = new List<DispatchSubCategory>
                        {
                            DispatchSubCategory.Campaign,
                            DispatchSubCategory.News,
                            DispatchSubCategory.Opinion,
                            DispatchSubCategory.Policy,
                        };

                        if (accepted.Contains(value))
                        {
                            this._SubCategory = value;
                        }
                        else
                        {
                            throw new NSError("SubCategory must match the Category.");
                        }

                        break;

                    case (DispatchCategory.Factbook):
                        accepted = new List<DispatchSubCategory>
                        {
                            DispatchSubCategory.Culture,
                            DispatchSubCategory.Economy,
                            DispatchSubCategory.Geography,
                            DispatchSubCategory.History,
                            DispatchSubCategory.International,
                            DispatchSubCategory.Legislation,
                            DispatchSubCategory.Military,
                            DispatchSubCategory.Miscellaneous,
                            DispatchSubCategory.Overview,
                            DispatchSubCategory.Politics,
                            DispatchSubCategory.Trivia,
                            DispatchSubCategory.Religion,
                        };

                        if (accepted.Contains(value))
                        {
                            this._SubCategory = value;
                        }
                        else
                        {
                            throw new NSError("SubCategory must match the Category.");
                        }

                        break;

                    case (DispatchCategory.Meta):
                        accepted = new List<DispatchSubCategory>
                        {
                            DispatchSubCategory.Gameplay,
                            DispatchSubCategory.Reference,
                        };

                        if (accepted.Contains(value))
                        {
                            this._SubCategory = value;
                        }
                        else
                        {
                            throw new NSError("SubCategory must match the Category.");
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
        public Dispatch(ulong id, string title, Nation author, DispatchCategory category, DispatchSubCategory subCategory, DateTime created, DateTime edited, long views, int score)
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
