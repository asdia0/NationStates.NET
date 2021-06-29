namespace NationStates.NET
{
    /// <summary>
    /// Represents the WorldAssembly.
    /// </summary>
    public static class WorldAssembly
    {
        public static WAResolution GetResolution(WACouncil council, long councilID)
        {
            return new WAResolution(council, councilID);
        }
    }
}
