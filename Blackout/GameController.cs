using System;

namespace Blackout
{
    /// <summary>
    /// Represents the Controller in the MVC.
    /// Manages the game loop, user input and coordinates between Model and View.
    /// </summary>
    public class GameController
    {
        private Game _game;
        private readonly IGameView _view;
        private int _selectorRow;
        private int _selectorCol;

        /// <summary>
        /// Creates a new instance of the GameController.
        /// </summary>
        /// <param name="view">The view used to render the game and read input.</param>
        public GameController(IGameView view)
        {
            _view = view;
            _selectorRow = 0;
            _selectorCol = 0;
        }

        /// <summary>
        /// Starts the application, showing the main menu and managing the flow between menus and game sessions.
        /// </summary>
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

        /// <summary>
        /// Runs the main game loop, handling input and checking the win condition after each move.
        /// </summary>
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
                    bool playAgain = _view.AskPlayAgain();
                    if (playAgain)
                    {
                        _game = new Game(_game.Level);
                        _selectorRow = 0;
                        _selectorCol = 0;
                        _view.Reset();
                    }
                    else
                    {
                        inGame = false;
                    }
                }
            }
        }

        /// <summary>
        /// Moves the cell selector by the given delta values, clamping it within the grid bounds.
        /// </summary>
        /// <param name="deltaRow">The row direction to move (-1 up, 1 down).</param>
        /// <param name="deltaCol">The column direction to move (-1 left, 1 right).</param>
        private void MoveSelector(int deltaRow, int deltaCol)
        {
            _selectorRow = Math.Clamp(_selectorRow + deltaRow, 0, _game.Size - 1);
            _selectorCol = Math.Clamp(_selectorCol + deltaCol, 0, _game.Size - 1);
        }
    }
}