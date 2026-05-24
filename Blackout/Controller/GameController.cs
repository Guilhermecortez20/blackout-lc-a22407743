using System;
using Blackout.Model;

namespace Blackout.Controller
{
    public class GameController
    {
        private Game _game;
        private int _selectorRow;
        private int _selectorCol;

        public GameController(Game game)
        {
            _game = game;
            _selectorRow = 0;
            _selectorCol = 0;
        }

        public void Run()
        {
            bool playing = true;

            while (playing)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;

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
                    playing = AskPlayAgain();
                }
            }
        }

        public int SelectorRow => _selectorRow;
        public int SelectorCol => _selectorCol;

        private void MoveSelector(int deltaRow, int deltaCol)
        {
            _selectorRow = Math.Clamp(_selectorRow + deltaRow, 0, _game.Size - 1);
            _selectorCol = Math.Clamp(_selectorCol + deltaCol, 0, _game.Size - 1);           
        }

        private bool AskPlayAgain()
        {
            Console.WriteLine("You won! Play again? (Y/N)");
            ConsoleKey key = Console.ReadKey(intercept: true).Key;

            if (key == ConsoleKey.Y)
            {
                _game = new Game(_game.Level);
                _selectorRow = 0;
                _selectorCol = 0;
                return true;
            }

            return false;            
        }
    }
}