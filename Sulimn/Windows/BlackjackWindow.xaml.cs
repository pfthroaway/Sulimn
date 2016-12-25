using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn
{
    /// <summary>Interaction logic for BlackjackWindow.xaml</summary>
    public partial class BlackjackWindow : INotifyPropertyChanged
    {
        private readonly List<Card> _cardList = new List<Card>();
        private readonly string _nl = Environment.NewLine;
        private Hand _playerHand, _dealerHand;
        private bool _handOver = true;
        private int _index, _bet, _totalWins, _totalLosses, _totalDraws, _totalBetWinnings, _totalBetLosses;
        private readonly int _sidePot = 0;
        internal TavernWindow RefToTavernWindow { private get; set; }

        /// <summary>Action taking place on the Dealer's turn.</summary>
        private void DealerAction()
        {
            bool keepGoing = true;

            while (keepGoing)
                if (_dealerHand.TotalValue() == 21)
                    keepGoing = false;
                else if (_dealerHand.TotalValue() >= 17)
                    if (_dealerHand.TotalValue() > 21 && CheckHasAceEleven(_dealerHand))
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
            get { return _totalWins; }
            set
            {
                _totalWins = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Total losses for the player.</summary>
        public int TotalLosses
        {
            get { return _totalLosses; }
            set
            {
                _totalLosses = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Total draws.</summary>
        public int TotalDraws
        {
            get { return _totalDraws; }
            set
            {
                _totalDraws = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Total bet winnings for the player.</summary>
        public int TotalBetWinnings
        {
            get { return _totalBetWinnings; }
            set
            {
                _totalBetWinnings = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Total bet losses for the player.</summary>
        public int TotalBetLosses
        {
            get { return _totalBetLosses; }
            set
            {
                _totalBetLosses = value;
                OnPropertyChanged("Statistics");
            }
        }

        /// <summary>Statistics about the player's games.</summary>
        public string Statistics => "Wins: " + TotalWins.ToString("N0") + _nl + "Losses: " + TotalLosses.ToString("N0") + _nl +
                                    "Draws: " + TotalDraws.ToString("N0") + _nl + "Gold Won: " + TotalBetWinnings.ToString("N0") + _nl +
                                    "Gold Lost: " + TotalBetLosses.ToString("N0");

        #endregion Properties

        #region Data-Binding

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        //private void BindLabels()
        //{
        //lstPlayer.DisplayMember = "CardToString";
        //lstPlayer.DataSource = playerHand.CardList;
        ////lstDealer.DataSource = dealerHand.CardList;
        //lblPlayerTotal.DataBindings.Add("Text", playerHand, "Value");
        //lblDealerTotal.DataBindings.Add("Text", dealerHand, "Value");
        //lblGold.DataBindings.Add("Text", GameState.currentHero, "GoldToString");
        //lblStatistics.DataBindings.Add("Text", this, "Statistics");
        //}
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
            return checkHand.TotalValue() == 21;
        }

        /// <summary>Checks whether the current Hand has gone Bust.</summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <returns>Returns true if Hand has gone Bust.</returns>
        private bool CheckBust(Hand checkHand)
        {
            return !CheckHasAceEleven(checkHand) && checkHand.TotalValue() > 21;
        }

        /// <summary>Checks whether the Player can Double Down with this Hand.</summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <param name="checkSplit">Hand the Hand could be split into</param>
        /// <returns>Returns true if Hand can Double Down.</returns>
        private bool CheckCanHandDoubleDown(Hand checkHand, Hand checkSplit)
        {
            return checkHand.CardList.Count == 2 && checkSplit.CardList.Count == 0 &&
                   (checkHand.TotalValue() >= 9 && checkHand.TotalValue() <= 11);
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
        private static void ConvertAce(Hand handConvert)
        {
            foreach (Card card in handConvert.CardList)
                if (card.Value == 11)
                {
                    card.Value = 1;
                    break;
                }
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

                        Card newCard = new Card(name, suit, value);
                        _cardList.Add(newCard);
                    }
        }

        /// <summary>Deals a Card to a specific Hand.</summary>
        /// <param name="handAdd">Hand where Card is to be added.</param>
        private void DealCard(Hand handAdd)
        {
            handAdd.CardList.Add(_cardList[_index]);
            _index++;
        }

        /// <summary>Creates a new Hand for both the Player and the Dealer.</summary>
        private void NewHand()
        {
            _playerHand = new Hand();
            _dealerHand = new Hand();

            _handOver = false;
            txtBet.IsEnabled = false;
            btnNewHand.IsEnabled = false;
            btnExit.IsEnabled = false;
            if (_index >= _cardList.Count * 0.8)
            {
                _index = 0;
                _cardList.Shuffle();
            }

            for (int i = 0; i < 2; i++)
            {
                DealCard(_playerHand);
                DealCard(_dealerHand);
            }
            DisplayHand();
        }

        #endregion Card Management

        #region Button Management

        /// <summary>Checks which Buttons should be enabled.</summary>
        private void CheckButtons()
        {
            ToggleHitStay(_playerHand.TotalValue() < 21);
            btnConvertAce.IsEnabled = CheckHasAceEleven(_playerHand);
        }

        /// <summary>Disables all the buttons on the Window except for btnNewHand.</summary>
        private void DisablePlayButtons()
        {
            btnNewHand.IsEnabled = true;
            btnExit.IsEnabled = true;
            btnHit.IsEnabled = false;
            btnStay.IsEnabled = false;
            btnConvertAce.IsEnabled = false;
        }

        /// <summary>Toggles the Hit and Stay Buttons' IsEnabled state.</summary>
        /// <param name="enabled">Should the Buttons be enabled?</param>
        private void ToggleHitStay(bool enabled)
        {
            btnHit.IsEnabled = enabled;
            btnStay.IsEnabled = enabled;
        }

        #endregion Button Management

        #region Game Results

        /// <summary>The game ends in a draw.</summary>
        private void DrawBlackjack()
        {
            EndHand();
            AddTextTT("You reach a draw.");
            TotalDraws += 1;
        }

        /// <summary>
        /// Ends the Hand.
        /// </summary>
        private void EndHand()
        {
            _handOver = true;
            txtBet.IsEnabled = true;
            DisplayDealerHand();
            DisablePlayButtons();
            DisplayStatistics();
        }

        /// <summary>The game ends with the Player losing.</summary>
        /// <param name="betAmount">Amount the Player bet</param>
        private void LoseBlackjack(int betAmount)
        {
            AddTextTT("You lose " + betAmount + ".");
            GameState.CurrentHero.Inventory.Gold -= betAmount;
            TotalLosses += 1;
            TotalBetLosses += betAmount;
            EndHand();
        }

        /// <summary>Player either chooses not to draw a card or has reached 21 with more than 2 cards.</summary>
        private void Stay()
        {
            _handOver = true;
            DealerAction();
            DisplayDealerHand();

            if (_playerHand.TotalValue() > _dealerHand.TotalValue() && !CheckBust(_dealerHand))
                WinBlackjack(_bet);
            else if (CheckBust(_dealerHand))
                WinBlackjack(_bet);
            else if (_playerHand.TotalValue() == _dealerHand.TotalValue())
                DrawBlackjack();
            else if (_playerHand.TotalValue() < _dealerHand.TotalValue())
                LoseBlackjack(_bet);
        }

        /// <summary>The game ends with the Player winning.</summary>
        /// <param name="betAmount">Amount the Player bet</param>
        private void WinBlackjack(int betAmount)
        {
            GameState.CurrentHero.Inventory.Gold += betAmount;
            AddTextTT("You win " + betAmount + "!");
            TotalWins += 1;
            TotalBetWinnings += betAmount;
            EndHand();
        }

        #endregion Game Results

        #region Display Manipulation

        /// <summary>Adds text to the txtBlackjack TextBox.</summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtBlackjack.Text += _nl + _nl + newText;
            txtBlackjack.Focus();
            txtBlackjack.CaretIndex = txtBlackjack.Text.Length;
            txtBlackjack.ScrollToEnd();
        }

        /// <summary>Displays the Dealer's Hand.</summary>
        private void DisplayDealerHand()
        {
            lstDealer.Items.Clear();

            if (!_handOver)
            {
                lstDealer.Items.Add(_dealerHand.CardList[0].Name + " of " + _dealerHand.CardList[0].Suit);
                lstDealer.Items.Add("?? of ??");
                lblDealerTotal.Text = _dealerHand.CardList[0].Value.ToString();
            }
            else
            {
                foreach (Card card in _dealerHand.CardList)
                    lstDealer.Items.Add(card.Name + " of " + card.Suit);

                lblDealerTotal.Text = _dealerHand.TotalValue().ToString();
            }
        }

        /// <summary>Displays all Cards in both the Player and the Dealer's Hands.</summary>
        private void DisplayHand()
        {
            DisplayDealerHand();
            DisplayPlayerHand();

            if (!CheckBlackjack(_playerHand) && !CheckBust(_playerHand) && !_handOver) //Player can still play
                CheckButtons();
            else if (CheckBust(_playerHand))
            {
                AddTextTT("You bust!");
                LoseBlackjack(_bet);
            }
            else if (CheckBlackjack(_playerHand))
            {
                if (_playerHand.CardList.Count == 2)
                {
                    AddTextTT("You have a natural blackjack!");
                    if (_dealerHand.TotalValue() != 21)
                        WinBlackjack(Int32Helper.Parse(_bet * 1.5));
                    else
                    {
                        AddTextTT("You and the dealer both have natural blackjacks.");
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
            lstPlayer.Items.Clear();

            foreach (Card card in _playerHand.CardList)
                lstPlayer.Items.Add(card.Name + " of " + card.Suit);
            lblPlayerTotal.Text = _playerHand.TotalValue().ToString();
        }

        /// <summary>Displays the game's statistics.</summary>
        private void DisplayStatistics()
        {
            lblStatistics.Text = Statistics;
            lblGold.Text = "Gold: " + GameState.CurrentHero.Inventory.Gold.ToString("N0");
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void btnConvertAce_Click(object sender, RoutedEventArgs e)
        {
            ConvertAce(_playerHand);
            DisplayHand();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            _playerHand.CardList.Add(new Card(_cardList[_index]));
            _index++;
            if (_playerHand.CardList.Count < 5)
                DisplayHand();
            else
            {
                if (_playerHand.TotalValue() < 21 || CheckHasAceEleven(_playerHand) && _playerHand.TotalValue() <= 31)
                {
                    AddTextTT("Five Card Charlie!");
                    DisplayPlayerHand();
                    WinBlackjack(_bet);
                }
                else
                    DisplayHand();
            }
        }

        private void btnNewHand_Click(object sender, RoutedEventArgs e)
        {
            _bet = Int32Helper.Parse(txtBet.Text);
            if (_bet > 0 && _bet <= GameState.CurrentHero.Inventory.Gold)
                NewHand();
            else if (_bet > GameState.CurrentHero.Inventory.Gold)
                new Notification("You can't bet more gold than you have!", "Sulimn", NotificationButtons.OK, this).ShowDialog();
            else
                new Notification("Please enter a valid bet.", "Sulimn", NotificationButtons.OK, this).ShowDialog();
        }

        private void btnStay_Click(object sender, RoutedEventArgs e)
        {
            Stay();
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>Closes the Window.</summary>
        private void CloseWindow()
        {
            if (_handOver)
                Close();
        }

        public BlackjackWindow()
        {
            InitializeComponent();
            CreateDeck(6);
            _cardList.Shuffle();
            DisplayStatistics();
            txtBlackjack.Text = "You approach a table where Blackjack is being played. You take a seat." + _nl + _nl +
            "\"Care to place a bet?\" asks the dealer.";
            txtBet.Focus();
        }

        private void txtBet_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBet.SelectAll();
        }

        private void txtBet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift,
            Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl,
            Key.Escape);

            if (keys.Any(key => key) || Key.D0 <= k && k <= Key.D9 || Key.NumPad0 <= k && k <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtBet_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBet.Text = new string((from c in txtBet.Text
                                      where char.IsDigit(c)
                                      select c).ToArray());
            txtBet.CaretIndex = txtBet.Text.Length;

            btnNewHand.IsEnabled = txtBet.Text.Length > 0;
        }

        private async void windowBlackjack_Closing(object sender, CancelEventArgs e)
        {
            if (_handOver)
            {
                RefToTavernWindow.Show();
                await GameState.SaveHero(GameState.CurrentHero);
            }
            else
                e.Cancel = true;
        }

        #endregion Window-Manipulation Methods
    }
}