namespace NationStates.NET
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using static Utility;

    /// <summary>
    /// Represents a regional officer.
    /// </summary>
    public struct Officer
    {
        /// <summary>
        /// Gets the time of appointment.
        /// </summary>
        [JsonProperty]
        public DateTime Appointed { get; }

        /// <summary>
        /// Gets the name of the nation that appointed the officer.
        /// </summary>
        [JsonProperty]
        public string Appointer { get; }

        /// <summary>
        /// Gets the list of authorities granted by the office.
        /// </summary>
        [JsonProperty]
        public HashSet<Authority> Authorities { get; }

        /// <summary>
        /// Gets the name of the officer.
        /// </summary>
        [JsonProperty]
        public string Nation { get; }

        /// <summary>
        /// Gets the name of the office.
        /// </summary>
        [JsonProperty]
        public string Office { get; }

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

        /// <summary>
        /// Gets a JSON string representing the regional officer.
        /// </summary>
        /// <returns>A JSON string representing the regional officer.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}