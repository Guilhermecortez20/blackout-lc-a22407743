namespace Blackout
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IGameView view = new GameView();
            Game game = new Game(Difficulty.Easy);
            GameController controller = new GameController(game, view);
            controller.Run();
        }
    }
}