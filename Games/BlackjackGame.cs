using Cards.Cards;
using Cards.Enums;
using Cards.Hands;
using static Cards.Helpers.ConsoleHelper;

namespace Cards.Games
{
    public class BlackjackGame
    {
        private const int SHUFFLES_PER_DECK = 5;
        private BlackjackHand _playerHand;
        private BlackjackHand _dealerHand;
        private DeckOfCards _deckOfCards;
        private BlackjackCard _dealerUpCard;
        private bool _isPlaying;
        private bool _tookInsurance;
        public BlackjackGame(CardDataLoader loader)
        {
            Console.Clear();
            _playerHand = new BlackjackHand();
            _dealerHand = new BlackjackHand();
            _deckOfCards = new DeckOfCards(loader);
            _dealerUpCard = null!;
            for(int i = 0; i < SHUFFLES_PER_DECK; i++)
            {
                _deckOfCards.Shuffle();
            }
            _isPlaying = true;
            _tookInsurance = false;
            InitializeHands();
        }

        public BlackjackCard DealerUpCard => _dealerUpCard;
        public bool IsPlaying => _isPlaying;
        public string PlayerScore => _playerHand.ScoreDisplay;
        public string DealerScore => _dealerHand.ScoreDisplay;
        private void InitializeHands()
        {
            DealCard(ref _playerHand);
            DealCard(ref _dealerHand, true);
            DealCard(ref _playerHand);
            DealCard(ref _dealerHand);


            if (_dealerUpCard.CardValue == CardValue.Ace)
            {
                OfferInsurance();
            }
            else if (_dealerUpCard.NumberValue == 10)
            {
                CheckForDealerBlackjack();
            }
            if (_playerHand.IsBlackjack)
            {
                Stand();
            }
        }
        private void CheckForDealerBlackjack()
        {
            if (_dealerHand.IsBlackjack)
            {
                EndHand();
            }
        }
        private void OfferInsurance()
        {
            while (true)
            {
                string userInput = PromptUser($"Dealer has {_dealerUpCard.NumberValue}. Player has {_playerHand.ScoreDisplay}. Would player like to accept insurance? (y/n)");
                if (userInput == "y")
                {
                    _tookInsurance = true;
                    if (_dealerHand.IsBlackjack)
                    {
                        Stand();
                        //Pay 2 to 1
                    }
                    Console.WriteLine("Wow, player took the sucker bet and won?");
                    break;
                }
                if(userInput == "n")
                {
                    if (_dealerHand.IsBlackjack)
                    {
                        Console.WriteLine("Dealer has blackjack.");
                        Stand();
                    }
                    break;
                }
            }
        }
        private void DealCard(ref BlackjackHand hand, bool isUpCard = false)
        {
            Card card = _deckOfCards.DrawCard();
            if(isUpCard)
            {
                _dealerUpCard = new BlackjackCard(card.CardValue, card.CardSuit);
            }
            hand.DealCard(card);
        }
        private void DealerDrawToSeventeen()
        {
            while (_dealerHand.Score < 17)
            {
                DealCard(ref _dealerHand);
            }
        }
        private void DisplayHand(BlackjackHand hand)
        {
            Console.WriteLine($"\t{string.Join(",", hand.Cards.Select(x => x.ToString()))}");
            Console.WriteLine($"Score: \r\n\t{hand.ScoreDisplay}.");
        }
        private void DisplayHands()
        {
            Console.WriteLine("Player has:");
            DisplayHand(_playerHand);
            Console.WriteLine();
            Console.WriteLine("Dealer has:");
            DisplayHand(_dealerHand);
        }
        private void EndHand()
        {
            if (_tookInsurance && _dealerHand.IsBlackjack)
            {
                HandleInsuranceWin();
            }
            if (!_playerHand.IsBusted && (_dealerHand.IsBusted || _playerHand.Score > _dealerHand.Score))
            {
                HandleWin();
            }
            else if (_dealerHand.Score == _playerHand.Score)
            {
                HandlePush();
            }
            else
            {
                HandleLoss();
            }

            DisplayHands();
            _isPlaying = false;
        }
        public void Hit()
        {
            DealCard(ref _playerHand);
            if(_playerHand.IsBusted)
            {
                EndHand();
            }
            else if(_playerHand.Score == 21)
            {
                Stand();
            }
        }
        public void Stand()
        {
            if (!_playerHand.IsBlackjack)
            {
                DealerDrawToSeventeen();
            }
            EndHand();
        }
        protected virtual void HandleWin()
        {
            Console.WriteLine($"Winner! Player has: {_playerHand.Score}. Dealer has: {_dealerHand.Score}.");
        }
        protected virtual void HandleInsuranceWin()
        {
            Console.WriteLine("Dealer had blackjack. Player wins 2 to 1 on the insurance bet.");
        }
        protected virtual void HandlePush()
        {
            Console.WriteLine($"Push, dealer and player both have {_dealerHand.Score}.");
        }
        protected virtual void HandleLoss()
        {
            Console.WriteLine($"Player loses.");
        }
    }
}
