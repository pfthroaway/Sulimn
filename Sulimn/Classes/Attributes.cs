using System.ComponentModel;

namespace Sulimn
{
    internal class Attributes : INotifyPropertyChanged
    {
        protected int _strength, _vitality, _dexterity, _wisdom;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public int Strength
        {
            get { return _strength; }
            set { _strength = value; OnPropertyChanged("Strength"); }
        }

        public int Vitality
        {
            get { return _vitality; }
            set { _vitality = value; OnPropertyChanged("Vitality"); }
        }

        public int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; OnPropertyChanged("Dexterity"); }
        }

        public int Wisdom
        {
            get { return _wisdom; }
            set { _wisdom = value; OnPropertyChanged("Wisdom"); }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Attributes.
        /// </summary>
        public Attributes()
        {
        }

        /// <summary>
        /// Initializes an instance of Attributes by assigning Properties.
        /// </summary>
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

        /// <summary>
        /// Replaces this instance of Attributes with another instance.
        /// </summary>
        /// <param name="otherAttributes">Instance to replace this instance</param>
        public Attributes(Attributes otherAttributes)
        {
            Strength = otherAttributes.Strength;
            Vitality = otherAttributes.Vitality;
            Dexterity = otherAttributes.Dexterity;
            Wisdom = otherAttributes.Wisdom;
        }

        #endregion Constructors
    }
}