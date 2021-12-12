namespace NationStates.NET
{
    /// <summary>
    /// Defines the types of notices.
    /// </summary>
    public enum NoticeType
    {
        /// <summary>
        /// Telegram received.
        /// </summary>
        Telegram,

        /// <summary>
        /// Issue encountered.
        /// </summary>
        Issue,

        /// <summary>
        /// Endorsement gained.
        /// </summary>
        EndorsementGained,

        /// <summary>
        /// Endorsement lost.
        /// </summary>
        EndorsementLost,

        /// <summary>
        /// Banner gained.
        /// </summary>
        Banner,

        /// <summary>
        /// Rank increased in a World Census scale.
        /// </summary>
        Rank,

        /// <summary>
        /// Policy installed or cancelled.
        /// </summary>
        Policy,

        /// <summary>
        /// Trading card bought or sold.
        /// </summary>
        Card,

        /// <summary>
        /// Mentioned in RMB post.
        /// </summary>
        RMBMention,

        /// <summary>
        /// Quoted in RMB post.
        /// </summary>
        RMBQuote,

        /// <summary>
        /// RMB post liked.
        /// </summary>
        RMBLike,

        /// <summary>
        /// Mentioned in a dispatch.
        /// </summary>
        DispatchMention,

        /// <summary>
        /// Dispatch pinned by a region.
        /// </summary>
        DispatchPin,

        /// <summary>
        /// Dispatch quoted in RMB post.
        /// </summary>
        DispatchQuote,

        /// <summary>
        /// Region receives request or changes status.
        /// </summary>
        Embassy,
    }
}