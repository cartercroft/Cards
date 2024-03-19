using Cards.Enums;

namespace Cards.Extensions
{
    public static class EnumExtensions
    {
        public static char GetSuitSymbol(this CardSuit suit)
        {
            //See https://theasciicode.com.ar/ascii-control-characters/end-of-text-hearts-card-suit-ascii-code-3.html
            return (char)((int)suit + 3);
        }
    }
}
