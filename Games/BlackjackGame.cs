using Cards.Cards;
using Cards.Enums;
using Cards.Extensions;
using Cards.Hands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Games
{
    public class BlackjackGame
    {
        const int SHUFFLES_PER_DECK = 5;
        private BlackjackHand _playerHand;
        private BlackjackHand _dealerHand;
        private DeckOfCards _deckOfCards;
        private BlackjackCard _dealerUpCard;
        private bool _isPlaying;
        public BlackjackGame(DataLoader loader)
        {
            Console.Clear();
            _playerHand = new BlackjackHand();
            _dealerHand = new BlackjackHand();
            _deckOfCards = new DeckOfCards(loader);
            for(int i = 0; i <  SHUFFLES_PER_DECK; i++)
            {
                _deckOfCards.Shuffle();
            }
            _isPlaying = true;
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
            }
            else if (_dealerUpCard.NumberValue == 10)
            {
                //Check for BJ
            }
            if (_playerHand.IsBlackjack)
            {
                Stand();
            }
        }

        private void DealPlayerCard()
        {
            _playerHand.DealCard(_deckOfCards.DrawCard());
        }
        private void DealDealerCard()
        {
            var card = _deckOfCards.DrawCard();
            if(_dealerHand.Cards.Count == 0)
            {
                _dealerUpCard = new BlackjackCard(card.CardValue);
            }
            _dealerHand.DealCard(card);
        }
        public void Hit()
        {
            DealPlayerCard();
            if(_playerHand.IsBusted)
            {
                Console.WriteLine($"Bust! Score: {_playerHand.Score}.");
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
            if(_dealerHand.IsBusted || _playerHand.Score > _dealerHand.Score)
            {
                Console.WriteLine($"Winner! You have: {_playerHand.Score}. Dealer had {_dealerHand.Score}.");
            }
            else if(_dealerHand.Score == _playerHand.Score)
            {
                Console.WriteLine($"Push, both players have {_dealerHand.Score}.");
            }
            else
            {
                Console.WriteLine($"You lose.");
            }
            DisplayHands();
            _isPlaying = false;
        }
        private void DealerDrawToSeventeen()
        {
            while(_dealerHand.Score < 17)
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
    }
}
