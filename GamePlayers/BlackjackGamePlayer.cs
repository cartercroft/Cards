using Cards.Games;
using static Cards.Helpers.ConsoleHelper;

namespace Cards.GamePlayers
{
    public class BlackjackGamePlayer
    {
        private BlackjackGame _game;
        private CardDataLoader _loader;
        private string _lastUserInput;
        public BlackjackGamePlayer(CardDataLoader loader)
        {
            _loader = loader;
            _game = null!;
            _lastUserInput = "";
        }

        public void Play()
        {
            while(true)
            {
                _game = new BlackjackGame(_loader);
                while (_game.IsPlaying)
                {
                    _lastUserInput = PromptUser($"You have: {_game.PlayerScore}. Dealer has: {_game.DealerUpCard.NumberValue}. 'h' for Hit, 's' for Stand.");
                    if (_lastUserInput == "s")
                    {
                        _game.Stand();
                    }
                    else if (_lastUserInput == "h")
                    {
                        _game.Hit();
                    }
                }
                _lastUserInput = PromptUser("Press enter to start a new game, enter 'x' to quit.");

                if(_lastUserInput == "x")
                {
                    return;
                }
            }
        }
    }
}
