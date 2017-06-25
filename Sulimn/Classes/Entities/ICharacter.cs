using Sulimn.Classes.HeroParts;

namespace Sulimn.Classes.Entities
{
    internal interface ICharacter
    {
        #region Modifying Properties

        string Name { get; set; }
        int Level { get; set; }
        int Experience { get; set; }
        Attributes Attributes { get; set; }
        Statistics Statistics { get; set; }
        Equipment Equipment { get; set; }
        Inventory Inventory { get; set; }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>The experience the Hero has gained this level alongside how much is needed to level up</summary>
        string ExperienceToString { get; }

        /// <summary>The experience the Hero has gained this level alongside how much is needed to level up with preceding text</summary>
        string ExperienceToStringWithText { get; }

        /// <summary>Returns the total Strength attribute and bonus produced by the current set of equipment.</summary>
        int TotalStrength { get; }

        /// <summary>Returns the total Vitality attribute and bonus produced by the current set of equipment.</summary>
        int TotalVitality { get; }

        /// <summary>Returns the total Dexterity attribute and bonus produced by the current set of equipment.</summary>
        int TotalDexterity { get; }

        /// <summary>Returns the total Wisdom attribute and bonus produced by the current set of equipment.</summary>
        int TotalWisdom { get; }

        #endregion Helper Properties
    }
}