namespace Blackout
{
    /// <summary>
    ///  Represents the difficulty levels available in the game.
    /// Each level determines the grid size and number of initial random clicks.
    /// </summary>
    public enum Difficulty
    {
        /// <summary>
        /// 3x3 grid, seeded with 3 initial random cell toggles.
        /// </summary>
        Easy,
        /// <summary>
        /// 5x5 grid, seeded with 5 initial random cell toggles.
        /// </summary>
        Medium,
        /// <summary>
        /// 8x8 grid, seeded with 8 initial random cell toggles.
        /// </summary>
        Hard
    }
}