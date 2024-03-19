using Cards.Cards;
using Cards.Enums;

namespace Cards.Hands
{
    public class BlackjackHand : Hand
    {
        private int _score;
        private bool _isSoft;
        private bool _isBusted;
        public BlackjackHand() : base()
        {
            _score = 0;    
            _isSoft = false;
            _isBusted = false;
        }
        public bool IsSoft => _isSoft;
        public bool IsBusted => !_isSoft && _score > 21;
        public bool IsBlackjack => Cards.Count == 2 && Score == 21;
        public int Score => _score;
        public string ScoreDisplay => GetScoreDisplay();

        public override void DealCard(Card card)
        {
            base.DealCard(card);
            BlackjackCard bjCard = new BlackjackCard(card.CardValue);
            _score += bjCard.NumberValue;
            if (card.CardValue.Equals(CardValue.Ace) && _score < 21)
            {
                _isSoft = true;
            }
            if (_score > 21) 
            {
                if (_isSoft)
                {
                    _score -= 10;
                    _isSoft = false;
                }
                else
                {
                    _isBusted = true;
                }
            }

        }
        private string GetScoreDisplay()
        {
            if (_isSoft)
            {
                return $"{_score - 10}/{_score}";
            }
            else return _score.ToString();
        }
    }
}
