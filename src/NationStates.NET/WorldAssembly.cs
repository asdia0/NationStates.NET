namespace NationStates.NET
{
    /// <summary>
    /// Represents the WorldAssembly.
    /// </summary>
    public static class WorldAssembly
    {
        /// <summary>
        /// Gets a World Assembly resolution.
        /// </summary>
        /// <param name="council">The council the resolution was submitted in.</param>
        /// <param name="councilID">The resolution's council ID.</param>
        /// <returns>A World Assembly resolution with the specified council ID.</returns>
        public static WAResolution GetResolution(WACouncil council, long councilID)
        {
            return new WAResolution(council, councilID);
        }
    }
}
