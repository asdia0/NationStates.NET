namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a regional officer.
    /// </summary>
    public struct Officer
    {
        /// <summary>
        /// Gets the name of the officer.
        /// </summary>
        public string Nation { get; }

        /// <summary>
        /// Gets the name of the office.
        /// </summary>
        public string Office { get; }

        /// <summary>
        /// Gets the list of authorities granted by the office.
        /// </summary>
        public HashSet<Authority> Authorities { get; }

        /// <summary>
        /// Gets the time of appointment.
        /// </summary>
        public DateTime Appointed { get; }

        /// <summary>
        /// Gets the name of the nation that appointed the officer.
        /// </summary>
        public string Appointer { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Officer"/> struct.
        /// </summary>
        /// <param name="nation">Name of the officer.</param>
        /// <param name="office">Name of the office.</param>
        /// <param name="authorities">List of authorities granted by the office.</param>
        /// <param name="appointed">Time of appointment.</param>
        /// <param name="appointer">Name of the nation that appointed the officer.</param>
        public Officer(string nation, string office, HashSet<Authority> authorities, DateTime appointed, string appointer)
        {
            this.Nation = nation;
            this.Office = office;
            this.Authorities = authorities;
            this.Appointed = appointed;
            this.Appointer = appointer;
        }
    }
}