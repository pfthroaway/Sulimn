using Newtonsoft.Json;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents the statistics of an entity.</summary>
    internal class Statistics : BaseINPC
    {
        private int _currentHealth, _maximumHealth, _currentMagic, _maximumMagic;

        /// <summary>Restores Magic to the entity.</summary>
        /// <param name="restoreAmount">Amount of Magic to be restored</param>
        /// <returns>String saying Magic was restored</returns>
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

        #region Modifying Properties

        /// <summary>Amount of current health the entity has.</summary>
        [JsonProperty(Order = 1)]
        public int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                NotifyPropertyChanged(nameof(CurrentHealth), nameof(HealthToString), nameof(HealthToStringWithText), nameof(HealthRatio));
            }
        }

        /// <summary>Amount of maximum health the entity has.</summary>
        [JsonProperty(Order = 2)]
        public int MaximumHealth
        {
            get => _maximumHealth;
            set
            {
                _maximumHealth = value;
                NotifyPropertyChanged(nameof(MaximumHealth), nameof(HealthToString), nameof(HealthToStringWithText), nameof(HealthRatio));
            }
        }

        /// <summary>Amount of current magic the entity has.</summary>
        [JsonProperty(Order = 3)]
        public int CurrentMagic
        {
            get => _currentMagic;
            set
            {
                _currentMagic = value;
                NotifyPropertyChanged(nameof(CurrentMagic), nameof(MagicToString), nameof(MagicToStringWithText), nameof(MagicRatio));
            }
        }

        /// <summary>Amount of maximum magic the entity has.</summary>
        [JsonProperty(Order = 4)]
        public int MaximumMagic
        {
            get => _maximumMagic;
            set
            {
                _maximumMagic = value;
                NotifyPropertyChanged(nameof(MaximumMagic), nameof(MagicToString), nameof(MagicToStringWithText), nameof(MagicRatio));
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Amount of health the Class has, formatted.</summary>
        [JsonIgnore]
        public string HealthToString => $"{CurrentHealth:N0} / {MaximumHealth:N0}";

        /// <summary>Amount of health the Class has, formatted.</summary>
        [JsonIgnore]
        public string HealthToStringWithText => $"Health: {HealthToString}";

        /// <summary>The amount of current Health in relation to the maximum Health.</summary>
        [JsonIgnore]
        public decimal HealthRatio => CurrentHealth * 1m / MaximumHealth;

        /// <summary>Amount of magic the Class has, formatted with preceding text.</summary>
        [JsonIgnore]
        public string MagicToString => $"{CurrentMagic:N0} / {MaximumMagic:N0}";

        /// <summary>Amount of magic the Class has, formatted with preceding text.</summary>
        [JsonIgnore]
        public string MagicToStringWithText => $"Magic: {MagicToString}";

        /// <summary>The amount of current Magic in relation to the maximum Magic.</summary>
        [JsonIgnore]
        public decimal MagicRatio => CurrentMagic * 1m / MaximumMagic;

        #endregion Helper Properties

        #region Override Operators

        public static bool Equals(Statistics left, Statistics right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return left.CurrentHealth == right.CurrentHealth && left.MaximumHealth == right.MaximumHealth && left.CurrentMagic == right.CurrentMagic && left.MaximumMagic == right.MaximumMagic;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Statistics);

        public bool Equals(Statistics otherStatistics) => Equals(this, otherStatistics);

        public static bool operator ==(Statistics left, Statistics right) => Equals(left, right);

        public static bool operator !=(Statistics left, Statistics right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        #endregion Override Operators

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
        /// <param name="other">Instance to replace this instance</param>
        public Statistics(Statistics other) : this(other.CurrentHealth, other.MaximumHealth, other.CurrentMagic,
            other.MaximumMagic)
        {
        }

        #endregion Constructors
    }
}