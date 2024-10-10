using Cards.Cards;
using Cards.Enums;
using Cards.Extensions;
using Newtonsoft.Json;

namespace Cards.Cards
{
    public class DeckOfCards
    {
        private List<Card> _stackOfCards;

        public DeckOfCards(CardDataLoader loader)
        {
            _stackOfCards = loader.DefaultDeck;
            _stackOfCards.Shuffle();
        }
        public void Shuffle()
        {
            _stackOfCards.Shuffle();
        }
        public Card DrawCard()
        {
            var first = _stackOfCards.First();
            _stackOfCards.RemoveAt(0);
            return first;
        }
        public Card? GetCard(CardSuit suit, CardValue value)
        {
            return _stackOfCards.FirstOrDefault(c => c.CardSuit == suit && c.CardValue == value);
        }
        public bool IsEmpty()
        {
            return _stackOfCards.Count == 0;
        }
    }
}
