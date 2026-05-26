using System;

namespace Blackout
{
    public class Grid
    {
        private readonly bool[,] _cells;

        public int Size { get; }

        public Grid(int size, int randomClicks)
        {
            Size = size;
            _cells = new bool[size, size];
            ApplyRandomClicks(randomClicks);
        }

        public bool IsOn(int row, int col) => _cells[row, col];

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

        public void Toggle(int row, int col)
        {
            ToggleCell(row, col);
            ToggleCell(row - 1, col);
            ToggleCell(row + 1, col);
            ToggleCell(row, col - 1);
            ToggleCell(row, col + 1);
        }

        private void ToggleCell(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
                return;

            _cells[row, col] = !_cells[row, col];
        }

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