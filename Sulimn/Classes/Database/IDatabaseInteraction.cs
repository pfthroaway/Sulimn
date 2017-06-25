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
        void VerifyDatabaseIntegrity();

        #region Load

        Task<List<HeroClass>> LoadClasses();

        Task<List<HeadArmor>> LoadHeadArmor();

        Task<List<BodyArmor>> LoadBodyArmor();

        Task<List<HandArmor>> LoadHandArmor();

        Task<List<LegArmor>> LoadLegArmor();

        Task<List<FeetArmor>> LoadFeetArmor();

        Task<List<Weapon>> LoadWeapons();

        Task<List<Ring>> LoadRings();

        Task<List<Potion>> LoadPotions();

        Task<List<Spell>> LoadSpells();

        Task<List<Enemy>> LoadEnemies();

        Task<List<Hero>> LoadHeroes();

        Task<Hero> LoadMaxHeroStats();

        Task<List<Food>> LoadFood();

        #endregion Load

        #region Administrator Management

        Task<bool> ChangeAdminPassword(string newPassword);

        Task<string> LoadAdminPassword();

        #endregion Administrator Management

        #region Hero Management

        Task<bool> DeleteHero(Hero deleteHero);

        Task<bool> NewHero(Hero newHero);

        Task<bool> SaveHero(Hero saveHero);

        Task<bool> SaveHeroBank(Hero saveHero, int goldInBank, int loanTaken);

        Task<bool> SaveHeroPassword(Hero saveHero);

        #endregion Hero Management
    }
}