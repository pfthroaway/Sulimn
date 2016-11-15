using System.ComponentModel;

namespace Sulimn
{
    internal class Equipment : INotifyPropertyChanged
    {
        protected Weapon _weapon;
        protected HeadArmor _head;
        protected BodyArmor _body;
        protected LegArmor _legs;
        protected FeetArmor _feet;

        #region Data-Binding

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Properties

        public Weapon Weapon
        {
            get { return _weapon; }
            set { _weapon = value; OnPropertyChanged("Weapon"); }
        }

        public HeadArmor Head
        {
            get { return _head; }
            set { _head = value; OnPropertyChanged("Head"); }
        }

        public BodyArmor Body
        {
            get { return _body; }
            set { _body = value; OnPropertyChanged("Body"); }
        }

        public LegArmor Legs
        {
            get { return _legs; }
            set { _legs = value; OnPropertyChanged("Legs"); }
        }

        public FeetArmor Feet
        {
            get { return _feet; }
            set { _feet = value; OnPropertyChanged("Feet"); }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a default instance of Equipment.
        /// </summary>
        public Equipment()
        {
        }

        /// <summary>
        /// Initializes an instance of Equipment by assigning Properties.
        /// </summary>
        /// <param name="weapon">Weapon</param>
        /// <param name="head">Head Armor</param>
        /// <param name="body">Body Armor</param>
        /// <param name="legs">Legs</param>
        /// <param name="feet">Feet</param>
        public Equipment(Weapon weapon, HeadArmor head, BodyArmor body, LegArmor legs, FeetArmor feet)
        {
            Weapon = weapon;
            Head = head;
            Body = body;
            Legs = legs;
            Feet = feet;
        }

        /// <summary>
        /// Replaces this instance of Equipment with another instance.
        /// </summary>
        /// <param name="otherEquipment">Instance of Equipment to replace this instance</param>
        public Equipment(Equipment otherEquipment)
        {
            Weapon = otherEquipment.Weapon;
            Head = otherEquipment.Head;
            Body = otherEquipment.Body;
            Legs = otherEquipment.Legs;
            Feet = otherEquipment.Feet;
        }

        #endregion Constructors
    }
}