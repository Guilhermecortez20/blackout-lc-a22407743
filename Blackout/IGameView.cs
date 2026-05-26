using System;

namespace Blackout
{
    public interface IGameView
    {
        bool ShowMainMenu();
        Difficulty AskDifficulty();
        void ShowInstructions();
        void RenderGrid(Game game, int selectorRow, int selectorCol);
        ConsoleKey ReadKey();
        void ShowWinMessage(int moves);
        bool AskPlayAgain();
    }
}