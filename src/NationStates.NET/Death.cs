namespace NationStates.NET
{
    /// <summary>
    /// Represents a cause of death.
    /// </summary>
    public class Death
    {
        private double _frequency;

        /// <summary>
        /// Gets or sets the cause of death.
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// Gets or sets the death's national frequency in percentage.
        /// </summary>
        public double Frequency
        {
            get
            {
                return this._frequency;
            }

            set
            {
                if (value > 100 || value < 0)
                {
                    throw new NSError("Frequency must be in the interval [0, 100].");
                }

                this._frequency = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Death"/> class.
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
