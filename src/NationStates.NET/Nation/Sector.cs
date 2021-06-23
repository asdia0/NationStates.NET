namespace NationStates.NET.Nation
{
    public class Sector
    {
        public SectorType Type { get; set; }

        public double Frequency { get; set; }

        public Sector(SectorType type, double frequency)
        {
            this.Type = type;
            this.Frequency = frequency;
        }
    }
}
