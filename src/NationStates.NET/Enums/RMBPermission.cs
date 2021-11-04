namespace NationStates.NET
{
    /// <summary>
    /// Types of RMB posting permissions for embassy regions.
    /// </summary>
    public enum RMBPermission
    {
        /// <summary>
        /// No embassy posting (0).
        /// </summary>
        None,

        /// <summary>
        /// Delegates and Founders (con).
        /// </summary>
        Delegate_Founder,

        /// <summary>
        /// Officers (off).
        /// </summary>
        Officers,

        /// <summary>
        /// Officers with <see cref="Authority.Communications"/> (com).
        /// </summary>
        CommunicationOfficers,

        /// <summary>
        /// All residents (all).
        /// </summary>
        All,
    }
}