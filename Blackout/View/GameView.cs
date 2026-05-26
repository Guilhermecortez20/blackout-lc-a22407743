using Spectre.Console;
using Blackout.Model;

namespace Blackout.View
{
    public class GameView
    {
        private const string ColorOff = "#6AA84F";
        private const string ColorOn = "#B6D7A8";
        private const string ColorSelected = "#FFE28C";

        public void RenderGrid(Game game, int selectorRow, int selectorCol)
        {
            AnsiConsole.Clear();

            for (int row = 0; row < game.Size; row++)
            {
                for (int col = 0; col < game.Size; col++)
                {
                    string color = GetCellColor(game, row, col, selectorRow, selectorCol);
                    AnsiConsole.Markup($"[{color}]████[/] ");
                }
                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine();
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[grey]Moves: {game.Moves}[/]");
        }

        public Difficulty AskDifficulty()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]BLACKOUT[/]");
            AnsiConsole.WriteLine();

            string choice = AnsiConsole.Prompt
            (
                new SelectionPrompt<string>()
                    .Title("Choose a difficulty:")
                    .AddChoices("Easy (3x3)", "Medium (5x5)", "Hard (8x8)")
            );

            switch (choice)
            {
                case "Easy (3x3)": return Difficulty.Easy;
                case "Medium (5x5)": return Difficulty.Medium;
                default: return Difficulty.Hard;
            }
        }

        public void ShowWinMessage(int moves)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold green]You won in {moves} moves![/]");
        }

        public bool AskPlayAgain()
        {
            return AnsiConsole.Confirm("Play again?");
        }

        private string GetCellColor(Game game, int row, int col, int selectorRow, int selectorCol)
        {
            if (row == selectorRow && col == selectorCol)
                return ColorSelected;
            return game.IsCellOn(row, col) ? ColorOn : ColorOff;
        }
    }
}