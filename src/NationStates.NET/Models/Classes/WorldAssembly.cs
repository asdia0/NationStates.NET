namespace NationStates.NET
{
    using System.Collections.Generic;
    using System.Linq;
    using static NationStates.NET.Utility;

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
    }
}