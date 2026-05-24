using System;
using Blackout.Model;
using Blackout.Controller;

namespace Blackout
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Game game = new Game(Difficulty.Hard);
            GameController controller = new GameController(game);
            controller.Run();
        }
    }
}