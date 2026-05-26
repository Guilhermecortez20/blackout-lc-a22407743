namespace Blackout
{
    /// <summary>
    /// Represents the game session, part of the Model in MVC.
    /// Manages the Grid state, move count and win condition.
    /// </summary>
    public class Game
    {
        private readonly Grid _grid;

        /// <summary>
        /// The difficulty level of the current game session.
        /// </summary>
        public Difficulty Level { get; }

        /// <summary>
        /// The number of moves the player has made so far.
        /// </summary>
        public int Moves { get; private set; }

        /// <summary>
        /// The size of the grid (number of rows and columns).
        /// </summary>
        public int Size => _grid.Size;

        /// <summary>
        /// Creates a new game session with a grid randomized according to the given difficulty level.
        /// </summary>
        /// <param name="difficulty">The difficulty level chosen by the player.</param>
        public Game(Difficulty difficulty)
        {
            Level = difficulty;
            int size = GetSize(difficulty);
            int randomClicks = GetRandomClicks(difficulty);
            _grid = new Grid(size, randomClicks);
            Moves = 0;
        }

        /// <summary>
        /// Returns whether the cell at the given position is currently on.
        /// </summary>
        /// <param name="row">The row index of the cell.<</param>
        /// <param name="col">The column index of the cell</param>
        /// <returns>True if the cell is on, false if off.</returns>
        public bool IsCellOn(int row, int col) => _grid.IsOn(row, col);

        /// <summary>
        /// Toggles the cell at the given position and its adjacent cells.
        /// Also increments the move counter.
        /// </summary>
        /// <param name="row">The row index of the cell to toggle.</param>
        /// <param name="col">The column index of the cell to toggle.</param>
        public void Toggle(int row, int col)
        {
            _grid.Toggle(row, col);
            Moves++;
        }

        /// <summary>
        /// Returns whether the player has won by checking if all cells are off.
        /// </summary>
        /// <returns>True if all cells are off, false otherwise.</returns>
        public bool IsWon() => _grid.IsCleared();

        /// <summary>
        /// Returns the grid size for the given difficulty level.
        /// </summary>
        /// <param name="difficulty">The difficulty level to get the size for.</param>
        /// <returns>The grid size as an integer.</returns>
        private int GetSize(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy: return 3;
                case Difficulty.Medium: return 5;
                case Difficulty.Hard: return 8;
                default: return 3;
            }
        }

        /// <summary>
        /// Returns the number of random clicks used to randomize the grid for the given difficulty level.
        /// </summary>
        /// <param name="difficulty">The difficulty level to get the click count for.</param>
        /// <returns>The number of random clicks as an integer.</returns>
        private int GetRandomClicks(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy: return 3;
                case Difficulty.Medium: return 5;
                case Difficulty.Hard: return 10;
                default: return 3;
            }
        }
    }
}