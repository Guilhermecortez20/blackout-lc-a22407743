namespace Blackout.Model
{
    public class Game
    {
        private readonly Grid _grid;

        public Difficulty Level { get; }

        public int Moves { get; private set; }

        public int Size => _grid.Size;

        public Game(Difficulty difficulty)
        {
            Level = difficulty;
            int size = GetSize(difficulty);
            int randomClicks = GetRandomClicks(difficulty);
            _grid = new Grid(size, randomClicks);
            Moves = 0;
        }

        public bool IsCellOn(int row, int col) => _grid.IsOn(row, col);

        public void Toggle(int row, int col)
        {
            _grid.Toggle(row, col);
            Moves++;
        }

        public bool IsWon() => _grid.IsCleared();

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