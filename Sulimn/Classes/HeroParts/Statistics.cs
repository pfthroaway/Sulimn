using System.ComponentModel;

namespace Sulimn
{
    /// <summary>Represents the statistics of an entity.</summary>
    internal class Statistics : INotifyPropertyChanged
    {
        private int _currentHealth, _maximumHealth, _currentMagic, _maximumMagic;

        /// <summary>Restores magic to the Hero.</summary>
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
            return $"You restore {restoreAmount:N0} magic.";
        }

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>Amount of current health the Class has.</summary>
        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                OnPropertyChanged("HealthToString");
                OnPropertyChanged("HealthToStringWithText");
            }
        }

        /// <summary>Amount of maximum health the Class has.</summary>
        public int MaximumHealth
        {
            get => _maximumHealth;
            set
            {
                _maximumHealth = value;
                OnPropertyChanged("HealthToString");
                OnPropertyChanged("HealthToStringWithText");
            }
        }

        /// <summary>Amount of current magic the Class has.</summary>
        public int CurrentMagic
        {
            get => _currentMagic;
            set
            {
                _currentMagic = value;
                OnPropertyChanged("MagicToString");
                OnPropertyChanged("MagicToStringWithText");
            }
        }

        /// <summary>Amount of maximum magic the Class has.</summary>
        public int MaximumMagic
        {
            get => _maximumMagic;
            set
            {
                _maximumMagic = value;
                OnPropertyChanged("MagicToString");
                OnPropertyChanged("MagicToStringWithText");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Amount of health the Class has, formatted.</summary>
        public string HealthToString => $"{CurrentHealth:N0} / {MaximumHealth:N0}";

        /// <summary>Amount of health the Class has, formatted.</summary>
        public string HealthToStringWithText => $"Health: {HealthToString}";

        /// <summary>Amount of magic the Class has, formatted with preceding text.</summary>
        public string MagicToString => $"{CurrentMagic:N0} / {MaximumMagic:N0}";

        /// <summary>Amount of magic the Class has, formatted with preceding text.</summary>
        public string MagicToStringWithText => $"Magic: {MagicToString}";

        #endregion Helper Properties

        #region Constructors

        /// <summary>Initializes a default instance of Statistics.</summary>
        public Statistics()
        {
        }

        /// <summary>Initializes an instance of Statistics by assigning Properties.</summary>
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

        /// <summary>Replaces this instance of Statistics with another instance.</summary>
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