using Sulimn.Classes.Entities;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sulimn.Classes.Database
{
    /// <summary>Represents required interactions for implementations of databases.</summary>
    internal interface IDatabaseInteraction
    {
        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        void VerifyDatabaseIntegrity();

        #region Load

        /// <summary>Loads the initial Bank state and Hero's Bank information.</summary>
        Task<Bank> LoadBank(Hero bankHero);

        /// <summary>Loads all Classes from the database.</summary>
        /// <returns>List of Classes</returns>
        Task<List<HeroClass>> LoadClasses();

        /// <summary>Loads all Head Armor from the database.</summary>
        /// <returns>List of Head Armor</returns>
        Task<List<HeadArmor>> LoadHeadArmor();

        /// <summary>Loads all Body Armor from the database.</summary>
        /// <returns>List of Body Armor</returns>
        Task<List<BodyArmor>> LoadBodyArmor();

        /// <summary>Loads all Hand Armor from the database.</summary>
        /// <returns>List of Hand Armor</returns>
        Task<List<HandArmor>> LoadHandArmor();

        /// <summary>Loads all Leg Armor from the database.</summary>
        /// <returns>List of Leg Armor</returns>
        Task<List<LegArmor>> LoadLegArmor();

        /// <summary>Loads all Feet Armor from the database.</summary>
        /// <returns>List of Feet Armor</returns>
        Task<List<FeetArmor>> LoadFeetArmor();

        /// <summary>Loads all Weapons from the database.</summary>
        /// <returns>List of Weapons</returns>
        Task<List<Weapon>> LoadWeapons();

        /// <summary>Loads all Rings from the database.</summary>
        /// <returns>List of Rings</returns>
        Task<List<Ring>> LoadRings();

        /// <summary>Loads all Potions from the database.</summary>
        /// <returns>List of Potions</returns>
        Task<List<Potion>> LoadPotions();

        /// <summary>Loads all Spells from the database.</summary>
        /// <returns>List of Spells</returns>
        Task<List<Spell>> LoadSpells();

        /// <summary>Loads all Enemies from the database.</summary>
        /// <returns>List of Enemies</returns>
        Task<List<Enemy>> LoadEnemies();

        /// <summary>Loads all Heroes from the database.</summary>
        /// <returns>List of Heroes</returns>
        Task<List<Hero>> LoadHeroes();

        /// <summary>Loads the Hero with maximum possible stats from the database.</summary>
        /// <returns>Hero with maximum possible stats</returns>
        Task<Hero> LoadMaxHeroStats();

        /// <summary>Loads all Food from the database.</summary>
        /// <returns>List of Food</returns>
        Task<List<Food>> LoadFood();

        #endregion Load

        #region Administrator Management

        /// <summary>Changes the administrator password in the database.</summary>
        /// <param name="newPassword">New administrator password</param>
        /// <returns>Returns true if password successfully updated</returns>
        Task<bool> ChangeAdminPassword(string newPassword);

        /// <summary>Loads the Admin password from the database.</summary>
        /// <returns>Admin password</returns>
        Task<string> LoadAdminPassword();

        #endregion Administrator Management

        #region Hero Management

        /// <summary>Modifies a Hero's details in the database.</summary>
        /// <param name="oldHero">Hero whose details need to be modified</param>
        /// <param name="newHero">Hero with new details</param>
        /// <returns>True if successful</returns>
        Task<bool> ChangeHeroDetails(Hero oldHero, Hero newHero);

        /// <summary>Deletes a Hero from the game and database.</summary>
        /// <param name="deleteHero">Hero to be deleted</param>
        /// <returns>Whether deletion was successful</returns>
        Task<bool> DeleteHero(Hero deleteHero);

        /// <summary>Creates a new Hero and adds it to the database.</summary>
        /// <param name="newHero">New Hero</param>
        /// <returns>Returns true if successfully created</returns>
        Task<bool> NewHero(Hero newHero);

        /// <summary>Saves Hero to database.</summary>
        /// <param name="saveHero">Hero to be saved</param>
        /// <returns>Returns true if successfully saved</returns>
        Task<bool> SaveHero(Hero saveHero);

        /// <summary>Saves the Hero's bank information.</summary>
        /// <param name="saveHero">Hero whose Bank needs to be saved</param>
        /// <param name="goldInBank">Gold in the bank</param>
        /// <param name="loanTaken">Loan taken out</param>
        Task<bool> SaveHeroBank(Hero saveHero, int goldInBank, int loanTaken);

        /// <summary>Saves the Hero's password to the database.</summary>
        /// <param name="saveHero">Hero whose password needs to be saved</param>
        Task<bool> SaveHeroPassword(Hero saveHero);

        #endregion Hero Management
    }
}