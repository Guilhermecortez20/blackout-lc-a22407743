using System;
using Blackout.Model;

namespace Blackout
{
    public class Program
    {
        private static void Main(string[] args)
        {
           Grid grid = new Grid(8, 10);

            for (int row = 0; row < grid.Size; row++)
            {
                for (int col = 0; col < grid.Size; col++)
                {
                    Console.Write(grid.IsOn(row, col) ? "■ " : "□ ");
                }
                Console.WriteLine();
            }
        }
    }
}
