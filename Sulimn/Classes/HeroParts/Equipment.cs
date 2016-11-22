using System.ComponentModel;

namespace Sulimn
{
    /// <summary>Represents pieces of equipment an entity is using.</summary>
    internal class Equipment : INotifyPropertyChanged
    {
        protected Weapon _weapon = new Weapon();
        protected HeadArmor _head = new HeadArmor();
        protected BodyArmor _body = new BodyArmor();
        protected HandArmor _hands = new HandArmor();
        protected LegArmor _legs = new LegArmor();
        protected FeetArmor _feet = new FeetArmor();
        protected Ring _leftRing = new Ring();
        protected Ring _rightRing = new Ring();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>The Weapon an entity is using.</summary>
        public Weapon Weapon
        {
            get { return _weapon; }
            set { _weapon = value; OnPropertyChanged("Weapon"); }
        }

        /// <summary>The Head Armor an entity is wearing.</summary>
        public HeadArmor Head
        {
            get { return _head; }
            set { _head = value; OnPropertyChanged("Head"); }
        }

        /// <summary>The Body Armor an entity is wearing.</summary>
        public BodyArmor Body
        {
            get { return _body; }
            set { _body = value; OnPropertyChanged("Body"); }
        }

        /// <summary>The Hand Armor an entity is wearing.</summary>
        public HandArmor Hands
        {
            get { return _hands; }
            set { _hands = value; OnPropertyChanged("Hands"); }
        }

        /// <summary>The Leg Armor an entity is wearing.</summary>
        public LegArmor Legs
        {
            get { return _legs; }
            set { _legs = value; OnPropertyChanged("Legs"); }
        }

        /// <summary>The Feet Armor an entity is wearing.</summary>
        public FeetArmor Feet
        {
            get { return _feet; }
            set { _feet = value; OnPropertyChanged("Feet"); }
        }

        /// <summary>The Ring an entity is wearing on its left hand.</summary>
        public Ring LeftRing
        {
            get { return _leftRing; }
            set { _leftRing = value; OnPropertyChanged("LeftRing"); }
        }

        /// <summary>The Ring an entity is wearing on its right hand.</summary>
        public Ring RightRing
        {
            get { return _rightRing; }
            set { _rightRing = value; OnPropertyChanged("RightRing"); }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Returns the total damage produced by the current set of equipment.</summary>
        public int TotalDamage
        {
            get { return Weapon.Damage; }
        }

        /// <summary>Returns the total defense produced by the current set of equipment.</summary>
        public int TotalDefense
        {
            get { return Head.Defense + Body.Defense + Hands.Defense + Legs.Defense + Feet.Defense + LeftRing.Defense + RightRing.Defense; }
        }

        /// <summary>Returns the total damage produced by the current set of equipment with thousand separators.</summary>
        public string TotalDefenseToString
        {
            get { return TotalDefense.ToString("N0"); }
        }

        /// <summary>Returns the total damage produced by the current set of equipment with thousand separators and preceding text.</summary>
        public string TotalDefenseToStringWithText
        {
            get { return "Defense: " + TotalDefense.ToString("N0"); }
        }

        /// <summary>Returns the total Strength bonus produced by the current set of equipment.</summary>
        public int BonusStrength
        {
            get { return 0; }
        }

        /// <summary>Returns the total Vitality bonus produced by the current set of equipment.</summary>
        public int BonusVitality
        {
            get { return 0; }
        }

        /// <summary>Returns the total Dexterity bonus produced by the current set of equipment.</summary>
        public int BonusDexterity
        {
            get { return 0; }
        }

        /// <summary>Returns the total Wisdom bonus produced by the current set of equipment.</summary>
        public int BonusWisdom
        {
            get { return 0; }
        }

        #endregion Helper Properties

        #region Constructors

        /// <summary>Initializes a default instance of Equipment.</summary>
        public Equipment()
        {
        }

        /// <summary>Initializes an instance of Equipment by assigning Properties.</summary>
        /// <param name="weapon">Weapon</param>
        /// <param name="head">Head Armor</param>
        /// <param name="body">Body Armor</param>
        /// <param name="legs">Legs</param>
        /// <param name="feet">Feet</param>
        public Equipment(Weapon weapon, HeadArmor head, HandArmor hands, BodyArmor body, LegArmor legs, FeetArmor feet, Ring leftRing, Ring rightRing)
        {
            Weapon = new Weapon(weapon);
            Head = new HeadArmor(head);
            Body = new BodyArmor(body);
            Hands = new HandArmor(hands);
            Legs = new LegArmor(legs);
            Feet = new FeetArmor(feet);
            LeftRing = new Ring(leftRing);
            RightRing = new Ring(rightRing);
        }

        /// <summary>Replaces this instance of Equipment with another instance.</summary>
        /// <param name="otherEquipment">Instance of Equipment to replace this instance</param>
        public Equipment(Equipment otherEquipment)
        {
            Weapon = new Weapon(otherEquipment.Weapon);
            Head = new HeadArmor(otherEquipment.Head);
            Body = new BodyArmor(otherEquipment.Body);
            Hands = new HandArmor(otherEquipment.Hands);
            Legs = new LegArmor(otherEquipment.Legs);
            Feet = new FeetArmor(otherEquipment.Feet);
            LeftRing = new Ring(otherEquipment.LeftRing);
            RightRing = new Ring(otherEquipment.RightRing);
        }

        #endregion Constructors
    }
}