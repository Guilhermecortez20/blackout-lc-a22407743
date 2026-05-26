using System;

namespace Blackout
{
    public interface IGameView
    {
        bool ShowMainMenu();
        Difficulty AskDifficulty();
        void RenderGrid(Game game, int selectorRow, int selectorCol);
        ConsoleKey ReadKey();
        void ShowWinMessage(int moves);
        bool AskPlayAgain();
    }
}