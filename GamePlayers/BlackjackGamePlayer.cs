using Cards.Games;
using static Cards.Helpers.ConsoleHelper;

namespace Cards.GamePlayers
{
    public class BlackjackGamePlayer
    {
        private BlackjackGame _currentGame;
        private readonly DataLoader _loader;
        private string _lastUserInput;
        private Dictionary<string, Action<BlackjackGame>> _userActionsMap;
        public BlackjackGamePlayer()
        {
            _loader = null!;
            _currentGame = null!;
            _lastUserInput = "";
            InitializeUserActionsMap();
        }
        public BlackjackGamePlayer(DataLoader loader)
        {
            _loader = loader;
            _currentGame = null!;
            _lastUserInput = "";
            InitializeUserActionsMap();
        }

        public void Play()
        {
            while(true)
            {
                _currentGame = new BlackjackGame(_loader);
                while (_currentGame.IsPlaying)
                {
                    _lastUserInput = PromptUser($"You have: {_currentGame.PlayerScore}. Dealer has: {_currentGame.DealerUpCard.NumberValue}. 'h' for Hit, 's' for Stand.");
                    if (!_userActionsMap.ContainsKey(_lastUserInput))
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    else
                    {
                        _userActionsMap[_lastUserInput](_currentGame);
                    }
                }
                _lastUserInput = PromptUser("Press enter to start a new game, enter 'x' to quit.");

                if(_lastUserInput == "x")
                {
                    return;
                }
            }
        }
        private void InitializeUserActionsMap()
        {
            if(_userActionsMap == null)
            {
                _userActionsMap = new();
                _userActionsMap.Add("s", (game) => game.Stand());
                _userActionsMap.Add("h", (game) => game.Hit());
            }
        }
    }
}
