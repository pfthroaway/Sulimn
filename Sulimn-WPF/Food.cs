using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulimn_WPF
{
    class Food : Item, IEquatable<Food>
    {
        string _foodType;
        int _amount;

        #region Properties
        public sealed override string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public sealed override string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string FoodType { get { return _foodType; } set { _foodType = value; } }

        public sealed override string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public sealed override int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public sealed override int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public sealed override bool CanSell
        {
            get { return _canSell; }
            set { _canSell = value; }
        }
        #endregion

        #region Override Operators
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Food food = obj as Food;
            if ((object)food == null)
                return false;

            return (this.Name == food.Name) && (this.Type == food.Type) && (this.Description == food.Description) && (this.Amount == food.Amount) && (this.Weight == food.Weight) && (this.Value == food.Value) && (this.CanSell == food.CanSell);
        }

        public bool Equals(Food otherFood)
        {
            if ((object)otherFood == null)
                return false;

            return (this.Name == otherFood.Name) && (this.Type == otherFood.Type) && (this.Description == otherFood.Description) && (this.Amount == otherFood.Amount) && (this.Weight == otherFood.Weight) && (this.Value == otherFood.Value) && (this.CanSell == otherFood.CanSell);
        }

        public static bool operator ==(Food left, Food right)
        {
            if (System.Object.ReferenceEquals(left, right))
                return true;

            if (((object)left == null) || ((object)right == null))
                return false;

            return (left.Name == right.Name) && (left.Type == right.Type) && (left.Description == right.Description) && (left.Amount == right.Amount) && (left.Weight == right.Weight) && (left.Value == right.Value) && (left.CanSell == right.CanSell);
        }

        public static bool operator !=(Food left, Food right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ 17;
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region Constructors
        internal Food() { }

        internal Food(string foodName, string foodType, string foodDescription, int foodAmount, int foodWeight, int foodValue, bool foodCanSell)
        {
            Name = foodName;
            Type = "Food";
            FoodType = foodType;
            Description = foodDescription;
            Weight = foodWeight;
            Value = foodValue;
            Amount = foodAmount;
            CanSell = foodCanSell;
        }
        #endregion
    }
}