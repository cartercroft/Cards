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
        public int Score => _score;
        public string ScoreDisplay => GetScoreDisplay();

        public override void DealCard(Card card)
        {
            base.DealCard(card);
            BlackjackCard bjCard = new BlackjackCard(card.CardValue);
            _score += bjCard.NumberValue;
            if (card.CardValue.Equals(CardValue.Ace))
            {
                if(_score > 21)
                {
                    _isSoft = false;
                }
                else
                {
                    _isSoft = true;
                    _score -= 10;
                }
            }
        }
        private string GetScoreDisplay()
        {
            if (_isSoft)
            {
                return $"{_score}/{_score + 10}";
            }
            else return _score.ToString();
        }
    }
}
