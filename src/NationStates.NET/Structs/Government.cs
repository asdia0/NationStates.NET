namespace NationStates.NET
{
    using Newtonsoft.Json;
    using static Utility;

    /// <summary>
    /// Represents a nation's government.
    /// </summary>
    public struct Government
    {
        /// <summary>
        /// Gets the percentage of the government budget spent on administration.
        /// </summary>
        [JsonProperty]
        public double Administration { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on commerce.
        /// </summary>
        [JsonProperty]
        public double Commerce { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on defence.
        /// </summary>
        [JsonProperty]
        public double Defense { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on education.
        /// </summary>
        [JsonProperty]
        public double Education { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on the environment.
        /// </summary>
        [JsonProperty]
        public double Environment { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on healthcare.
        /// </summary>
        [JsonProperty]
        public double Healthcare { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on international aid.
        /// </summary>
        [JsonProperty]
        public double InternationalAid { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on law and order.
        /// </summary>
        [JsonProperty]
        public double LawAndOrder { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on public transport.
        /// </summary>
        [JsonProperty]
        public double PublicTransport { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on social equality.
        /// </summary>
        [JsonProperty]
        public double SocialEquality { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on spirituality.
        /// </summary>
        [JsonProperty]
        public double Spirituality { get; }

        /// <summary>
        /// Gets the percentage of the government budget spent on welfare.
        /// </summary>
        [JsonProperty]
        public double Welfare { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Government"/> struct.
        /// </summary>
        /// <param name="administration">Percentage of government budget spent on administration.</param>
        /// <param name="defense">Percentage of government budget spent on defense.</param>
        /// <param name="education">Percentage of government budget spent on education.</param>
        /// <param name="environment">Percentage of government budget spent on the environment.</param>
        /// <param name="healthcare">Percentage of government budget spent on healthcare.</param>
        /// <param name="commerce">Percentage of government budget spent on commerce.</param>
        /// <param name="internationalAid">Percentage of government budget spent on international aid.</param>
        /// <param name="lawAndOrder">Percentage of government budget spent on law and order.</param>
        /// <param name="publicTransport">Percentage of government budget spent on public transport.</param>
        /// <param name="socialEquality">Percentage of government budget spent on social equality.</param>
        /// <param name="spirituality">Percentage of government budget spent on sprituality.</param>
        /// <param name="welfare">Percentage of government budget spent on welfare.</param>
        public Government(double administration, double defense, double education, double environment, double healthcare, double commerce, double internationalAid, double lawAndOrder, double publicTransport, double socialEquality, double spirituality, double welfare)
        {
            this.Administration = administration;
            this.Defense = defense;
            this.Education = education;
            this.Environment = environment;
            this.Healthcare = healthcare;
            this.Commerce = commerce;
            this.InternationalAid = internationalAid;
            this.LawAndOrder = lawAndOrder;
            this.PublicTransport = publicTransport;
            this.SocialEquality = socialEquality;
            this.Spirituality = spirituality;
            this.Welfare = welfare;
        }

        /// <summary>
        /// Gets a JSON string representing the nation's government.
        /// </summary>
        /// <returns>A JSON string representing the nation's government.</returns>
        public override string ToString()
        {
            return Serialize(this);
        }
    }
}