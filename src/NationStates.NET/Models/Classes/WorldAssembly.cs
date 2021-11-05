namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using static Utility;

    /// <summary>
    /// Represents the WorldAssembly.
    /// </summary>
    public static class WorldAssembly
    {
        /// <summary>
        /// Gets the number of nations in the world assembly.
        /// </summary>
        public static long NumNations
        {
            get
            {
                return long.Parse(ParseDocument("wa=1&q=numnations")
                    .SelectSingleNode("/WA/NUMNATIONS")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets the number of delegates.
        /// </summary>
        public static int NumDelegates
        {
            get
            {
                return int.Parse(ParseDocument("wa=1&q=numdelegates")
                    .SelectSingleNode("/WA/NUMDELEGATES")
                    .InnerText);
            }
        }

        /// <summary>
        /// Gets a list of the names of all the delegates.
        /// </summary>
        public static HashSet<string> Delegates
        {
            get
            {
                return ParseDocument("wa=1&q=delegates")
                    .SelectSingleNode("/WA/DELEGATES")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets a list of the names of all the nations in the world assembly.
        /// </summary>
        public static HashSet<string> Members
        {
            get
            {
                return ParseDocument("wa=1&q=members")
                    .SelectSingleNode("/WA/MEMBERS")
                    .InnerText
                    .Split(",")
                    .ToHashSet();
            }
        }

        /// <summary>
        /// Gets a list of recent events in the world assembly.
        /// </summary>
        public static HashSet<Event> Happenings
        {
            get
            {
                return ParseEvents(ParseDocument($"wa=1&q=happenings")
                    .SelectSingleNode("/WA/HAPPENINGS"));
            }
        }

        /// <summary>
        /// Gets the outcome for the last resolution that was voted upon in the Security Council.
        /// </summary>
        /// <param name="council">The council to get the last resolution of.</param>
        /// <returns>A string describing the outcome of the last resolution.</returns>
        public static string LastResolution(WACouncil council)
        {
            return ParseDocument($"wa={(int)council + 1}&q=lastresolution").SelectSingleNode("/WA/LASTRESOLUTION").InnerText;
        }

        /// <summary>
        /// Gets a list of proposals.
        /// </summary>
        /// <param name="council">The council to get the proposals of.</param>
        /// <returns>A list of proposals in the given council.</returns>
        public static HashSet<WAProposal> Proposals(WACouncil council)
        {
            HashSet<WAProposal> proposals = new();

            foreach (XmlNode node in ParseDocument($"wa={(int)council + 1}&q=proposals").SelectNodes("/WA/PROPOSALS/PROPOSAL"))
            {
                string id = node.Attributes["id"].Value;
                HashSet<string> approvals = node.SelectSingleNode("APPROVALS").InnerText.Split(":").ToHashSet();
                dynamic category;

                switch (council)
                {
                    case WACouncil.General_Assembly:
                        category = (WAGACategory)Enum.Parse(typeof(WAGACategory), FormatForEnum(Capitalise(node.SelectSingleNode("CATEGORY").InnerText)));
                        break;
                    case WACouncil.Security_Council:
                        category = (WASCCategory)Enum.Parse(typeof(WASCCategory), FormatForEnum(Capitalise(node.SelectSingleNode("CATEGORY").InnerText)));
                        break;
                    default:
                        throw new NSError("Invalid council.");
                }

                DateTime created = ParseUnix(node.SelectSingleNode("CREATED").InnerText);
                string description = node.SelectSingleNode("DESC").InnerText;
                string name = node.SelectSingleNode("NAME").InnerText;
                string proposer = node.SelectSingleNode("PROPOSED_BY").InnerText;
                dynamic subCategory = ParseSubCategory(node.SelectSingleNode("OPTION"), council, category);

                proposals.Add(new(id, approvals, category, council, created, description, name, proposer, subCategory));
            }

            return proposals;
        }
    }
}