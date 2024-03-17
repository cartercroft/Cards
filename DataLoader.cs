using Cards.Cards;
using Newtonsoft.Json;

namespace Cards
{
    public class DataLoader
    {
        private List<Card>? _defaultDeck = null;
        public List<Card> DefaultDeck
        {
            get
            {
                if (_defaultDeck != null)
                {
                    return _defaultDeck;
                }
                using (StreamReader reader = new StreamReader($"{Environment.CurrentDirectory}\\Content\\DeckData.json"))
                {
                    Console.WriteLine("Loading JSON...");
                    string json = reader.ReadToEnd();
                    Console.WriteLine("Done Loading JSON.");
                    List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(json) ?? new();
                    _defaultDeck = cards;
                }
                return _defaultDeck;
            }
        }
    }
}
