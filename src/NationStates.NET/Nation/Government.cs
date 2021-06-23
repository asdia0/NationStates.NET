namespace NationStates.NET.Nation
{
    public class Government
    {
        public double Administration { get; set; }

        public double Defense { get; set; }

        public double Education { get; set; }

        public double Environment { get; set; }

        public double Healthcare { get; set; }

        public double Commerce { get; set; }

        public double InternationalAid { get; set; }

        public double LawAndOrder { get; set; }

        public double PublicTransport { get; set; }

        public double SocialEquality { get; set; }

        public double Spirituality { get; set; }

        public double Welfare { get; set; }

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
