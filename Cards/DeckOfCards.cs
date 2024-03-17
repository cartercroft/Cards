using Cards.Cards;
using Cards.Extensions;
using Newtonsoft.Json;

namespace Cards.Cards
{
    public class DeckOfCards
    {
        private Stack<Card> stackOfCards;
        public DeckOfCards(DataLoader loader)
        {
            List<Card> cards = loader.DefaultDeck;
            cards.Shuffle();

            stackOfCards = new Stack<Card>(cards);
        }
        public Card DrawCard()
        {
            return stackOfCards.Pop();
        }
        public bool IsEmpty()
        {
            return stackOfCards.Count == 0;
        }
    }
}
