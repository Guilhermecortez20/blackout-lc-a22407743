using System;

namespace Blackout
{
    /// <summary>
    /// Represents the game grid. Part of the Model in MVC.
    /// Manages the state of all cells and handles toggle logic.
    /// </summary>
    public class Grid
    {
        private readonly bool[,] _cells;

        /// <summary>
        /// The number of rows and columns in the grid.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Creates a new grid of the given size and randomizes it by applying a number of random toggles.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        /// <param name="randomClicks">The number of random toggles to apply.</param>
        public Grid(int size, int randomClicks)
        {
            Size = size;
            _cells = new bool[size, size];
            do
            {
                ApplyRandomClicks(randomClicks);
            } while (IsCleared());
        }

       /// <summary>
       /// Returns whether the cell at the given position is currently on.
       /// </summary>
       /// <param name="row">The row index of the cell.</param>
       /// <param name="col">The column index of the cell.</param>
       /// <returns>True if the cell is on, false if off.</returns>
        public bool IsOn(int row, int col) => _cells[row, col];

        /// <summary>
        /// Returns whether all cells in the grid are off.
        /// </summary>
        /// <returns>True if all cells are off, false otherwise.</returns>
        public bool IsCleared()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (_cells[row, col])
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Toggles the cell at the given position and its adjacent cells (above, below, left, and right).
        /// </summary>
        /// <param name="row">The row index of the cell to toggle.</param>
        /// <param name="col">The column index of the cell to toggle.</param>
        public void Toggle(int row, int col)
        {
            ToggleCell(row, col);
            ToggleCell(row - 1, col);
            ToggleCell(row + 1, col);
            ToggleCell(row, col - 1);
            ToggleCell(row, col + 1);
        }

        /// <summary>
        /// Toggles a single cell if its position is within the grid bounds.
        /// </summary>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="col">The column index of the cell.</param>
        private void ToggleCell(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return;

            _cells[row, col] = !_cells[row, col];
        }

        /// <summary>
        /// Randomizes the grid by applying a given number of random toggles.
        /// </summary>
        /// <param name="count">The number of random toggles to apply.</param>
        private void ApplyRandomClicks(int count)
        {
            Random rng = new();

            for (int i = 0; i < count; i++)
            {
                int row = rng.Next(Size);
                int col = rng.Next(Size);
                Toggle(row, col);
            }
        }
    }
}