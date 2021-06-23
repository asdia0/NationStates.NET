namespace NationStates.NET.Nation
{
    public class Freedom
    {
        public CivilRights CivilRights { get; set; }

        public Economy Economy { get; set; }

        public PoliticalFreedoms PoliticalFreedom { get; set; }

        public Freedom(CivilRights civilRights, Economy economy, PoliticalFreedoms politicalFreedoms)
        {
            this.CivilRights = civilRights;
            this.Economy = economy;
            this.PoliticalFreedom = politicalFreedoms;
        }
    }
}
