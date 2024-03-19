using Cards.Cards;
using Cards.Enums;

namespace Cards.Hands
{
    public class BlackjackHand : Hand
    {
        private int _score;
        private bool _isSoft;
        public BlackjackHand() : base()
        {
            _score = 0;    
            _isSoft = false;
        }
        public bool IsSoft => _isSoft;
        public bool IsBusted => !_isSoft && _score > 21;
        public bool IsBlackjack => Cards.Count == 2 && Score == 21;
        public int Score => _score;
        public string ScoreDisplay => $"{(_isSoft ? $"{_score - 10}/" : "")}{_score}";

        public override void DealCard(Card card)
        {
            base.DealCard(card);
            BlackjackCard bjCard = new BlackjackCard(card.CardValue);
            _score += bjCard.NumberValue;
            if (card.CardValue.Equals(CardValue.Ace))
            {
                if(_isSoft)
                {
                    _score -= 10;
                }
                _isSoft = true;
            }
            if (_score > 21) 
            {
                if (_isSoft)
                {
                    _score -= 10;
                    _isSoft = false;
                }
            }

        }
    }
}
