using Cards.Enums;

namespace Cards.Cards
{
    public class Card
    {
        public Card()
        {
            CardSuit = CardSuit.None;
            CardValue = CardValue.None;
        }
        public Card(CardValue value, CardSuit suit) 
        {
            CardValue = value;
            CardSuit = suit;
        }
        public CardSuit CardSuit { get; set; }
        public CardValue CardValue { get; set; }
        public virtual int NumberValue => (int)CardValue;
        public override string ToString()
        {
            //https://theasciicode.com.ar/ascii-control-characters/start-of-header-ascii-code-1.html
            //offset ascii value by 3 to get suits
            return $"{CardValue} {(char)(CardSuit + 3)}";
        }
    }
}
