using Cards.Cards;
using Cards.Extensions;
using Newtonsoft.Json;

namespace Cards.Cards
{
    public class DeckOfCards
    {
        private List<Card> _stackOfCards;

        public DeckOfCards(DataLoader loader)
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
        public bool IsEmpty()
        {
            return _stackOfCards.Count == 0;
        }
    }
}
