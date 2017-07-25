using Extensions;
using Extensions.DataTypeHelpers;
using Extensions.Enums;
using Sulimn.Classes;
using Sulimn.Classes.Card;
using Sulimn.Classes.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn.Pages.Gambling
{
    /// <summary>Interaction logic for BlackjackPage.xaml</summary>
    public partial class BlackjackPage : INotifyPropertyChanged
    {
        private readonly List<Card> _cardList = new List<Card>();

        private Hand _playerHand = new Hand(), _dealerHand = new Hand();
        private bool _handOver = true;
        private int _index, _bet, _totalWins, _totalLosses, _totalDraws, _totalBetWinnings, _totalBetLosses;
        private readonly int _sidePot = 0;

        /// <summary>Action taking place on the Dealer's turn.</summary>
        private void DealerAction()
        {
            bool keepGoing = true;

            while (keepGoing)
                if (_dealerHand.ActualValue == 21)
                    keepGoing = false;
                else if (_dealerHand.ActualValue >= 17)
                    if (_dealerHand.ActualValue > 21 && CheckHasAceEleven(_dealerHand))
                        ConvertAce(_dealerHand);
                    else
                        keepGoing = false;
                else
                    DealCard(_dealerHand);
        }

        #region Properties

        /// <summary>Total wins for the player.</summary>
        public int TotalWins
        {
            get => _totalWins;
            set
            {
                _totalWins = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Total losses for the player.</summary>
        public int TotalLosses
        {
            get => _totalLosses;
            set
            {
                _totalLosses = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Total draws.</summary>
        public int TotalDraws
        {
            get => _totalDraws;
            set
            {
                _totalDraws = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Total bet winnings for the player.</summary>
        public int TotalBetWinnings
        {
            get => _totalBetWinnings;
            set
            {
                _totalBetWinnings = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Total bet losses for the player.</summary>
        public int TotalBetLosses
        {
            get => _totalBetLosses;
            set
            {
                _totalBetLosses = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Statistics about the player's games.</summary>
        public string Statistics => $"Wins: {TotalWins:N0}\n" +
        $"Losses: {TotalLosses:N0}\n" +
        $"Draws: {TotalDraws:N0}\n" +
        $"Gold Won: {TotalBetWinnings:N0}\n" +
        $"Gold Lost: {TotalBetLosses:N0}";

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        private void BindLabels()
        {
            DataContext = this;
            LstPlayer.ItemsSource = _playerHand.CardList;
            LstPlayer.Items.Refresh();
            LstDealer.ItemsSource = _dealerHand.CardList;
            LstDealer.Items.Refresh();
            LblPlayerTotal.DataContext = _playerHand;
            LblDealerTotal.DataContext = _dealerHand;
            LblGold.DataContext = GameState.CurrentHero.Inventory;
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Check Logic

        /// <summary>Checks whether a Hand has reached Blackjack.</summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <returns>Returns true if the Hand's value is 21.</returns>
        private static bool CheckBlackjack(Hand checkHand)
        {
            return checkHand.TotalValue == 21;
        }

        /// <summary>Checks whether the current Hand has gone Bust.</summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <returns>Returns true if Hand has gone Bust.</returns>
        private bool CheckBust(Hand checkHand)
        {
            return !CheckHasAceEleven(checkHand) && checkHand.TotalValue > 21;
        }

        /// <summary>Checks whether the Player can Double Down with Window Hand.</summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <param name="checkSplit">Hand the Hand could be split into</param>
        /// <returns>Returns true if Hand can Double Down.</returns>
        private bool CheckCanHandDoubleDown(Hand checkHand, Hand checkSplit)
        {
            return checkHand.CardList.Count == 2 && checkSplit.CardList.Count == 0 &&
            (checkHand.TotalValue >= 9 && checkHand.TotalValue <= 11);
        }

        /// <summary>Checks whether the Player can split a specific Hand.</summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <param name="checkSplit">Hand it could be spilt into</param>
        /// <returns>Returns true if Hand can be Split.</returns>
        private bool CheckCanHandSplit(Hand checkHand, Hand checkSplit)
        {
            return checkHand.CardList.Count == 2 && checkSplit.CardList.Count == 0 &&
            checkHand.CardList[0].Value == checkHand.CardList[1].Value;
        }

        /// <summary>Checks whether a Hand has an Ace valued at eleven in it.</summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <returns>Returns true if Hand has an Ace valued at eleven in it.</returns>
        private static bool CheckHasAceEleven(Hand checkHand)
        {
            return checkHand.CardList.Any(card => card.Value == 11);
        }

        /// <summary>Checks whether the Dealer has an Ace face up.</summary>
        /// <returns>Returns true if the Dealer has an Ace face up.</returns>
        private bool CheckInsurance()
        {
            return !_handOver && _dealerHand.CardList[0].Value == 11 && _sidePot == 0;
        }

        #endregion Check Logic

        #region Card Management

        /// <summary>Converts the first Ace valued at eleven in the Hand to be valued at one.</summary>
        /// <param name="handConvert">Hand to be converted.</param>
        private void ConvertAce(Hand handConvert)
        {
            handConvert.ConvertAce();
            BindLabels();
        }

        /// <summary>Creates a deck of Cards.</summary>
        /// <param name="numberOfDecks">Number of standard-sized decks to add to total deck.</param>
        private void CreateDeck(int numberOfDecks)
        {
            _cardList.Clear();
            for (int h = 0; h < numberOfDecks; h++)
                for (int i = 1; i < 14; i++)
                    for (int j = 0; j < 4; j++)
                    {
                        string name;
                        CardSuit suit = CardSuit.Spades;
                        int value;

                        switch (j)
                        {
                            case 0:
                                suit = CardSuit.Spades;
                                break;

                            case 1:
                                suit = CardSuit.Hearts;
                                break;

                            case 2:
                                suit = CardSuit.Clubs;
                                break;

                            case 3:
                                suit = CardSuit.Diamonds;
                                break;
                        }

                        switch (i)
                        {
                            case 1:
                                name = "Ace";
                                value = 11;
                                break;

                            case 11:
                                name = "Jack";
                                value = 10;
                                break;

                            case 12:
                                name = "Queen";
                                value = 10;
                                break;

                            case 13:
                                name = "King";
                                value = 10;
                                break;

                            default:
                                name = i.ToString();
                                value = i;
                                break;
                        }

                        Card newCard = new Card(name, suit, value, false);
                        _cardList.Add(newCard);
                    }
        }

        /// <summary>Deals a Card to a specific Hand.</summary>
        /// <param name="handAdd">Hand where Card is to be added.</param>
        /// <param name="hidden">Should the Card be hidden?</param>
        private void DealCard(Hand handAdd, bool hidden = false)
        {
            handAdd.AddCard(new Card(_cardList[_index], hidden));
            _index++;
        }

        /// <summary>Creates a new Hand for both the Player and the Dealer.</summary>
        private void NewHand()
        {
            _playerHand = new Hand();
            _dealerHand = new Hand();

            _handOver = false;
            TxtBet.IsEnabled = false;
            ToggleNewGameExitButtons(false);
            if (_index >= _cardList.Count * 0.8)
            {
                _index = 0;
                _cardList.Shuffle();
            }

            DealCard(_playerHand);
            DealCard(_dealerHand);
            DealCard(_playerHand);
            DealCard(_dealerHand, true);
            DisplayHand();
        }

        #endregion Card Management

        #region Button Management

        /// <summary>Checks which Buttons should be enabled.</summary>
        private void CheckButtons()
        {
            ToggleHitStay(_playerHand.TotalValue < 21);
            BtnConvertAce.IsEnabled = CheckHasAceEleven(_playerHand);
        }

        /// <summary>Disables all the buttons on the Page except for BtnNewHand.</summary>
        private void DisablePlayButtons()
        {
            ToggleNewGameExitButtons(true);
            ToggleHitStay(false);
            BtnConvertAce.IsEnabled = false;
        }

        private void ToggleNewGameExitButtons(bool enabled)
        {
            BtnNewHand.IsEnabled = enabled;
            BtnExit.IsEnabled = enabled;
        }

        /// <summary>Toggles the Hit and Stay Buttons' IsEnabled state.</summary>
        /// <param name="enabled">Should the Buttons be enabled?</param>
        private void ToggleHitStay(bool enabled)
        {
            BtnHit.IsEnabled = enabled;
            BtnStay.IsEnabled = enabled;
        }

        #endregion Button Management

        #region Game Results

        /// <summary>The game ends in a draw.</summary>
        private void DrawBlackjack()
        {
            EndHand();
            Functions.AddTextToTextBox(TxtBlackjack, "You reach a draw.");
            TotalDraws++;
        }

        /// <summary>
        /// Ends the Hand.
        /// </summary>
        private void EndHand()
        {
            _handOver = true;
            TxtBet.IsEnabled = true;
            DisplayDealerHand();
            DisablePlayButtons();
            DisplayStatistics();
            BindLabels();
        }

        /// <summary>The game ends with the Player losing.</summary>
        /// <param name="betAmount">Amount the Player bet</param>
        private void LoseBlackjack(int betAmount)
        {
            Functions.AddTextToTextBox(TxtBlackjack, $"You lose {betAmount:N0}.");
            GameState.CurrentHero.Inventory.Gold -= betAmount;
            TotalLosses++;
            TotalBetLosses += betAmount;
            EndHand();
        }

        /// <summary>Player either chooses not to draw a card or has reached 21 with more than 2 cards.</summary>
        private void Stay()
        {
            _handOver = true;
            DealerAction();
            DisplayDealerHand();

            if (_playerHand.TotalValue > _dealerHand.TotalValue && !CheckBust(_dealerHand))
                WinBlackjack(_bet);
            else if (CheckBust(_dealerHand))
                WinBlackjack(_bet);
            else if (_playerHand.TotalValue == _dealerHand.TotalValue)
                DrawBlackjack();
            else if (_playerHand.TotalValue < _dealerHand.TotalValue)
                LoseBlackjack(_bet);
        }

        /// <summary>The game ends with the Player winning.</summary>
        /// <param name="betAmount">Amount the Player bet</param>
        private void WinBlackjack(int betAmount)
        {
            GameState.CurrentHero.Inventory.Gold += betAmount;
            Functions.AddTextToTextBox(TxtBlackjack, $"You win {betAmount}!");
            TotalWins++;
            TotalBetWinnings += betAmount;
            EndHand();
        }

        #endregion Game Results

        #region Display Manipulation

        /// <summary>Displays the Dealer's Hand.</summary>
        private void DisplayDealerHand()
        {
            if (_handOver)
                _dealerHand.ClearHidden();
            BindLabels();
        }

        /// <summary>Displays all Cards in both the Player and the Dealer's Hands.</summary>
        private void DisplayHand()
        {
            BindLabels();

            if (!CheckBlackjack(_playerHand) && !CheckBust(_playerHand) && !_handOver) //Player can still play
                CheckButtons();
            else if (CheckBust(_playerHand))
            {
                Functions.AddTextToTextBox(TxtBlackjack, "You bust!");
                LoseBlackjack(_bet);
            }
            else if (CheckBlackjack(_playerHand))
            {
                if (_playerHand.CardList.Count == 2)
                {
                    Functions.AddTextToTextBox(TxtBlackjack, "You have a natural blackjack!");
                    if (_dealerHand.TotalValue != 21)
                        WinBlackjack(Int32Helper.Parse(_bet * 1.5));
                    else
                    {
                        Functions.AddTextToTextBox(TxtBlackjack, "You and the dealer both have natural blackjacks.");
                        DrawBlackjack();
                    }
                }
                else
                    Stay();
            }
            else if (!_handOver)
            {
                // FUTURE
            }
        }

        /// <summary>Displays the Player's Hand.</summary>
        private void DisplayPlayerHand()
        {
            BindLabels();
        }

        /// <summary>Displays the game's statistics.</summary>
        private void DisplayStatistics()
        {
            LblStatistics.Text = Statistics;
            LblGold.Text = $"Gold: {GameState.CurrentHero.Inventory.GoldToString}";
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void BtnConvertAce_Click(object sender, RoutedEventArgs e)
        {
            ConvertAce(_playerHand);
            DisplayHand();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            DealCard(_playerHand);
            _index++;
            if (_playerHand.CardList.Count < 5)
                DisplayHand();
            else
            {
                if (_playerHand.TotalValue < 21 || CheckHasAceEleven(_playerHand) && _playerHand.TotalValue <= 31)
                {
                    Functions.AddTextToTextBox(TxtBlackjack, "Five Card Charlie!");
                    DisplayPlayerHand();
                    WinBlackjack(_bet);
                }
                else
                    DisplayHand();
            }
        }

        private void BtnNewHand_Click(object sender, RoutedEventArgs e)
        {
            _bet = Int32Helper.Parse(TxtBet.Text);
            if (_bet > 0 && _bet <= GameState.CurrentHero.Inventory.Gold)
                NewHand();
            else if (_bet > GameState.CurrentHero.Inventory.Gold)
                GameState.DisplayNotification("You can't bet more gold than you have!", "Sulimn");
            else
                GameState.DisplayNotification("Please enter a valid bet.", "Sulimn");
        }

        private void BtnStay_Click(object sender, RoutedEventArgs e)
        {
            Stay();
        }

        #endregion Button-Click Methods

        #region Page-Manipulation Methods

        /// <summary>Closes the Page.</summary>
        private async void ClosePage()
        {
            if (_handOver)
            {
                await GameState.SaveHero(GameState.CurrentHero);
                GameState.GoBack();
            }
        }

        public BlackjackPage()
        {
            InitializeComponent();
            CreateDeck(6);
            _cardList.Shuffle();
            DisplayStatistics();
            TxtBlackjack.Text = "You approach a table where Blackjack is being played. You take a seat.\n\n" +
            "\"Care to place a bet?\" asks the dealer.";
            TxtBet.Focus();
            BindLabels();
        }

        private void TxtBet_GotFocus(object sender, RoutedEventArgs e)
        {
            Functions.TextBoxGotFocus(sender);
        }

        private void TxtBet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Functions.PreviewKeyDown(e, KeyType.Integers);
        }

        private void TxtBet_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Integers);
            BtnNewHand.IsEnabled = TxtBet.Text.Length > 0;
        }

        #endregion Page-Manipulation Methods

        private void BlackjackPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            GameState.CalculateScale(Grid);
        }
    }
}