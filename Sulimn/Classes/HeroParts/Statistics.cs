using System.ComponentModel;

namespace Sulimn
{
    internal class Statistics : INotifyPropertyChanged
    {
        private int _currentHealth, _maximumHealth, _currentMagic, _maximumMagic;

        /// <summary>
        /// Restores magic to the Hero.
        /// </summary>
        /// <param name="restoreAmount">Amount of Magic to be restored.</param>
        /// <returns>String saying magic was restored</returns>
        internal string RestoreMagic(int restoreAmount)
        {
            CurrentMagic += restoreAmount;
            if (CurrentMagic > MaximumMagic)
            {
                CurrentMagic = MaximumMagic;
                return "You restore your magic to its maximum.";
            }
            return "You restore " + restoreAmount + " magic.";
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = value;
                OnPropertyChanged("HealthToString");
                OnPropertyChanged("HealthToStringWithText");
            }
        }

        public int MaximumHealth
        {
            get { return _maximumHealth; }
            set
            {
                _maximumHealth = value;
                OnPropertyChanged("HealthToString");
                OnPropertyChanged("HealthToStringWithText");
            }
        }

        public int CurrentMagic
        {
            get { return _currentMagic; }
            set
            {
                _currentMagic = value;
                OnPropertyChanged("MagicToString");
                OnPropertyChanged("MagicToStringWithText");
            }
        }

        public int MaximumMagic
        {
            get { return _maximumMagic; }
            set
            {
                _maximumMagic = value;
                OnPropertyChanged("MagicToString");
                OnPropertyChanged("MagicToStringWithText");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        public string HealthToString => CurrentHealth.ToString("N0") + " / " + MaximumHealth.ToString("N0");

        public string HealthToStringWithText => "Health: " + HealthToString;

        public string MagicToString => CurrentMagic.ToString("N0") + " / " + MaximumMagic.ToString("N0");

        public string MagicToStringWithText => "Magic: " + MagicToString;

        #endregion Helper Properties

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Statistics.
        /// </summary>
        public Statistics()
        {
        }

        /// <summary>
        /// Initializes an instance of Statistics by assigning Properties.
        /// </summary>
        /// <param name="currentHealth">Current Health</param>
        /// <param name="maximumHealth">Maximum Health</param>
        /// <param name="currentMagic">Current Magic</param>
        /// <param name="maximumMagic">Maximum Magic</param>
        public Statistics(int currentHealth, int maximumHealth, int currentMagic, int maximumMagic)
        {
            CurrentHealth = currentHealth;
            MaximumHealth = maximumHealth;
            CurrentMagic = currentMagic;
            MaximumMagic = maximumMagic;
        }

        /// <summary>
        /// Replaces this instance of Statistics with another instance.
        /// </summary>
        /// <param name="otherStatistics">Instance to replace this instance</param>
        public Statistics(Statistics otherStatistics)
        {
            CurrentHealth = otherStatistics.CurrentHealth;
            MaximumHealth = otherStatistics.MaximumHealth;
            CurrentMagic = otherStatistics.CurrentMagic;
            MaximumMagic = otherStatistics.MaximumMagic;
        }

        #endregion Constructors
    }
}