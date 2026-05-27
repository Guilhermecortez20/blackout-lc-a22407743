namespace Blackout
{
    /// <summary>
    /// Entry point of the Blackout application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Initializes the MVC components and starts the game.
        /// </summary>
        /// <param name="args">Not used.</param>
        private static void Main(string[] args)
        {
            IGameView view = new GameView();
            Game game = new Game(Difficulty.Easy);
            GameController controller = new GameController(game, view);
            controller.Run();
        }
    }
}