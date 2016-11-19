using System.ComponentModel;

namespace Sulimn
{
    /// <summary>
    /// Represents pieces of equipment an entity is using.
    /// </summary>
    internal class Equipment : INotifyPropertyChanged
    {
        protected Weapon _weapon;
        protected HeadArmor _head;
        protected BodyArmor _body;
        protected LegArmor _legs;
        protected FeetArmor _feet;

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

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

        #endregion Modifying Properties

        #region Helper Properties

        public int TotalDamage
        {
            get { return Weapon.Damage; }
        }

        public int TotalDefense
        {
            get { return Head.Defense + Body.Defense + Legs.Defense + Feet.Defense; }
        }

        public string TotalDefenseToString
        {
            get { return TotalDefense.ToString("N0"); }
        }

        public string TotalDefenseToStringWithText
        {
            get { return "Defense: " + TotalDefense.ToString("N0"); }
        }

        public int BonusStrength
        {
            get { return 0; }
        }

        public int BonusVitality
        {
            get { return 0; }
        }

        public int BonusDexterity
        {
            get { return 0; }
        }

        public int BonusWisdom
        {
            get { return 0; }
        }

        #endregion Helper Properties

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
            Weapon = new Weapon(weapon);
            Head = new HeadArmor(head);
            Body = new BodyArmor(body);
            Legs = new LegArmor(legs);
            Feet = new FeetArmor(feet);
        }

        /// <summary>
        /// Replaces this instance of Equipment with another instance.
        /// </summary>
        /// <param name="otherEquipment">Instance of Equipment to replace this instance</param>
        public Equipment(Equipment otherEquipment)
        {
            Weapon = new Weapon(otherEquipment.Weapon);
            Head = new HeadArmor(otherEquipment.Head);
            Body = new BodyArmor(otherEquipment.Body);
            Legs = new LegArmor(otherEquipment.Legs);
            Feet = new FeetArmor(otherEquipment.Feet);
        }

        #endregion Constructors
    }
}