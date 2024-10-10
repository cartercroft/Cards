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
            DealPlayerCard();
            DealDealerCard();
            DealPlayerCard();
            DealDealerCard();

            if(_dealerUpCard.CardValue == CardValue.Ace)
            {
                //offer insurance
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
                ShowResults();
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
        private void DealPlayerCard()
        {
            _playerHand.DealCard(_deckOfCards.DrawCard());
        }
        private void DealDealerCard()
        {
            var card = _deckOfCards.DrawCard();
            if (!_dealerHand.Cards.Any())
            {
                _dealerUpCard = new BlackjackCard(card.CardValue, card.CardSuit);
            }
            _dealerHand.DealCard(card);
        }
        private void DealerDrawToSeventeen()
        {
            while (_dealerHand.Score < 17)
            {
                DealDealerCard();
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
        private void ShowResults()
        {
            if (_tookInsurance && _dealerHand.IsBlackjack)
            {
                Console.WriteLine("Dealer had blackjack. Player wins 2 to 1 on the insurance bet.");
            }
            if (_dealerHand.IsBusted || _playerHand.Score > _dealerHand.Score)
            {
                Console.WriteLine($"Winner! Player has: {_playerHand.Score}. Dealer has: {_dealerHand.Score}.");
            }
            else if (_dealerHand.Score == _playerHand.Score)
            {
                Console.WriteLine($"Push, dealer and player both have {_dealerHand.Score}.");
            }
            else
            {
                Console.WriteLine($"Player loses.");
            }
            DisplayHands();
            _isPlaying = false;
        }
        public void Hit()
        {
            DealPlayerCard();
            if(_playerHand.IsBusted)
            {
                Console.WriteLine($"Bust!");
                DisplayHands();
                _isPlaying = false;
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
            ShowResults();
        }
    }
}
