using System.ComponentModel;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents the attributes an entity has.</summary>
    internal class Attributes : INotifyPropertyChanged
    {
        private int _strength, _vitality, _dexterity, _wisdom;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>How strong an entity is.</summary>
        public int Strength
        {
            get => _strength;
            set
            {
                _strength = value;
                OnPropertyChanged("Strength");
            }
        }

        /// <summary>How much health an entity can have.</summary>
        public int Vitality
        {
            get => _vitality;
            set
            {
                _vitality = value;
                OnPropertyChanged("Vitality");
            }
        }

        /// <summary>How fast an entity can move.</summary>
        public int Dexterity
        {
            get => _dexterity;
            set
            {
                _dexterity = value;
                OnPropertyChanged("Dexterity");
            }
        }

        /// <summary>How magically-inclined an entity is.</summary>
        public int Wisdom
        {
            get => _wisdom;
            set
            {
                _wisdom = value;
                OnPropertyChanged("Wisdom");
            }
        }

        #endregion Modifying Properties

        #region Override Operators

        public static bool Equals(Attributes left, Attributes right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return left.Strength == right.Strength && left.Vitality == right.Vitality && left.Dexterity == right.Dexterity && left.Wisdom == right.Wisdom;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Attributes);

        public bool Equals(Attributes otherAttributes) => Equals(this, otherAttributes);

        public static bool operator ==(Attributes left, Attributes right) => Equals(left, right);

        public static bool operator !=(Attributes left, Attributes right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of Attributes.</summary>
        public Attributes()
        {
        }

        /// <summary>Initializes an instance of Attributes by assigning Properties.</summary>
        /// <param name="strength">Strength</param>
        /// <param name="vitality">Vitality</param>
        /// <param name="dexterity">Dexterity</param>
        /// <param name="wisdom">Wisdom</param>
        public Attributes(int strength, int vitality, int dexterity, int wisdom)
        {
            Strength = strength;
            Vitality = vitality;
            Dexterity = dexterity;
            Wisdom = wisdom;
        }

        /// <summary>Replaces this instance of Attributes with another instance.</summary>
        /// <param name="other">Instance to replace this instance</param>
        public Attributes(Attributes other) : this(other.Strength, other.Vitality, other.Dexterity, other.Wisdom)
        {
        }

        #endregion Constructors
    }
}