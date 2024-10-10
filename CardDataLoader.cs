using Cards.Cards;
using Newtonsoft.Json;

namespace Cards
{
    public class CardDataLoader
    {
        private List<Card>? _defaultDeck = null;
        public List<Card> DefaultDeck
        {
            get
            {
                if (_defaultDeck != null)
                {
                    return new List<Card>(_defaultDeck);
                }
                using (StreamReader reader = new StreamReader($"{Environment.CurrentDirectory}\\Content\\DeckData.json"))
                {
                    string json = reader.ReadToEnd();
                    List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(json) ?? new();
                    _defaultDeck = cards;
                }
                return new List<Card>(_defaultDeck);
            }
        }
    }
}
