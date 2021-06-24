﻿namespace NationStates.NET.Nation
{
    /// <summary>
    /// Represents a government.
    /// </summary>
    public class Government
    {
        /// <summary>
        /// Percentage of government budget spent on administration.
        /// </summary>
        public double Administration { get; set; }

        /// <summary>
        /// Percentage of government budget spent on defence.
        /// </summary>
        public double Defense { get; set; }

        /// <summary>
        /// Percentage of government budget spent on education.
        /// </summary>
        public double Education { get; set; }

        /// <summary>
        /// Percentage of government budget spent on the environment.
        /// </summary>
        public double Environment { get; set; }

        /// <summary>
        /// Percentage of government budget spent on healthcare.
        /// </summary>
        public double Healthcare { get; set; }

        /// <summary>
        /// Percentage of government budget spent on commerce.
        /// </summary>
        public double Commerce { get; set; }

        /// <summary>
        /// Percentage of government budget spent on international aid.
        /// </summary>
        public double InternationalAid { get; set; }

        /// <summary>
        /// Percentage of government budget spent on law and order.
        /// </summary>
        public double LawAndOrder { get; set; }

        /// <summary>
        /// Percentage of government budget spent on public transport.
        /// </summary>
        public double PublicTransport { get; set; }

        /// <summary>
        /// Percentage of government budget spent on social equality.
        /// </summary>
        public double SocialEquality { get; set; }

        /// <summary>
        /// Percentage of government budget spent on spirituality.
        /// </summary>
        public double Spirituality { get; set; }

        /// <summary>
        /// Percentage of government budget spent on welfare.
        /// </summary>
        public double Welfare { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Government"/> class.
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
    }
}
