namespace NationStates.NET
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a regional officer.
    /// </summary>
    public class Officer
    {
        /// <summary>
        /// Name of the officer.
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// Name of the office.
        /// </summary>
        public string Office { get; set; }

        /// <summary>
        /// List of authorities granted by the office.
        /// </summary>
        public HashSet<Authority> Authorities { get; set; }

        /// <summary>
        /// Time of appointment.
        /// </summary>
        public DateTime Appointed { get; set; }

        /// <summary>
        /// Name of the nation that appointed the officer.
        /// </summary>
        public string Appointer { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Officer"/> class.
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
