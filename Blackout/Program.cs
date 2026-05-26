namespace Blackout
{
    public class Program
    {
        private static void Main(string[] args)
        {
            GameView view = new GameView();
            Difficulty difficulty = view.AskDifficulty();
            Game game = new Game(difficulty);
            GameController controller = new GameController(game, view);
            controller.Run();
        }
    }
}