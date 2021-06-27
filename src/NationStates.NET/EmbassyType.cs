namespace NationStates.NET
{
    /// <summary>
    /// Types of embassies.
    /// </summary>
    public enum EmbassyType
    {
        /// <summary>
        /// Embassy has been constructed.
        /// </summary>
        Constructed,

        /// <summary>
        /// Invitation has been sent.
        /// </summary>
        Invited,

        /// <summary>
        /// Embassy is under construction.
        /// </summary>
        Pending,

        /// <summary>
        /// Embassy is closing.
        /// </summary>
        Closing,
    }
}
