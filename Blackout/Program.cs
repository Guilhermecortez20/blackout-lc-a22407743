using System;
using Blackout.Model;

namespace Blackout
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Game game = new Game(Difficulty.Hard);

            for (int row = 0; row < game.Size; row++)
            {
                for (int col = 0; col < game.Size; col++)
                {
                    Console.Write(game.IsCellOn(row, col) ? "■ " : "□ ");
                }
                Console.WriteLine();
            }
        }
    }
}