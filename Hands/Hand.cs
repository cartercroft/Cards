using Cards.Cards;

namespace Cards.Hands
{
    public class Hand
    {
        public Hand()
        {
            Cards = new List<Card>();
        }
        public Hand(List<Card> cards, int maxCards)
        {
            Cards = cards;
        }
        public virtual List<Card> Cards { get; set; }
        public virtual void DealCard(Card card)
        {
            Cards.Add(card);
        }
    }
}
