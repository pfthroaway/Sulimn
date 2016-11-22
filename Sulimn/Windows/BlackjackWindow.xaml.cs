using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sulimn
{
    /// <summary>
    /// Interaction logic for BlackjackWindow.xaml
    /// </summary>
    public partial class BlackjackWindow : Window, INotifyPropertyChanged
    {
        internal TavernWindow RefToTavernWindow { get; set; }
        private List<Card> cardList = new List<Card>();
        private Hand playerHand = new Hand();
        private Hand dealerHand = new Hand();
        private int index = 0, bet = 0, _totalWins = 0, _totalLosses = 0, _totalDraws = 0, _totalBetWinnings = 0, _totalBetLosses = 0;
        private int sidePot = 0;
        private bool handOver = true;
        private string nl = Environment.NewLine;

        #region Properties

        public int TotalWins
        {
            get { return _totalWins; }
            set { _totalWins = value; OnPropertyChanged("Statistics"); }
        }

        public int TotalLosses
        {
            get { return _totalLosses; }
            set { _totalLosses = value; OnPropertyChanged("Statistics"); }
        }

        public int TotalDraws
        {
            get { return _totalDraws; }
            set { _totalDraws = value; OnPropertyChanged("Statistics"); }
        }

        public int TotalBetWinnings
        {
            get { return _totalBetWinnings; }
            set { _totalBetWinnings = value; OnPropertyChanged("Statistics"); }
        }

        public int TotalBetLosses
        {
            get { return _totalBetLosses; }
            set { _totalBetLosses = value; OnPropertyChanged("Statistics"); }
        }

        public string Statistics
        {
            get { return "Wins: " + TotalWins.ToString("N0") + nl + "Losses: " + TotalLosses.ToString("N0") + nl + "Draws: " + TotalDraws.ToString("N0") + nl + "Gold Won: " + TotalBetWinnings.ToString("N0") + nl + "Gold Lost: " + TotalBetLosses.ToString("N0"); }
        }

        #endregion Properties

        #region Data-Binding

        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Binds information to controls.
        /// </summary>
        //private void BindLabels()
        //{
        //    lstPlayer.DisplayMember = "CardToString";
        //    lstPlayer.DataSource = playerHand.CardList;
        //    //lstDealer.DataSource = dealerHand.CardList;
        //    lblPlayerTotal.DataBindings.Add("Text", playerHand, "Value");
        //    lblDealerTotal.DataBindings.Add("Text", dealerHand, "Value");
        //    lblGold.DataBindings.Add("Text", GameState.currentHero, "GoldToString");
        //    lblStatistics.DataBindings.Add("Text", this, "Statistics");
        //}

        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion Data-Binding

        #region Check Logic

        /// <summary>
        /// Checks whether a Hand has reached Blackjack.
        /// </summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <returns>Returns true if the Hand's value is 21.</returns>
        private bool CheckBlackjack(Hand checkHand)
        {
            if (checkHand.TotalValue() == 21)
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether the current Hand has gone Bust.
        /// </summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <returns>Returns true if Hand has gone Bust.</returns>
        private bool CheckBust(Hand checkHand)
        {
            if (!CheckHasAceEleven(checkHand) && checkHand.TotalValue() > 21)
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether the Player can Double Down with this Hand.
        /// </summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <param name="checkSplit">Hand the Hand could be split into</param>
        /// <returns>Returns true if Hand can Double Down.</returns>
        private bool CheckCanHandDoubleDown(Hand checkHand, Hand checkSplit)
        {
            if (checkHand.CardList.Count == 2 && checkSplit.CardList.Count == 0)
            {
                return (checkHand.TotalValue() >= 9) && (checkHand.TotalValue() <= 11);
            }
            return false;
        }

        /// <summary>
        /// Checks whether the Player can split a specific Hand.
        /// </summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <param name="checkSplit">Hand it could be spilt into</param>
        /// <returns>Returns true if Hand can be Split.</returns>
        private bool CheckCanHandSplit(Hand checkHand, Hand checkSplit)
        {
            if (checkHand.CardList.Count == 2 && checkSplit.CardList.Count == 0)
                return checkHand.CardList[0].Value == checkHand.CardList[1].Value;
            return false;
        }

        /// <summary>
        /// Checks whether a Hand has an Ace valued at eleven in it.
        /// </summary>
        /// <param name="checkHand">Hand to be checked</param>
        /// <returns>Returns true if Hand has an Ace valued at eleven in it.</returns>
        private bool CheckHasAceEleven(Hand checkHand)
        {
            foreach (Card card in checkHand.CardList)
            {
                if (card.Value == 11)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether the Dealer has an Ace face up.
        /// </summary>
        /// <returns>Returns true if the Dealer has an Ace face up.</returns>
        private bool CheckInsurance()
        {
            if (!handOver && dealerHand.CardList[0].Value == 11 && sidePot == 0)
                return true;
            return false;
        }

        #endregion Check Logic

        #region Card Management

        /// <summary>
        /// Converts the first Ace valued at eleven in the Hand to be valued at one.
        /// </summary>
        /// <param name="handConvert">Hand to be converted.</param>
        private void ConvertAce(Hand handConvert)
        {
            foreach (Card card in handConvert.CardList)
            {
                if (card.Value == 11)
                {
                    card.Value = 1;
                    break;
                }
            }
        }

        /// <summary>
        /// Creates a deck of Cards.
        /// </summary>
        private void CreateDeck(int numberOfDecks)
        {
            cardList.Clear();
            for (int h = 0; h < numberOfDecks; h++)
            {
                for (int i = 1; i < 14; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        string name = "";
                        CardSuit suit = CardSuit.Spades;
                        int value = 0;

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
                        cardList.Add(newCard);
                    }
                }
            }
        }

        /// <summary>
        /// Deals a Card to a specific Hand.
        /// </summary>
        /// <param name="handAdd">Hand where Card is to be added.</param>
        private void DealCard(Hand handAdd)
        {
            handAdd.CardList.Add(cardList[index]);
            index++;
        }

        /// <summary>
        /// Creates a new Hand for both the Player and the Dealer.
        /// </summary>
        private void NewHand()
        {
            playerHand = new Hand();
            dealerHand = new Hand();

            handOver = false;
            txtBet.IsEnabled = false;
            btnNewHand.IsEnabled = false;
            btnExit.IsEnabled = false;
            if (index >= (cardList.Count * 0.8))
            {
                index = 0;
                cardList.Shuffle();
            }

            for (int i = 0; i < 2; i++)
            {
                DealCard(playerHand);
                DealCard(dealerHand);
            }
            DisplayHand();
        }

        #endregion Card Management

        private void DealerAction()
        {
            bool keepGoing = true;

            while (keepGoing)
            {
                if (dealerHand.TotalValue() == 21)
                    keepGoing = false;
                else if (dealerHand.TotalValue() >= 17)
                {
                    if (dealerHand.TotalValue() > 21 && CheckHasAceEleven(dealerHand))
                        ConvertAce(dealerHand);
                    else
                        keepGoing = false;
                }
                else
                    DealCard(dealerHand);
            }
        }

        #region Button Management

        /// <summary>
        /// Checks which Buttons should be enabled.
        /// </summary>
        private void CheckButtons()
        {
            if (playerHand.TotalValue() < 21)
            {
                btnHit.IsEnabled = true;
                btnStay.IsEnabled = true;
            }
            else
            {
                btnHit.IsEnabled = false;
                btnStay.IsEnabled = false;
            }

            if (CheckHasAceEleven(playerHand))
                btnConvertAce.IsEnabled = true;
            else
                btnConvertAce.IsEnabled = false;
        }

        /// <summary>
        /// Disables all the buttons on the Window except for btnNewHand.
        /// </summary>
        private void DisablePlayButtons()
        {
            btnNewHand.IsEnabled = true;
            btnExit.IsEnabled = true;
            btnHit.IsEnabled = false;
            btnStay.IsEnabled = false;
            btnConvertAce.IsEnabled = false;
        }

        #endregion Button Management

        #region Game Results

        /// <summary>
        /// The game ends in a draw.
        /// </summary>
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
            handOver = true;
            txtBet.IsEnabled = true;
            DisplayDealerHand();
            DisablePlayButtons();
            DisplayStatistics();
        }

        /// <summary>
        /// The game ends with the Player losing.
        /// </summary>
        /// <param name="betAmount">Amount the Player bet</param>
        private void LoseBlackjack(int betAmount)
        {
            AddTextTT("You lose " + betAmount + ".");
            GameState.CurrentHero.Inventory.Gold -= betAmount;
            TotalLosses += 1;
            TotalBetLosses += betAmount;
            EndHand();
        }

        /// <summary>
        /// Player either chooses not to draw a card or has reached 21 with more than 2 cards.
        /// </summary>
        private void Stay()
        {
            handOver = true;
            DealerAction();
            DisplayDealerHand();

            if (playerHand.TotalValue() > dealerHand.TotalValue() && !CheckBust(dealerHand))
                WinBlackjack(bet);
            else if (CheckBust(dealerHand))
                WinBlackjack(bet);
            else if (playerHand.TotalValue() == dealerHand.TotalValue())
                DrawBlackjack();
            else if (playerHand.TotalValue() < dealerHand.TotalValue())
                LoseBlackjack(bet);
        }

        /// <summary>
        /// The game ends with the Player winning.
        /// </summary>
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

        /// <summary>
        /// Adds text to the txtBlackjack TextBox.
        /// </summary>
        /// <param name="newText">Text to be added</param>
        private void AddTextTT(string newText)
        {
            txtBlackjack.Text += nl + nl + newText;
            txtBlackjack.Focus();
            txtBlackjack.CaretIndex = txtBlackjack.Text.Length;
            txtBlackjack.ScrollToEnd();
        }

        /// <summary>
        /// Displays the Dealer's Hand.
        /// </summary>
        private void DisplayDealerHand()
        {
            lstDealer.Items.Clear();

            if (!handOver)
            {
                lstDealer.Items.Add(dealerHand.CardList[0].Name + " of " + dealerHand.CardList[0].Suit);
                lstDealer.Items.Add("?? of ??");
                lblDealerTotal.Text = dealerHand.CardList[0].Value.ToString();
            }
            else
            {
                foreach (Card card in dealerHand.CardList)
                    lstDealer.Items.Add(card.Name + " of " + card.Suit);

                lblDealerTotal.Text = dealerHand.TotalValue().ToString();
            }
        }

        /// <summary>
        /// Displays all Cards in both the Player and the Dealer's Hands.
        /// </summary>
        private void DisplayHand()
        {
            DisplayDealerHand();
            DisplayPlayerHand();

            if (!CheckBlackjack(playerHand) && !CheckBust(playerHand) && !handOver) //Player can still play
            {
                CheckButtons();
            }
            else if (CheckBust(playerHand))
            {
                AddTextTT("You bust!");
                LoseBlackjack(bet);
            }
            else if (CheckBlackjack(playerHand))
            {
                if (playerHand.CardList.Count == 2)
                {
                    AddTextTT("You have a natural blackjack!");
                    if (dealerHand.TotalValue() != 21)
                        WinBlackjack(Int32Helper.Parse(bet * 1.5));
                    else
                    {
                        AddTextTT("You and the dealer both have natural blackjacks.");
                        DrawBlackjack();
                    }
                }
                else
                    Stay();
            }
            else if (!handOver)
            {
                // FUTURE
            }
        }

        /// <summary>
        /// Displays the Player's Hand.
        /// </summary>
        private void DisplayPlayerHand()
        {
            lstPlayer.Items.Clear();

            foreach (Card card in playerHand.CardList)
            {
                lstPlayer.Items.Add(card.Name + " of " + card.Suit);
            }
            lblPlayerTotal.Text = playerHand.TotalValue().ToString();
        }

        /// <summary>
        /// Displays the game's statistics.
        /// </summary>
        private void DisplayStatistics()
        {
            lblStatistics.Text = Statistics;
            lblGold.Text = "Gold: " + GameState.CurrentHero.Inventory.Gold.ToString("N0");
        }

        #endregion Display Manipulation

        #region Button-Click Methods

        private void btnConvertAce_Click(object sender, RoutedEventArgs e)
        {
            ConvertAce(playerHand);
            DisplayHand();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            playerHand.CardList.Add(new Card(cardList[index]));
            index++;
            if (playerHand.CardList.Count < 5)
                DisplayHand();
            else
            {
                if (playerHand.TotalValue() < 21 || (CheckHasAceEleven(playerHand) && playerHand.TotalValue() <= 31))
                {
                    AddTextTT("Five Card Charlie!");
                    DisplayPlayerHand();
                    WinBlackjack(bet);
                }
                else
                    DisplayHand();
            }
        }

        private void btnNewHand_Click(object sender, RoutedEventArgs e)
        {
            bet = Int32Helper.Parse(txtBet.Text);
            if (bet > 0 && bet <= GameState.CurrentHero.Inventory.Gold)
                NewHand();
            else if (bet > GameState.CurrentHero.Inventory.Gold)
                MessageBox.Show("You can't bet more gold than you have!", "Sulimn", MessageBoxButton.OK);
            else
                MessageBox.Show("Please enter a valid bet.", "Sulimn", MessageBoxButton.OK);
        }

        private void btnStay_Click(object sender, RoutedEventArgs e)
        {
            handOver = true;
            DealerAction();
            DisplayDealerHand();

            if (playerHand.TotalValue() > dealerHand.TotalValue() && !CheckBust(dealerHand))
                WinBlackjack(bet);
            else if (CheckBust(dealerHand))
                WinBlackjack(bet);
            else if (playerHand.TotalValue() == dealerHand.TotalValue())
                DrawBlackjack();
            else if (playerHand.TotalValue() < dealerHand.TotalValue())
                LoseBlackjack(bet);
        }

        #endregion Button-Click Methods

        #region Window-Manipulation Methods

        /// <summary>
        /// Closes the Window.
        /// </summary>
        private void CloseWindow()
        {
            if (handOver)
            {
                this.Close();
            }
        }

        public BlackjackWindow()
        {
            InitializeComponent();
            CreateDeck(6);
            cardList.Shuffle();
            DisplayStatistics();
            txtBlackjack.Text = "You approach a table where Blackjack is being played. You take a seat." + nl + nl + "\"Care to place a bet?\" asks the dealer.";
            txtBet.Focus();
        }

        private void txtBet_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBet.SelectAll();
        }

        private void txtBet_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key k = e.Key;

            List<bool> keys = Functions.GetListOfKeys(Key.Back, Key.Delete, Key.Home, Key.End, Key.LeftShift, Key.RightShift, Key.Enter, Key.Tab, Key.LeftAlt, Key.RightAlt, Key.Left, Key.Right, Key.LeftCtrl, Key.RightCtrl, Key.Escape);

            if (keys.Any(key => key == true) || (Key.D0 <= k && k <= Key.D9) || (Key.NumPad0 <= k && k <= Key.NumPad9))
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

            if (txtBet.Text.Length > 0)
                btnNewHand.IsEnabled = true;
            else
                btnNewHand.IsEnabled = false;
        }

        private async void windowBlackjack_Closing(object sender, CancelEventArgs e)
        {
            if (handOver)
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