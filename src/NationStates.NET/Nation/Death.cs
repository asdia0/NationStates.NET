namespace NationStates.NET.Nation
{
    public class Death
    {
        private double _Frequency;

        public string Cause { get; set; }

        public double Frequency
        {
            get
            {
                return this._Frequency;
            }

            set
            {
                if (value > 100 || value < 0)
                {
                    throw new NSError("Frequency must be in the interval [0, 100].");
                }

                this._Frequency = value;
            }
        }

        public Death(string cause, double frequency)
        {
            this.Cause = cause;
            this.Frequency = frequency;
        }
    }
}
