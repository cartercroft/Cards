using Cards.Enums;

namespace Cards.Cards
{
    public class BlackjackCard : Card
    {
        private int _numberValue;
        private string _numberValueString;
        public BlackjackCard()
        {
            _numberValueString = "";
            _numberValue = 0;
        }
        public BlackjackCard(CardValue value)
        {
            _numberValueString = "";
            SetNumberValue(value);
        }
        public override int NumberValue => _numberValue;
        public string NumberValueString => _numberValueString;
        private void SetNumberValue(CardValue value)
        {
            if (value.Equals(CardValue.Ace))
            {
                _numberValue = 11;
                _numberValueString = "1/11";
                return;
            }
            else if ((int)value >= 10)
                _numberValue = 10;
            else
                _numberValue = (int)value;
            _numberValueString = _numberValue.ToString();
        }
    }
}
