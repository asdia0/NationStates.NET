﻿namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Represents the NationStates world.
    /// </summary>
    public static class World
    {
        /// <summary>
        /// Gets a dispatch from its ID.
        /// </summary>
        /// <param name="id">The dispatch's ID.</param>
        /// <returns>The dispatch with the specified ID.</returns>
        public static Dispatch GetDispatch(ulong id)
        {
            return new Dispatch(id);
        }

        /// <summary>
        /// Gets a poll from its ID.
        /// </summary>
        /// <param name="id">The poll's ID.</param>
        /// <returns>The poll with the specified ID.</returns>
        public static Poll GetPoll(long id)
        {
            return new Poll(id);
        }
    }
}
