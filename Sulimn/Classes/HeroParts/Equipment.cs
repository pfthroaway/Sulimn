using Sulimn.Classes.Items;
using System.ComponentModel;

namespace Sulimn.Classes.HeroParts
{
    /// <summary>Represents pieces of equipment an entity is using.</summary>
    internal class Equipment : INotifyPropertyChanged
    {
        private BodyArmor _body = new BodyArmor();
        private FeetArmor _feet = new FeetArmor();
        private HandArmor _hands = new HandArmor();
        private HeadArmor _head = new HeadArmor();
        private Ring _leftRing = new Ring();
        private LegArmor _legs = new LegArmor();
        private Ring _rightRing = new Ring();
        private Weapon _weapon = new Weapon();

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(property));

        #endregion Data-Binding

        #region Modifying Properties

        /// <summary>The Weapon an entity is using.</summary>
        public Weapon Weapon
        {
            get => _weapon;
            set
            {
                _weapon = value;
                OnPropertyChanged("Weapon");
            }
        }

        /// <summary>The Head Armor an entity is wearing.</summary>
        public HeadArmor Head
        {
            get => _head;
            set
            {
                _head = value;
                OnPropertyChanged("Head");
            }
        }

        /// <summary>The Body Armor an entity is wearing.</summary>
        public BodyArmor Body
        {
            get => _body;
            set
            {
                _body = value;
                OnPropertyChanged("Body");
            }
        }

        /// <summary>The Hand Armor an entity is wearing.</summary>
        public HandArmor Hands
        {
            get => _hands;
            set
            {
                _hands = value;
                OnPropertyChanged("Hands");
            }
        }

        /// <summary>The Leg Armor an entity is wearing.</summary>
        public LegArmor Legs
        {
            get => _legs;
            set
            {
                _legs = value;
                OnPropertyChanged("Legs");
            }
        }

        /// <summary>The Feet Armor an entity is wearing.</summary>
        public FeetArmor Feet
        {
            get => _feet;
            set
            {
                _feet = value;
                OnPropertyChanged("Feet");
            }
        }

        /// <summary>The Ring an entity is wearing on its left hand.</summary>
        public Ring LeftRing
        {
            get => _leftRing;
            set
            {
                _leftRing = value;
                OnPropertyChanged("LeftRing");
            }
        }

        /// <summary>The Ring an entity is wearing on its right hand.</summary>
        public Ring RightRing
        {
            get => _rightRing;
            set
            {
                _rightRing = value;
                OnPropertyChanged("RightRing");
            }
        }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Weight of all the Equipment currently equipped.</summary>
        public int TotalWeight => Weapon.Weight + Body.Weight + Head.Weight + Body.Weight + Hands.Weight + Legs.Weight
                                  + Feet.Weight;

        /// <summary>Returns the total damage produced by the current set of equipment.</summary>
        public int TotalDamage => Weapon.Damage + LeftRing.Damage + RightRing.Damage;

        /// <summary>Returns the total defense produced by the current set of equipment.</summary>
        public int TotalDefense => Head.Defense + Body.Defense + Hands.Defense + Legs.Defense + Feet.Defense + LeftRing.Defense
        + RightRing.Defense;

        /// <summary>Returns the total damage produced by the current set of equipment with thousand separators.</summary>
        public string TotalDefenseToString => TotalDefense.ToString("N0", GameState.CurrentCulture);

        /// <summary>Returns the total damage produced by the current set of equipment with thousand separators and preceding text.</summary>
        public string TotalDefenseToStringWithText => $"Defense: {TotalDefense:N0}";

        /// <summary>Returns the total Strength bonus produced by the current set of equipment.</summary>
        public int BonusStrength => LeftRing.Strength + RightRing.Strength;

        /// <summary>Returns the total Vitality bonus produced by the current set of equipment.</summary>
        public int BonusVitality => LeftRing.Vitality + RightRing.Vitality;

        /// <summary>Returns the total Dexterity bonus produced by the current set of equipment.</summary>
        public int BonusDexterity => LeftRing.Dexterity + RightRing.Dexterity;

        /// <summary>Returns the total Wisdom bonus produced by the current set of equipment.</summary>
        public int BonusWisdom => LeftRing.Wisdom + RightRing.Wisdom;

        #endregion Helper Properties

        #region Override Operators

        private static bool Equals(Equipment left, Equipment right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return left.Head == right.Head
                && left.Body == right.Body
                && left.Hands == right.Hands
                && left.Legs == right.Legs
                && left.Feet == right.Feet
                && left.LeftRing == right.LeftRing
                && left.RightRing == right.RightRing
                && left.Weapon == right.Weapon;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Equipment);

        public bool Equals(Equipment otherEquipment) => Equals(this, otherEquipment);

        public static bool operator ==(Equipment left, Equipment right) => Equals(left, right);

        public static bool operator !=(Equipment left, Equipment right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        #endregion Override Operators

        #region Constructors

        /// <summary>Initializes a default instance of Equipment.</summary>
        public Equipment()
        {
        }

        /// <summary>Initializes an instance of Equipment by assigning Properties.</summary>
        /// <param name="weapon">Weapon</param>
        /// <param name="head">Head Armor</param>
        /// <param name="body">Body Armor</param>
        /// <param name="hands">Hand Armor</param>
        /// <param name="legs">Leg Armor</param>
        /// <param name="feet">Feet Armor</param>
        /// <param name="leftRing">Left Ring</param>
        /// <param name="rightRing">Right Ring</param>
        public Equipment(Weapon weapon, HeadArmor head, BodyArmor body, HandArmor hands, LegArmor legs, FeetArmor feet,
        Ring leftRing, Ring rightRing)
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
        /// <param name="other">Instance of Equipment to replace this instance</param>
        public Equipment(Equipment other) : this(new Weapon(other.Weapon), new HeadArmor(other.Head),
            new BodyArmor(other.Body), new HandArmor(other.Hands), new LegArmor(other.Legs), new FeetArmor(other.Feet),
            new Ring(other.LeftRing), new Ring(other.RightRing))
        {
        }

        #endregion Constructors
    }
}