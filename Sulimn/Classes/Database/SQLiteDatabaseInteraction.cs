using Extensions;
using Extensions.DatabaseHelp;
using Extensions.DataTypeHelpers;
using Sulimn.Classes.Entities;
using Sulimn.Classes.Enums;
using Sulimn.Classes.HeroParts;
using Sulimn.Classes.Items;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Threading.Tasks;

namespace Sulimn.Classes.Database
{
    /// <summary>Represents database interaction covered by SQLite.</summary>
    internal class SQLiteDatabaseInteraction : IDatabaseInteraction
    {
        private readonly string _con = $"Data Source = {_DATABASENAME}; foreign keys = TRUE;Version = 3; ";

        // ReSharper disable once InconsistentNaming
        private const string _DATABASENAME = "Sulimn.sqlite";

        #region Database Interaction

        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        public void VerifyDatabaseIntegrity()
        {
            Functions.VerifyFileIntegrity(
            Assembly.GetExecutingAssembly().GetManifestResourceStream($"Sulimn.{_DATABASENAME}"), _DATABASENAME);
        }

        #endregion Database Interaction

        #region Hero Management

        /// <summary>Modifies a Hero's details in the database.</summary>
        /// <param name="oldHero">Hero whose details need to be modified</param>
        /// <param name="newHero">Hero with new details</param>
        /// <returns>True if successful</returns>
        public async Task<bool> ChangeHeroDetails(Hero oldHero, Hero newHero)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "UPDATE Players Set [CharacterName] = @name, [CharacterPassword] = @password WHERE [CharacterName] = @oldName" };
            cmd.Parameters.AddWithValue("name", newHero.Name);
            cmd.Parameters.AddWithValue("password", newHero.Password);
            cmd.Parameters.AddWithValue("oldName", oldHero.Name);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Deletes a Hero from the game and database.</summary>
        /// <param name="deleteHero">Hero to be deleted</param>
        /// <returns>Whether deletion was successful</returns>
        public async Task<bool> DeleteHero(Hero deleteHero)
        {
            SQLiteCommand cmd = new SQLiteCommand()
            {
                CommandText =
            "DELETE FROM Players WHERE [CharacterName] = @name;DELETE FROM Bank WHERE [CharacterName] = @name;DELETE FROM Equipment WHERE [CharacterName] = @name"
            };
            cmd.Parameters.AddWithValue("@name", deleteHero.Name);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Creates a new Hero and adds it to the database.</summary>
        /// <param name="newHero">New Hero</param>
        /// <returns>Returns true if successfully created</returns>
        public async Task<bool> NewHero(Hero newHero)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText =
            "INSERT INTO Players([CharacterName], [CharacterPassword], [Class], [Level], [Experience], [SkillPoints], [Strength], [Vitality], [Dexterity], [Wisdom], [Gold], [CurrentHealth], [MaximumHealth], [CurrentMagic], [MaximumMagic], [KnownSpells], [Inventory], [Hardcore])VALUES(@name, @password, @class, @level, @experience, @skillPoints, @strength, @vitality, @dexterity, @wisdom, @gold, @currentHealth, @maximumHealth, @maximumMagic, @maximumMagic, @spells, @inventory, @hardcore);" +
            "INSERT INTO Bank([CharacterName], [Gold], [LoanTaken])Values(@name, 0, 0);" +
            "INSERT INTO Equipment([CharacterName], [Weapon], [Head], [Body], [Hands], [Legs], [Feet])VALUES(@name, @weapon, @head, @body, @hands, @legs, @feet)"
            };
            cmd.Parameters.AddWithValue("@name", newHero.Name);
            cmd.Parameters.AddWithValue("@password", newHero.Password.Replace("\0", ""));
            cmd.Parameters.AddWithValue("@class", newHero.Class.Name);
            cmd.Parameters.AddWithValue("@level", newHero.Level);
            cmd.Parameters.AddWithValue("@experience", newHero.Experience);
            cmd.Parameters.AddWithValue("@skillPoints", newHero.SkillPoints);
            cmd.Parameters.AddWithValue("@strength", newHero.Attributes.Strength);
            cmd.Parameters.AddWithValue("@vitality", newHero.Attributes.Vitality);
            cmd.Parameters.AddWithValue("@dexterity", newHero.Attributes.Dexterity);
            cmd.Parameters.AddWithValue("@wisdom", newHero.Attributes.Wisdom);
            cmd.Parameters.AddWithValue("@gold", newHero.Inventory.Gold);
            cmd.Parameters.AddWithValue("@currentHealth", newHero.Statistics.CurrentHealth);
            cmd.Parameters.AddWithValue("@maximumHealth", newHero.Statistics.MaximumHealth);
            cmd.Parameters.AddWithValue("@currentMagic", newHero.Statistics.CurrentMagic);
            cmd.Parameters.AddWithValue("@maximumMagic", newHero.Statistics.MaximumMagic);
            cmd.Parameters.AddWithValue("@spells", newHero.Spellbook.ToString());
            cmd.Parameters.AddWithValue("@inventory", newHero.Inventory.ToString());
            cmd.Parameters.AddWithValue("@hardcore", Int32Helper.Parse(newHero.Hardcore));
            cmd.Parameters.AddWithValue("@weapon", newHero.Equipment.Weapon.Name);
            cmd.Parameters.AddWithValue("@head", newHero.Equipment.Head.Name);
            cmd.Parameters.AddWithValue("@body", newHero.Equipment.Body.Name);
            cmd.Parameters.AddWithValue("@hands", newHero.Equipment.Hands.Name);
            cmd.Parameters.AddWithValue("@legs", newHero.Equipment.Legs.Name);
            cmd.Parameters.AddWithValue("@feet", newHero.Equipment.Feet.Name);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Saves Hero to database.</summary>
        /// <param name="saveHero">Hero to be saved</param>
        /// <returns>Returns true if successfully saved</returns>
        public async Task<bool> SaveHero(Hero saveHero)
        {
            SQLiteCommand cmd = new SQLiteCommand()
            {
                CommandText =
            "UPDATE Players SET [Level] = @level, [Experience] = @experience, [SkillPoints] = @skillPoints, [Strength] = @strength, [Vitality] = @vitality, [Dexterity] = @dexterity, [Wisdom] = @wisdom, [Gold] = @gold, [CurrentHealth] = @currentHealth, [MaximumHealth] = @maximumHealth, [CurrentMagic] = @currentMagic, [MaximumMagic] = @maximumMagic, [KnownSpells] = @spells, [Inventory] = @inventory, [HardCore] = @hardcore WHERE [CharacterName] = @name;" +
            "UPDATE Equipment SET[Weapon] = @weapon,[Head] = @head,[Body] = @body,[Hands] = hands,[Legs] = @legs,[Feet] = @feet,[LeftRing] = @leftRing,[RightRing] = @rightRing WHERE [CharacterName] = @name"
            };
            cmd.Parameters.AddWithValue("@level", saveHero.Level);
            cmd.Parameters.AddWithValue("@experience", saveHero.Experience.ToString());
            cmd.Parameters.AddWithValue("@skillPoints", saveHero.SkillPoints.ToString());
            cmd.Parameters.AddWithValue("@strength", saveHero.Attributes.Strength.ToString());
            cmd.Parameters.AddWithValue("@vitality", saveHero.Attributes.Vitality.ToString());
            cmd.Parameters.AddWithValue("@dexterity", saveHero.Attributes.Dexterity.ToString());
            cmd.Parameters.AddWithValue("@wisdom", saveHero.Attributes.Wisdom.ToString());
            cmd.Parameters.AddWithValue("@gold", saveHero.Inventory.Gold.ToString());
            cmd.Parameters.AddWithValue("@currentHealth", saveHero.Statistics.CurrentHealth.ToString());
            cmd.Parameters.AddWithValue("@maximumHealth", saveHero.Statistics.MaximumHealth.ToString());
            cmd.Parameters.AddWithValue("@currentMagic", saveHero.Statistics.CurrentMagic.ToString());
            cmd.Parameters.AddWithValue("@maximumMagic", saveHero.Statistics.MaximumMagic.ToString());
            cmd.Parameters.AddWithValue("@spells", saveHero.Spellbook.ToString());
            cmd.Parameters.AddWithValue("@inventory", saveHero.Inventory.ToString());
            cmd.Parameters.AddWithValue("@hardcore", Int32Helper.Parse(saveHero.Hardcore));
            cmd.Parameters.AddWithValue("@name", saveHero.Name);
            cmd.Parameters.AddWithValue("@weapon", saveHero.Equipment.Weapon.Name);
            cmd.Parameters.AddWithValue("@head", saveHero.Equipment.Head.Name);
            cmd.Parameters.AddWithValue("@body", saveHero.Equipment.Body.Name);
            cmd.Parameters.AddWithValue("@hands", saveHero.Equipment.Hands.Name);
            cmd.Parameters.AddWithValue("@legs", saveHero.Equipment.Legs.Name);
            cmd.Parameters.AddWithValue("@feet", saveHero.Equipment.Feet.Name);
            cmd.Parameters.AddWithValue("@leftRing", saveHero.Equipment.LeftRing.Name);
            cmd.Parameters.AddWithValue("@rightRing", saveHero.Equipment.RightRing.Name);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Saves the Hero's bank information.</summary>
        /// <param name="saveHero">Hero whose Bank needs to be saved</param>
        /// <param name="goldInBank">Gold in the bank</param>
        /// <param name="loanTaken">Loan taken out</param>
        public async Task<bool> SaveHeroBank(Hero saveHero, int goldInBank, int loanTaken)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "UPDATE Bank SET [Gold] = @gold, [LoanTaken] = @loanTaken WHERE [CharacterName] = @name"
            };
            cmd.Parameters.AddWithValue("@gold", goldInBank);
            cmd.Parameters.AddWithValue("@loanTaken", loanTaken);
            cmd.Parameters.AddWithValue("@name", saveHero.Name);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Saves the Hero's password to the database.</summary>
        /// <param name="saveHero">Hero whose password needs to be saved</param>
        public async Task<bool> SaveHeroPassword(Hero saveHero)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "UPDATE Players SET [CharacterPassword] = @password WHERE [CharacterName] = @name"
            };
            cmd.Parameters.AddWithValue("@password", saveHero.Password);
            cmd.Parameters.AddWithValue("@name", saveHero.Name);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        #endregion Hero Management

        #region Administrator Management

        /// <summary>Changes the administrator password in the database.</summary>
        /// <param name="newPassword">New administrator password</param>
        /// <returns>Returns true if password successfully updated</returns>
        public async Task<bool> ChangeAdminPassword(string newPassword)
        {
            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "UPDATE Admin SET [AdminPassword] = @password"
            };
            cmd.Parameters.AddWithValue("@password", newPassword);

            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Loads the Admin password from the database.</summary>
        /// <returns>Admin password</returns>
        public async Task<string> LoadAdminPassword()
        {
            string adminPassword = "";
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Admin", _con);

            if (ds.Tables[0].Rows.Count > 0)
                adminPassword = ds.Tables[0].Rows[0]["AdminPassword"].ToString();

            return adminPassword;
        }

        #endregion Administrator Management

        #region Load

        /// <summary>Loads the initial Bank state and Hero's Bank information.</summary>
        public async Task<Bank> LoadBank(Hero bankHero)
        {
            Bank heroBank = new Bank();

            SQLiteCommand cmd = new SQLiteCommand
            {
                CommandText = "SELECT * FROM Bank WHERE[CharacterName] = @name"
            };

            cmd.Parameters.AddWithValue("@name", bankHero.Name);
            DataSet ds = await SQLite.FillDataSet(cmd, _con);

            if (ds.Tables[0].Rows.Count > 0)
            {
                heroBank.GoldInBank = Int32Helper.Parse(ds.Tables[0].Rows[0]["Gold"]);
                heroBank.LoanTaken = Int32Helper.Parse(ds.Tables[0].Rows[0]["LoanTaken"]);
                heroBank.LoanAvailable = (bankHero.Level * 250) - heroBank.LoanTaken;
            }
            else
                GameState.DisplayNotification("No such user exists in the bank.", "Sulimn");

            return heroBank;
        }

        /// <summary>Loads all Classes from the database.</summary>
        /// <returns>List of Classes</returns>
        public async Task<List<HeroClass>> LoadClasses()
        {
            List<HeroClass> allClasses = new List<HeroClass>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Classes", _con);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                HeroClass newClass = new HeroClass(
                name: dr["ClassName"].ToString(),
                description: dr["ClassDescription"].ToString(),
                skillPoints: Int32Helper.Parse(dr["SkillPoints"]),
                strength: Int32Helper.Parse(dr["Strength"]),
                vitality: Int32Helper.Parse(dr["Vitality"]),
                dexterity: Int32Helper.Parse(dr["Dexterity"]),
                wisdom: Int32Helper.Parse(dr["Wisdom"]));

                allClasses.Add(newClass);
            }

            return allClasses;
        }

        #region Entities

        /// <summary>Loads all Enemies from the database.</summary>
        /// <returns>List of Enemies</returns>
        public async Task<List<Enemy>> LoadEnemies()
        {
            List<Enemy> allEnemies = new List<Enemy>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Enemies", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Weapon weapon = new Weapon();
                    if (!string.IsNullOrWhiteSpace(dr["Weapon"].ToString()))
                        weapon =
                        new Weapon(
                        GameState.AllWeapons.Find(wpn => wpn.Name == dr["Weapon"].ToString()));
                    HeadArmor head = new HeadArmor();
                    if (!string.IsNullOrWhiteSpace(dr["Head"].ToString()))
                        head =
                        new HeadArmor(
                        GameState.AllHeadArmor.Find(armr => armr.Name == dr["Head"].ToString()));
                    BodyArmor body = new BodyArmor();
                    if (!string.IsNullOrWhiteSpace(dr["Body"].ToString()))
                        body =
                        new BodyArmor(
                        GameState.AllBodyArmor.Find(armr => armr.Name == dr["Body"].ToString()));
                    HandArmor hands = new HandArmor();
                    if (!string.IsNullOrWhiteSpace(dr["Hands"].ToString()))
                        hands =
                        new HandArmor(
                        GameState.AllHandArmor.Find(armr => armr.Name == dr["Hands"].ToString()));
                    LegArmor legs = new LegArmor();
                    if (!string.IsNullOrWhiteSpace(dr["Legs"].ToString()))
                        legs =
                        new LegArmor(
                        GameState.AllLegArmor.Find(armr => armr.Name == dr["Legs"].ToString()));
                    FeetArmor feet = new FeetArmor();
                    if (!string.IsNullOrWhiteSpace(dr["Feet"].ToString()))
                        feet =
                        new FeetArmor(
                        GameState.AllFeetArmor.Find(armr => armr.Name == dr["Feet"].ToString()));
                    Ring leftRing = new Ring();
                    if (!string.IsNullOrWhiteSpace(dr["LeftRing"].ToString()))
                        leftRing =
                        new Ring(
                        GameState.AllRings.Find(ring => ring.Name == dr["LeftRing"].ToString()));
                    Ring rightRing = new Ring();
                    if (!string.IsNullOrWhiteSpace(dr["RightRing"].ToString()))
                        rightRing =
                        new Ring(
                        GameState.AllRings.Find(ring => ring.Name == dr["RightRing"].ToString()));

                    int gold = Int32Helper.Parse(dr["Gold"]);

                    Enemy newEnemy = new Enemy(
                    dr["EnemyName"].ToString(),
                    dr["EnemyType"].ToString(),
                    Int32Helper.Parse(dr["Level"]),
                    Int32Helper.Parse(dr["Experience"]),
                    new Attributes(
                    Int32Helper.Parse(dr["Strength"]),
                    Int32Helper.Parse(dr["Vitality"]),
                    Int32Helper.Parse(dr["Dexterity"]),
                    Int32Helper.Parse(dr["Wisdom"])),
                    new Statistics(
                    Int32Helper.Parse(dr["CurrentHealth"]),
                    Int32Helper.Parse(dr["MaximumHealth"]),
                    Int32Helper.Parse(dr["CurrentMagic"]),
                    Int32Helper.Parse(dr["MaximumMagic"])),
                    new Equipment(
                    weapon,
                    head,
                    body,
                    hands,
                    legs,
                    feet,
                    leftRing,
                    rightRing),
                    new Inventory(
                    new List<Item>(),
                    gold));

                    allEnemies.Add(newEnemy);
                }

            return allEnemies;
        }

        /// <summary>Loads all Heroes from the database.</summary>
        /// <returns>List of Heroes</returns>
        public async Task<List<Hero>> LoadHeroes()
        {
            List<Hero> allHeroes = new List<Hero>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Players", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string name = dr["CharacterName"].ToString();
                    SQLiteCommand cmd = new SQLiteCommand { CommandText = "SELECT * FROM Equipment WHERE [CharacterName] = @name" };
                    cmd.Parameters.AddWithValue("@name", name);
                    DataSet equipmentDS = await SQLite.FillDataSet(cmd, _con);
                    DataRow eDR = equipmentDS.Tables[0].Rows[0];
                    string leftRingText = eDR["LeftRing"].ToString();
                    string rightRingText = eDR["RightRing"].ToString();

                    Ring leftRing = new Ring();
                    Ring rightRing = new Ring();
                    if (!string.IsNullOrWhiteSpace(leftRingText))
                        leftRing = GameState.AllRings.Find(x => x.Name == leftRingText);
                    if (!string.IsNullOrWhiteSpace(rightRingText))
                        rightRing = GameState.AllRings.Find(x => x.Name == rightRingText);

                    string className = dr["Class"].ToString();
                    Hero newHero = new Hero(
                    name,
                    dr["CharacterPassword"].ToString(),
                    new HeroClass(
                    GameState.AllClasses.Find(
                    heroClass => heroClass.Name == className)),
                    Int32Helper.Parse(dr["Level"]),
                    Int32Helper.Parse(dr["Experience"]),
                    Int32Helper.Parse(dr["SkillPoints"]),
                    new Attributes(
                    Int32Helper.Parse(dr["Strength"]),
                    Int32Helper.Parse(dr["Vitality"]),
                    Int32Helper.Parse(dr["Dexterity"]),
                    Int32Helper.Parse(dr["Wisdom"])),
                    new Statistics(
                    Int32Helper.Parse(dr["CurrentHealth"]),
                    Int32Helper.Parse(dr["MaximumHealth"]),
                    Int32Helper.Parse(dr["CurrentMagic"]),
                    Int32Helper.Parse(dr["MaximumMagic"])),
                    new Equipment(
                    GameState.AllWeapons.Find(x => x.Name == eDR["Weapon"].ToString()),
                    GameState.AllHeadArmor.Find(x => x.Name == eDR["Head"].ToString()),
                    GameState.AllBodyArmor.Find(x => x.Name == eDR["Body"].ToString()),
                    GameState.AllHandArmor.Find(x => x.Name == eDR["Hands"].ToString()),
                    GameState.AllLegArmor.Find(x => x.Name == eDR["Legs"].ToString()),
                    GameState.AllFeetArmor.Find(x => x.Name == eDR["Feet"].ToString()),
                    leftRing,
                    rightRing),
                    GameState.SetSpellbook(dr["KnownSpells"].ToString()),
                    GameState.SetInventory(dr["Inventory"].ToString(),
                    Int32Helper.Parse(dr["Gold"])),
                    BoolHelper.Parse(dr["Hardcore"]));

                    allHeroes.Add(newHero);
                }

            return allHeroes;
        }

        /// <summary>Loads the Hero with maximum possible stats from the database.</summary>
        /// <returns>Hero with maximum possible stats</returns>
        public async Task<Hero> LoadMaxHeroStats()
        {
            Hero maximumStatsHero = new Hero();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM MaxHeroStats", _con);

            if (ds.Tables[0].Rows.Count > 0)
            {
                maximumStatsHero.Level = Int32Helper.Parse(ds.Tables[0].Rows[0]["Level"]);
                maximumStatsHero.Experience = Int32Helper.Parse(ds.Tables[0].Rows[0]["Experience"]);
                maximumStatsHero.SkillPoints = Int32Helper.Parse(ds.Tables[0].Rows[0]["SkillPoints"]);
                maximumStatsHero.Attributes.Strength = Int32Helper.Parse(ds.Tables[0].Rows[0]["Strength"]);
                maximumStatsHero.Attributes.Vitality = Int32Helper.Parse(ds.Tables[0].Rows[0]["Vitality"]);
                maximumStatsHero.Attributes.Dexterity = Int32Helper.Parse(ds.Tables[0].Rows[0]["Dexterity"]);
                maximumStatsHero.Attributes.Wisdom = Int32Helper.Parse(ds.Tables[0].Rows[0]["Wisdom"]);
                maximumStatsHero.Inventory.Gold = Int32Helper.Parse(ds.Tables[0].Rows[0]["Gold"]);
                maximumStatsHero.Statistics.CurrentHealth =
                Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentHealth"]);
                maximumStatsHero.Statistics.MaximumHealth =
                Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumHealth"]);
                maximumStatsHero.Statistics.CurrentMagic =
                Int32Helper.Parse(ds.Tables[0].Rows[0]["CurrentMagic"]);
                maximumStatsHero.Statistics.MaximumMagic =
                Int32Helper.Parse(ds.Tables[0].Rows[0]["MaximumMagic"]);
            }

            return maximumStatsHero;
        }

        #endregion Entities

        #region Items

        #region Equipment

        #region Armor

        /// <summary>Loads all Head Armor from the database.</summary>
        /// <returns>List of Head Armor</returns>
        public async Task<List<HeadArmor>> LoadHeadArmor()
        {
            List<HeadArmor> allHeadArmor = new List<HeadArmor>();

            DataSet ds = await SQLite.FillDataSet("SELECT * FROM HeadArmor", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HeadArmor newHeadArmor = new HeadArmor(
                    dr["ArmorName"].ToString(),
                    dr["ArmorDescription"].ToString(),
                    Int32Helper.Parse(dr["ArmorDefense"]),
                    Int32Helper.Parse(dr["ArmorWeight"]),
                    Int32Helper.Parse(dr["ArmorValue"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allHeadArmor.Add(newHeadArmor);
                }

            return allHeadArmor;
        }

        /// <summary>Loads all Body Armor from the database.</summary>
        /// <returns>List of Body Armor</returns>
        public async Task<List<BodyArmor>> LoadBodyArmor()
        {
            List<BodyArmor> allBodyArmor = new List<BodyArmor>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM BodyArmor", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BodyArmor newBodyArmor = new BodyArmor(
                    dr["ArmorName"].ToString(),
                    dr["ArmorDescription"].ToString(),
                    Int32Helper.Parse(dr["ArmorDefense"]),
                    Int32Helper.Parse(dr["ArmorWeight"]),
                    Int32Helper.Parse(dr["ArmorValue"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allBodyArmor.Add(newBodyArmor);
                }

            return allBodyArmor;
        }

        /// <summary>Loads all Hand Armor from the database.</summary>
        /// <returns>List of Hand Armor</returns>
        public async Task<List<HandArmor>> LoadHandArmor()
        {
            List<HandArmor> allHandArmor = new List<HandArmor>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM HandArmor", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HandArmor newHandArmor = new HandArmor(
                    dr["ArmorName"].ToString(),
                    dr["ArmorDescription"].ToString(),
                    Int32Helper.Parse(dr["ArmorDefense"]),
                    Int32Helper.Parse(dr["ArmorWeight"]),
                    Int32Helper.Parse(dr["ArmorValue"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allHandArmor.Add(newHandArmor);
                }

            return allHandArmor;
        }

        /// <summary>Loads all Leg Armor from the database.</summary>
        /// <returns>List of Leg Armor</returns>
        public async Task<List<LegArmor>> LoadLegArmor()
        {
            List<LegArmor> allLegArmor = new List<LegArmor>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM LegArmor", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LegArmor newLegArmor = new LegArmor(
                    dr["ArmorName"].ToString(),
                    dr["ArmorDescription"].ToString(),
                    Int32Helper.Parse(dr["ArmorDefense"]),
                    Int32Helper.Parse(dr["ArmorWeight"]),
                    Int32Helper.Parse(dr["ArmorValue"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allLegArmor.Add(newLegArmor);
                }

            return allLegArmor;
        }

        /// <summary>Loads all Feet Armor from the database.</summary>
        /// <returns>List of Feet Armor</returns>
        public async Task<List<FeetArmor>> LoadFeetArmor()
        {
            List<FeetArmor> allFeetArmor = new List<FeetArmor>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM FeetArmor", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    FeetArmor newFeetArmor = new FeetArmor(
                    dr["ArmorName"].ToString(),
                    dr["ArmorDescription"].ToString(),
                    Int32Helper.Parse(dr["ArmorDefense"]),
                    Int32Helper.Parse(dr["ArmorWeight"]),
                    Int32Helper.Parse(dr["ArmorValue"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allFeetArmor.Add(newFeetArmor);
                }

            return allFeetArmor;
        }

        #endregion Armor

        /// <summary>Loads all Rings from the database.</summary>
        /// <returns>List of Rings</returns>
        public async Task<List<Ring>> LoadRings()
        {
            List<Ring> allRings = new List<Ring>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Rings", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Ring newRing = new Ring(
                    dr["Name"].ToString(),
                    ItemTypes.Ring,
                    dr["Description"].ToString(),
                    Int32Helper.Parse(dr["Damage"]),
                    Int32Helper.Parse(dr["Defense"]),
                    Int32Helper.Parse(dr["Strength"]),
                    Int32Helper.Parse(dr["Vitality"]),
                    Int32Helper.Parse(dr["Dexterity"]),
                    Int32Helper.Parse(dr["Wisdom"]),
                    Int32Helper.Parse(dr["Weight"]),
                    Int32Helper.Parse(dr["Value"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allRings.Add(newRing);
                }

            return allRings;
        }

        /// <summary>Loads all Weapons from the database.</summary>
        /// <returns>List of Weapons</returns>
        public async Task<List<Weapon>> LoadWeapons()
        {
            List<Weapon> allWeapons = new List<Weapon>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Weapons", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Weapon newWeapon = new Weapon(
                    dr["WeaponName"].ToString(),
                    EnumHelper.Parse<WeaponTypes>(dr["WeaponType"].ToString()),
                    dr["WeaponDescription"].ToString(),
                    Int32Helper.Parse(dr["WeaponDamage"]),
                    Int32Helper.Parse(dr["WeaponWeight"]),
                    Int32Helper.Parse(dr["WeaponValue"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allWeapons.Add(newWeapon);
                }

            return allWeapons;
        }

        #endregion Equipment

        /// <summary>Loads all Potions from the database.</summary>
        /// <returns>List of Potions</returns>
        public async Task<List<Potion>> LoadPotions()
        {
            List<Potion> allPotions = new List<Potion>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Potions", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Potion newPotion = new Potion(
                    dr["PotionName"].ToString(),
                    EnumHelper.Parse<PotionTypes>(dr["PotionType"].ToString()),
                    dr["PotionDescription"].ToString(),
                    Int32Helper.Parse(dr["PotionAmount"]),
                    Int32Helper.Parse(dr["PotionValue"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allPotions.Add(newPotion);
                }

            return allPotions;
        }

        /// <summary>Loads all Food from the database.</summary>
        /// <returns>List of Food</returns>
        public async Task<List<Food>> LoadFood()
        {
            List<Food> allFood = new List<Food>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Food", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Food newFood = new Food(
                    dr["FoodName"].ToString(),
                    EnumHelper.Parse<FoodTypes>(dr["FoodType"].ToString()),
                    dr["FoodDescription"].ToString(),
                    Int32Helper.Parse(dr["FoodAmount"]),
                    Int32Helper.Parse(dr["FoodWeight"]),
                    Int32Helper.Parse(dr["FoodValue"]),
                    BoolHelper.Parse(dr["CanSell"]),
                    BoolHelper.Parse(dr["IsSold"]));

                    allFood.Add(newFood);
                }

            return allFood;
        }

        /// <summary>Loads all Spells from the database.</summary>
        /// <returns>List of Spells</returns>
        public async Task<List<Spell>> LoadSpells()
        {
            List<Spell> allSpells = new List<Spell>();
            DataSet ds = await SQLite.FillDataSet("SELECT * FROM Spells", _con);

            if (ds.Tables[0].Rows.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Spell newSpell = new Spell(
                    dr["SpellName"].ToString(),
                    EnumHelper.Parse<SpellTypes>(dr["SpellType"].ToString()),
                    dr["SpellDescription"].ToString(),
                    dr["RequiredClass"].ToString(),
                    Int32Helper.Parse(dr["RequiredLevel"]),
                    Int32Helper.Parse(dr["MagicCost"]),
                    Int32Helper.Parse(dr["SpellAmount"]));

                    allSpells.Add(newSpell);
                }

            return allSpells;
        }

        #endregion Items

        #endregion Load

        #region Theme Management

        /// <summary>Changes the current theme in the database.</summary>
        /// <param name="theme">Current theme</param>
        /// <returns>True if successful</returns>
        public async Task<bool> ChangeTheme(string theme)
        {
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "UPDATE Settings SET [Theme] = @theme" };
            cmd.Parameters.AddWithValue("@theme", theme);
            return await SQLite.ExecuteCommand(_con, cmd);
        }

        /// <summary>Loads the current theme from the database.</summary>
        /// <returns>Current theme</returns>
        public async Task<string> LoadTheme()
        {
            string theme = "Dark";
            SQLiteCommand cmd = new SQLiteCommand { CommandText = "SELECT * FROM Settings" };
            DataSet ds = await SQLite.FillDataSet(cmd, _con);
            if (ds.Tables[0].Rows.Count > 0)
                theme = ds.Tables[0].Rows[0]["Theme"].ToString();
            return theme;
        }

        #endregion Theme Management
    }
}