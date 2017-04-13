using Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Sulimn
{
    internal class SQLiteDatabaseInteraction : IDatabaseInteraction
    {
        private readonly SQLiteConnection _con = new SQLiteConnection { ConnectionString = $"Data Source = {_DATABASENAME};Version=3" };

        // ReSharper disable once InconsistentNaming
        private const string _DATABASENAME = "Sulimn.sqlite";

        /// <summary>Verifies that the requested database exists and that its file size is greater than zero. If not, it extracts the embedded database file to the local output folder.</summary>
        /// <returns>Returns true once the database has been validated</returns>
        public bool VerifyDatabaseIntegrity()
        {
            if (!File.Exists(_DATABASENAME) || new FileInfo(_DATABASENAME).Length == 0)
                Functions.ExtractEmbeddedResource("Sulimn", Directory.GetCurrentDirectory(), "", _DATABASENAME);
            return true;
        }

        #region Hero Management

        /// <summary>Creates a new Hero and adds it to the database.</summary>
        /// <param name="newHero">New Hero</param>
        /// <returns>Returns true if successfully created</returns>
        public async Task<bool> NewHero(Hero newHero)
        {
            bool success = false;
            SQLiteCommand cmd = _con.CreateCommand();
            cmd.CommandText =
            $"INSERT INTO Players([CharacterName],[CharacterPassword],[Class],[Level],[Experience],[SkillPoints],[Strength],[Vitality],[Dexterity],[Wisdom],[Gold],[CurrentHealth],[MaximumHealth],[CurrentMagic],[MaximumMagic],[KnownSpells],[Inventory])VALUES('{newHero.Name}','{newHero.Password}','{newHero.Class.Name}','{newHero.Level}','{newHero.Experience}','{newHero.SkillPoints}','{newHero.Attributes.Strength}','{newHero.Attributes.Vitality}','{newHero.Attributes.Dexterity}','{newHero.Attributes.Wisdom}','{newHero.Inventory.Gold}','{newHero.Statistics.CurrentHealth}','{newHero.Statistics.MaximumHealth}','{newHero.Statistics.CurrentMagic}','{newHero.Statistics.MaximumMagic}','{newHero.Spellbook}','{newHero.Inventory}')";

            await Task.Run(() =>
            {
                try
                {
                    _con.Open();
                    cmd.Connection = _con;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"INSERT INTO Bank([CharacterName],[Gold],[LoanTaken])Values('{newHero.Name}',0,0)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText =
        $"INSERT INTO Equipment([CharacterName],[Weapon],[Head],[Body],[Hands],[Legs],[Feet])Values('{newHero.Name}','{newHero.Equipment.Weapon.Name}','{newHero.Equipment.Head.Name}','{newHero.Equipment.Body.Name}','{newHero.Equipment.Hands.Name}','{newHero.Equipment.Legs.Name}','{ newHero.Equipment.Feet.Name}')";
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Creating New Hero", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return success;
        }

        /// <summary>Saves Hero to database.</summary>
        /// <param name="saveHero">Hero to be saved</param>
        /// <returns>Returns true if successfully saved</returns>
        public async Task<bool> SaveHero(Hero saveHero)
        {
            bool success = false;
            SQLiteCommand cmd = new SQLiteCommand()
            {
                CommandText =
            "UPDATE Players SET [Level] = @level, [Experience] = @experience, [SkillPoints] = @skillPoints, [Strength] = @strength, [Vitality] = @vitality, [Dexterity] = @dexterity, [Wisdom] = @wisdom, [Gold] = @gold, [CurrentHealth] = @currentHealth, [MaximumHealth] = @maximumHealth, [CurrentMagic] = @currentMagic, [MaximumMagic] = @maximumMagic, [KnownSpells] = @spells, [Inventory] = @inventory WHERE [CharacterName] = @name"
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
            cmd.Parameters.AddWithValue("@name", saveHero.Name);

            await Task.Run(() =>
            {
                try
                {
                    cmd.Connection = _con;
                    _con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    cmd.CommandText =
        "UPDATE Equipment SET [Weapon] = @weapon, [Head] = @head, [Body] = @body, [Hands] = hands, [Legs] = @legs, [Feet] = @feet, [LeftRing] = @leftRing, [RightRing] = @rightRing WHERE [CharacterName] = @name";
                    cmd.Parameters.AddWithValue("@weapon", saveHero.Equipment.Weapon.Name);
                    cmd.Parameters.AddWithValue("@head", saveHero.Equipment.Head.Name);
                    cmd.Parameters.AddWithValue("@body", saveHero.Equipment.Body.Name);
                    cmd.Parameters.AddWithValue("@hands", saveHero.Equipment.Hands.Name);
                    cmd.Parameters.AddWithValue("@legs", saveHero.Equipment.Legs.Name);
                    cmd.Parameters.AddWithValue("@feet", saveHero.Equipment.Feet.Name);
                    cmd.Parameters.AddWithValue("@leftRing", saveHero.Equipment.LeftRing.Name);
                    cmd.Parameters.AddWithValue("@rightRing", saveHero.Equipment.RightRing.Name);
                    cmd.Parameters.AddWithValue("@name", saveHero.Name);
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Saving Hero", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return success;
        }

        /// <summary>Saves the Hero's bank information.</summary>
        /// <param name="saveHero">Hero whose Bank needs to be saved</param>
        /// <param name="goldInBank">Gold in the bank</param>
        /// <param name="loanTaken">Loan taken out</param>
        public async Task<bool> SaveHeroBank(Hero saveHero, int goldInBank, int loanTaken)

        {
            bool success = false;
            SQLiteCommand cmd = new SQLiteCommand()
            {
                CommandText = "UPDATE Bank SET [Gold] = @gold, [LoanTaken] = @loanTaken WHERE [CharacterName] = @name"
            };
            cmd.Parameters.AddWithValue("@gold", goldInBank);
            cmd.Parameters.AddWithValue("@loanTaken", loanTaken);
            cmd.Parameters.AddWithValue("@name", saveHero.Name);

            await Task.Run(() =>
            {
                try
                {
                    cmd.Connection = _con;
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Saving Hero Bank", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return success;
        }

        /// <summary>Saves the Hero's password to the database.</summary>
        /// <param name="saveHero">Hero whose password needs to be saved</param>
        public async Task<bool> SaveHeroPassword(Hero saveHero)
        {
            bool success = false;
            SQLiteCommand cmd = new SQLiteCommand()
            {
                CommandText = "UPDATE Players SET [CharacterPassword] = @password WHERE [CharacterName] = @name"
            };
            cmd.Parameters.AddWithValue("@password", saveHero.Password);
            cmd.Parameters.AddWithValue("@name", saveHero.Name);

            await Task.Run(() =>
            {
                try
                {
                    cmd.Connection = _con;
                    _con.Open();
                    cmd.ExecuteNonQuery();
                    success = true;
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Saving Hero Password", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return success;
        }

        #endregion Hero Management

        #region Load

        /// <summary>Loads the Admin password from the database.</summary>
        /// <returns>Admin password</returns>
        public async Task<string> LoadAdminPassword()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            string adminPassword = "";
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Admin", _con);
                    da.Fill(ds, "Admin");

                    if (ds.Tables[0].Rows.Count > 0)
                        adminPassword = ds.Tables[0].Rows[0]["AdminPassword"].ToString();
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Admin Password", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return adminPassword;
        }

        /// <summary>Loads the initial Bank state and Hero's Bank information..</summary>
        internal async Task<Bank> LoadBank(Hero bankHero)
        {
            Bank heroBank = new Bank();

            SQLiteDataAdapter da;
            DataSet ds;
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Bank", _con);
                    da.Fill(ds, "Bank");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        heroBank.GoldInBank = Int32Helper.Parse(ds.Tables[0].Rows[0]["Gold"]);
                        heroBank.LoanTaken = Int32Helper.Parse(ds.Tables[0].Rows[0]["LoanTaken"]);
                        heroBank.LoanAvailable = bankHero.Level * 250 - heroBank.LoanTaken;
                    }
                    else
                        GameState.DisplayNotification("No such user exists in the bank.", "Sulimn", NotificationButtons.OK);
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Hero Bank", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return heroBank;
        }

        /// <summary>Loads all Classes from the database.</summary>
        /// <returns>List of Classes</returns>
        public async Task<List<HeroClass>> LoadClasses()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<HeroClass> allClasses = new List<HeroClass>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Classes", _con);
                    da.Fill(ds, "Classes");

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        HeroClass newClass = new HeroClass(
            name: ds.Tables[0].Rows[i]["ClassName"].ToString(),
            description: ds.Tables[0].Rows[i]["ClassDescription"].ToString(),
            skillPoints: Int32Helper.Parse(ds.Tables[0].Rows[i]["SkillPoints"]),
            strength: Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]),
            vitality: Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]),
            dexterity: Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]),
            wisdom: Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"]));

                        allClasses.Add(newClass);
                    }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Hero Classes", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });

            return allClasses;
        }

        #region Entities

        /// <summary>Loads all Enemies from the database.</summary>
        /// <returns>List of Enemies</returns>
        public async Task<List<Enemy>> LoadEnemies()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<Enemy> allEnemies = new List<Enemy>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Enemies", _con);
                    da.Fill(ds, "Enemies");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            Weapon weapon = new Weapon();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Weapon"].ToString()))
                                weapon =
                    new Weapon(
                    GameState.AllWeapons.Find(wpn => wpn.Name == dr["Weapon"].ToString()));
                            HeadArmor head = new HeadArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Head"].ToString()))
                                head =
                    new HeadArmor(
                    GameState.AllHeadArmor.Find(armr => armr.Name == dr["Head"].ToString()));
                            BodyArmor body = new BodyArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Body"].ToString()))
                                body =
                    new BodyArmor(
                    GameState.AllBodyArmor.Find(armr => armr.Name == dr["Body"].ToString()));
                            HandArmor hands = new HandArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Hands"].ToString()))
                                hands =
                    new HandArmor(
                    GameState.AllHandArmor.Find(armr => armr.Name == dr["Hands"].ToString()));
                            LegArmor legs = new LegArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Legs"].ToString()))
                                legs =
                    new LegArmor(
                    GameState.AllLegArmor.Find(armr => armr.Name == dr["Legs"].ToString()));
                            FeetArmor feet = new FeetArmor();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["Feet"].ToString()))
                                feet =
                    new FeetArmor(
                    GameState.AllFeetArmor.Find(armr => armr.Name == dr["Feet"].ToString()));
                            Ring leftRing = new Ring();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["LeftRing"].ToString()))
                                leftRing =
                    new Ring(
                    GameState.AllRings.Find(ring => ring.Name == dr["LeftRing"].ToString()));
                            Ring rightRing = new Ring();
                            if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["RightRing"].ToString()))
                                rightRing =
                    new Ring(
                    GameState.AllRings.Find(ring => ring.Name == dr["RightRing"].ToString()));

                            int gold = Int32Helper.Parse(ds.Tables[0].Rows[i]["Gold"]);

                            Enemy newEnemy = new Enemy(
                ds.Tables[0].Rows[i]["EnemyName"].ToString(),
                ds.Tables[0].Rows[i]["EnemyType"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Level"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Experience"]),
                new Attributes(
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"])),
                new Statistics(
                Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentHealth"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumHealth"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentMagic"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumMagic"])),
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
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Enemies", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allEnemies;
        }

        /// <summary>Loads all Heroes from the database.</summary>
        /// <returns>List of Heroes</returns>
        public async Task<List<Hero>> LoadHeroes()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<Hero> allHeroes = new List<Hero>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Players", _con);
                    da.Fill(ds, "Players");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string name = ds.Tables[0].Rows[i]["CharacterName"].ToString();
                            DataSet equipmentDS = new DataSet();
                            SQLiteDataAdapter equipmentDA =
                new SQLiteDataAdapter($"SELECT * FROM Equipment WHERE [CharacterName]='{name}'",
                _con);
                            equipmentDA.Fill(equipmentDS, "Equipment");

                            string leftRingText = equipmentDS.Tables[0].Rows[0]["LeftRing"].ToString();
                            string rightRingText = equipmentDS.Tables[0].Rows[0]["RightRing"].ToString();

                            Ring leftRing = new Ring();
                            Ring rightRing = new Ring();
                            if (!string.IsNullOrWhiteSpace(leftRingText))
                                leftRing = GameState.AllRings.Find(x => x.Name == leftRingText);
                            if (!string.IsNullOrWhiteSpace(rightRingText))
                                rightRing = GameState.AllRings.Find(x => x.Name == rightRingText);

                            string className = ds.Tables[0].Rows[i]["Class"].ToString();
                            Hero newHero = new Hero(
                name,
                ds.Tables[0].Rows[i]["CharacterPassword"].ToString(),
                new HeroClass(
                GameState.AllClasses.Find(
                heroClass => heroClass.Name == className)),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Level"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Experience"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["SkillPoints"]),
                new Attributes(
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"])),
                new Statistics(
                Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentHealth"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumHealth"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["CurrentMagic"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MaximumMagic"])),
                new Equipment(
                GameState.AllWeapons.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Weapon"].ToString()),
                GameState.AllHeadArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Head"].ToString()),
                GameState.AllBodyArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Body"].ToString()),
                GameState.AllHandArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Hands"].ToString()),
                GameState.AllLegArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Legs"].ToString()),
                GameState.AllFeetArmor.Find(x => x.Name == equipmentDS.Tables[0].Rows[0]["Feet"].ToString()),
                leftRing,
                rightRing),
                GameState.SetSpellbook(ds.Tables[0].Rows[i]["KnownSpells"].ToString()),
                GameState.SetInventory(ds.Tables[0].Rows[i]["Inventory"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Gold"])));

                            allHeroes.Add(newHero);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading All Heroes", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allHeroes;
        }

        /// <summary>Loads the Hero with maximum possible stats from the database.</summary>
        /// <returns>Hero with maximum possible stats</returns>
        public async Task<Hero> LoadMaxHeroStats()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            Hero maximumStatsHero = new Hero();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM MaxHeroStats", _con);
                    da.Fill(ds, "MaxHeroStats");

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
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Maximum Statistics Hero", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
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
            SQLiteDataAdapter da;
            DataSet ds;
            List<HeadArmor> allHeadArmor = new List<HeadArmor>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM HeadArmor", _con);
                    da.Fill(ds, "HeadArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            HeadArmor newHeadArmor = new HeadArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allHeadArmor.Add(newHeadArmor);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Head Armor", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allHeadArmor;
        }

        /// <summary>Loads all Body Armor from the database.</summary>
        /// <returns>List of Body Armor</returns>
        public async Task<List<BodyArmor>> LoadBodyArmor()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<BodyArmor> allBodyArmor = new List<BodyArmor>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM BodyArmor", _con);
                    da.Fill(ds, "BodyArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            BodyArmor newBodyArmor = new BodyArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allBodyArmor.Add(newBodyArmor);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Body Armor", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allBodyArmor;
        }

        /// <summary>Loads all Hand Armor from the database.</summary>
        /// <returns>List of Hand Armor</returns>
        public async Task<List<HandArmor>> LoadHandArmor()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<HandArmor> allHandArmor = new List<HandArmor>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM HandArmor", _con);
                    da.Fill(ds, "HandArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            HandArmor newHandArmor = new HandArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allHandArmor.Add(newHandArmor);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Hand Armor", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allHandArmor;
        }

        /// <summary>Loads all Leg Armor from the database.</summary>
        /// <returns>List of Leg Armor</returns>
        public async Task<List<LegArmor>> LoadLegArmor()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<LegArmor> allLegArmor = new List<LegArmor>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM LegArmor", _con);
                    da.Fill(ds, "LegArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            LegArmor newLegArmor = new LegArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allLegArmor.Add(newLegArmor);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Leg Armor", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allLegArmor;
        }

        /// <summary>Loads all Feet Armor from the database.</summary>
        /// <returns>List of Feet Armor</returns>
        public async Task<List<FeetArmor>> LoadFeetArmor()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<FeetArmor> allFeetArmor = new List<FeetArmor>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM FeetArmor", _con);
                    da.Fill(ds, "FeetArmor");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            FeetArmor newFeetArmor = new FeetArmor(
                ds.Tables[0].Rows[i]["ArmorName"].ToString(),
                ds.Tables[0].Rows[i]["ArmorDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorDefense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["ArmorValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allFeetArmor.Add(newFeetArmor);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Feet Armor", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allFeetArmor;
        }

        #endregion Armor

        /// <summary>Loads all Rings from the database.</summary>
        /// <returns>List of Rings</returns>
        public async Task<List<Ring>> LoadRings()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<Ring> allRings = new List<Ring>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Rings", _con);
                    da.Fill(ds, "Rings");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Ring newRing = new Ring(
                ds.Tables[0].Rows[i]["Name"].ToString(),
                ItemTypes.Ring,
                ds.Tables[0].Rows[i]["Description"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Damage"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Defense"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Strength"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Vitality"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Dexterity"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Wisdom"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Weight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["Value"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allRings.Add(newRing);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Rings", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allRings;
        }

        /// <summary>Loads all Weapons from the database.</summary>
        /// <returns>List of Weapons</returns>
        public async Task<List<Weapon>> LoadWeapons()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<Weapon> allWeapons = new List<Weapon>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Weapons", _con);
                    da.Fill(ds, "Weapons");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Enum.TryParse(ds.Tables[0].Rows[i]["WeaponType"].ToString(), out WeaponTypes currentWeaponType);
                            Weapon newWeapon = new Weapon(
                ds.Tables[0].Rows[i]["WeaponName"].ToString(),
                currentWeaponType,
                ds.Tables[0].Rows[i]["WeaponDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponDamage"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["WeaponValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allWeapons.Add(newWeapon);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Feet Armor", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allWeapons;
        }

        #endregion Equipment

        /// <summary>Loads all Potions from the database.</summary>
        /// <returns>List of Potions</returns>
        public async Task<List<Potion>> LoadPotions()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<Potion> allPotions = new List<Potion>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Potions", _con);
                    da.Fill(ds, "Potions");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Enum.TryParse(ds.Tables[0].Rows[i]["PotionType"].ToString(), out PotionTypes currentPotionType);
                            Potion newPotion = new Potion(
                ds.Tables[0].Rows[i]["PotionName"].ToString(),
                currentPotionType,
                ds.Tables[0].Rows[i]["PotionDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionAmount"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["PotionValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allPotions.Add(newPotion);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Potions", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allPotions;
        }

        /// <summary>Loads all Food from the database.</summary>
        /// <returns>List of Food</returns>
        public async Task<List<Food>> LoadFood()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<Food> allFood = new List<Food>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Food", _con);
                    da.Fill(ds, "Food");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Enum.TryParse(ds.Tables[0].Rows[i]["FoodType"].ToString(), out FoodTypes currentFoodType);
                            Food newFood = new Food(
                ds.Tables[0].Rows[i]["FoodName"].ToString(),
                currentFoodType,
                ds.Tables[0].Rows[i]["FoodDescription"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodAmount"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodWeight"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["FoodValue"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["CanSell"]),
                BoolHelper.Parse(ds.Tables[0].Rows[i]["IsSold"]));

                            allFood.Add(newFood);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Maximum Statistics Hero", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allFood;
        }

        /// <summary>Loads all Spells from the database.</summary>
        /// <returns>List of Spells</returns>
        public async Task<List<Spell>> LoadSpells()
        {
            SQLiteDataAdapter da;
            DataSet ds;
            List<Spell> allSpells = new List<Spell>();
            await Task.Run(() =>
            {
                try
                {
                    ds = new DataSet();
                    da = new SQLiteDataAdapter("SELECT * FROM Spells", _con);
                    da.Fill(ds, "Spells");

                    if (ds.Tables[0].Rows.Count > 0)
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Enum.TryParse(ds.Tables[0].Rows[i]["SpellType"].ToString(), out SpellTypes currentSpellType);
                            Spell newSpell = new Spell(
                ds.Tables[0].Rows[i]["SpellName"].ToString(),
                currentSpellType,
                ds.Tables[0].Rows[i]["SpellDescription"].ToString(),
                ds.Tables[0].Rows[i]["RequiredClass"].ToString(),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["RequiredLevel"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["MagicCost"]),
                Int32Helper.Parse(ds.Tables[0].Rows[i]["SpellAmount"]));

                            allSpells.Add(newSpell);
                        }
                }
                catch (Exception ex)
                {
                    GameState.DisplayNotification(ex.Message, "Error Loading Spells", NotificationButtons.OK);
                }
                finally
                {
                    _con.Close();
                }
            });
            return allSpells;
        }

        #endregion Items

        #endregion Load
    }
}