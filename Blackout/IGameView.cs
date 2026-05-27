using System;

namespace Blackout
{
    /// <summary>
    /// Interface for the game's View in the MVC pattern.
    /// Any class implementing this interface can be used as the game's UI.
    /// </summary>
    public interface IGameView
    {
        /// <summary>
        /// Displays the Main Menu and returns whether the player chose to play.
        /// </summary>
        /// <returns>True if the player chose Play, false if Quit.</returns>
        bool ShowMainMenu();

        /// <summary>
        /// Displays the difficulty selection menu and returns the chosen difficulty.
        /// </summary>
        /// <returns>The <see cref="Difficulty"/> chosen by the player.</returns>
        Difficulty AskDifficulty();
        
        /// <summary>
        /// Displays the instructions screen and waits for the player to press a key.
        /// </summary>
        void ShowInstructions();
        
        /// <summary>
        /// Renders the current state of the grid, highlighting the selected cell.
        /// </summary>
        /// <param name="game">The game model to read cell states from.</param>
        /// <param name="selectorRow">The row index of the currently selected cell.</param>
        /// <param name="selectorCol">The column index of the currently selected cell</param>
        void RenderGrid(Game game, int selectorRow, int selectorCol);
        
        /// <summary>
        /// Reads a single key press from the player without displaying it.
        /// </summary>
        /// <returns>The <see cref="ConsoleKey"/> pressed by the player.</returns>
        ConsoleKey ReadKey();
        
        /// <summary>
        /// Displays the win message with the number of moves taken.
        /// </summary>
        /// <param name="moves">The number of moves the player took to win.</param>
        void ShowWinMessage(int moves);
        
        /// <summary>
        /// Asks the player if they want to play again.
        /// </summary>
        /// <returns>True if the player wants to play again, false otherwise.</returns>
        bool AskPlayAgain();

        /// <summary>
        /// Resets the view state for a new game session.
        /// </summary>
        void Reset();
    }
}