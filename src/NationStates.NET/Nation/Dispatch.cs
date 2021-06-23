namespace NationStates.NET.Nation
{
    using System;
    using System.Collections.Generic;

    public class Dispatch
    {
        private DispatchSubCategory _SubCategory;

        public ulong ID;

        public string Title;

        public Nation Author;

        public DispatchCategory Category;

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

        public DateTime Created;

        public DateTime Edited;

        public long Views;

        public int Score;

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
