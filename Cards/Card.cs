using Cards.Enums;
using Cards.Extensions;

namespace Cards.Cards
{
    public class Card
    {
        public Card()
        {
            CardSuit = CardSuit.None;
            CardValue = CardValue.None;
        }
        public Card(CardSuit suit, CardValue value) 
        {
            CardValue = value;
            CardSuit = suit;
        }
        public CardSuit CardSuit { get; set; }
        public CardValue CardValue { get; set; }
        public virtual int NumberValue => (int)CardValue;
        public override string ToString()
        {
            return $"{CardValue} {CardSuit.GetSuitSymbol()}";
        }
    }
}
