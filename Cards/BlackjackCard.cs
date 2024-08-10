using Cards.Enums;

namespace Cards.Cards
{
    public class BlackjackCard : Card
    {
        private int _numberValue;
        public BlackjackCard() : base()
        {
            _numberValue = 0;
        }
        public BlackjackCard(CardValue value, CardSuit suit) : base(value, suit)
        {
            SetNumberValue(value);
        }
        public override int NumberValue => _numberValue;
        private void SetNumberValue(CardValue value)
        {
            if (value.Equals(CardValue.Ace))
            {
                _numberValue = 11;
                return;
            }
            else if ((int)value >= 10)
                _numberValue = 10;
            else
                _numberValue = (int)value;
        }
    }
}
