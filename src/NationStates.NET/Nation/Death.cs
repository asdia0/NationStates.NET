namespace NationStates.NET.Nation
{
    /// <summary>
    /// Represents a cause of death.
    /// </summary>
    public class Death
    {
        private double _Frequency;

        /// <summary>
        /// Cause of death.
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// Frequency in percentage.
        /// </summary>
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

        /// <summary>
        /// Initialises a new instance of the <see cref="Death"/> class.
        /// </summary>
        /// <param name="cause">Cause of death.</param>
        /// <param name="frequency">Frequency in percentage.</param>
        public Death(string cause, double frequency)
        {
            this.Cause = cause;
            this.Frequency = frequency;
        }
    }
}
