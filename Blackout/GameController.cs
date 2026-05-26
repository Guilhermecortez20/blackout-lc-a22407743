using System;

namespace Blackout
{
    public class GameController
    {
        private Game _game;
        private readonly IGameView _view;
        private int _selectorRow;
        private int _selectorCol;

        public GameController(Game game, IGameView view)
        {
            _game = game;
            _view = view;
            _selectorRow = 0;
            _selectorCol = 0;
        }

        public void Run()
        {
            bool playing = _view.ShowMainMenu();

            while (playing)
            {
                Difficulty difficulty = _view.AskDifficulty();
                _view.ShowInstructions();
                _game = new Game(difficulty);
                _selectorRow = 0;
                _selectorCol = 0;
                RunGameLoop();
                playing = _view.ShowMainMenu();
            }
        }

        private void RunGameLoop()
        {
            bool inGame = true;

            while (inGame)
            {
                _view.RenderGrid(_game, _selectorRow, _selectorCol);
                ConsoleKey key = _view.ReadKey();

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MoveSelector(-1, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        MoveSelector(1, 0);
                        break;
                    case ConsoleKey.LeftArrow:
                        MoveSelector(0, -1);
                        break;
                    case ConsoleKey.RightArrow:
                        MoveSelector(0, 1);
                        break;
                    case ConsoleKey.Enter:
                        _game.Toggle(_selectorRow, _selectorCol);
                        break;
                }

                if (_game.IsWon())
                {
                    _view.ShowWinMessage(_game.Moves);
                    inGame = _view.AskPlayAgain();
                }
            }
        }

        private void MoveSelector(int deltaRow, int deltaCol)
        {
            _selectorRow = Math.Clamp(_selectorRow + deltaRow, 0, _game.Size - 1);
            _selectorCol = Math.Clamp(_selectorCol + deltaCol, 0, _game.Size - 1);
        }
    }
}