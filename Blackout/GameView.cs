using Spectre.Console;
using System;

namespace Blackout
{
    /// <summary>
    /// Represents the View in the MVC pattern.
    /// Handles all console rendering and user input using Spectre.Console.
    /// </summary>
    public class GameView : IGameView
    {
        private static readonly Color ColorOff = new Color(106, 168, 79);
        private static readonly Color ColorOn = new Color(182, 215, 168);
        private static readonly Color ColorSelected = new Color(255, 226, 140);
        private bool _firstRender = true;

        /// <summary>
        /// Displays the main menu and returns whether the player chose to play.
        /// </summary>
        /// <returns>True if the player chose Play, false if Quit.</returns>
        public bool ShowMainMenu()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("BLACKOUT")
                    .Color(Color.Green1)
            );
            AnsiConsole.MarkupLine("[italic rgb(0,255,0)]The lights are on. Can you turn them all off?[/]");
            AnsiConsole.WriteLine();

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(foreground: Color.Green1))
                    .AddChoices("Play", "Quit")
            );

            return choice == "Play";
        }

        /// <summary>
        /// Displays the difficulty selection menu and returns the chosen difficulty.
        /// </summary>
        /// <returns>The <see cref="Difficulty"/> chosen by the player.</returns>
        public Difficulty AskDifficulty()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("BLACKOUT")
                    .Color(Color.Green1)
            );
            AnsiConsole.MarkupLine("[italic rgb(0,255,0)]How bold are you feeling?[/]");
            AnsiConsole.WriteLine();

            string choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(foreground: Color.Green1))
                    .AddChoices("Easy (3x3)", "Medium (5x5)", "Hard (8x8)")
            );

            switch (choice)
            {
                case "Easy (3x3)": return Difficulty.Easy;
                case "Medium (5x5)": return Difficulty.Medium;
                default: return Difficulty.Hard;
            }
        }

        /// <summary>
        /// Displays the instructions screen and waits for the player to press a key.
        /// </summary>
        public void ShowInstructions()
        {
            _firstRender = true;
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("BLACKOUT")
                    .Color(Color.Green1)
            );
            AnsiConsole.MarkupLine("[italic rgb(0,255,0)]How to Play[/]");
            AnsiConsole.MarkupLine("[rgb(42,74,50)]────────[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[rgb(0,255,0)]Controls[/]");
            AnsiConsole.MarkupLine("[rgb(200,240,216)]↑ ↓ ← →[/] [rgb(124,177,145)]Move selector[/]");
            AnsiConsole.MarkupLine("[rgb(200,240,216)]Enter  [/] [rgb(124,177,145)]Click selected cell[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[rgb(0,255,0)]Objective[/]");
            AnsiConsole.MarkupLine("[rgb(124,177,145)]Turn all cells OFF to win.[/]");
            AnsiConsole.MarkupLine("[rgb(124,177,145)]Turning a cell on or off also flips its adjacent cells.[/]");
            AnsiConsole.WriteLine();
            AnsiConsole.Markup("[rgb(42,74,50)]Press any key to start...[/]");
            Console.ReadKey(intercept: true);
        }

        /// <summary>
        /// Renders the current state of the grid using a Canvas, highlighting the selected cell.
        /// Clears the screen on the first render, then repositions the cursor for subsequent frames to avoid flickering.
        /// </summary>
        /// <param name="game">The game model to read cell states from.</param>
        /// <param name="selectorRow">The row index of the currently selected cell.</param>
        /// <param name="selectorCol">The column index of the currently selected cell.</param>
        public void RenderGrid(Game game, int selectorRow, int selectorCol)
        {
            Console.CursorVisible = false;

            if (_firstRender)
            {
                AnsiConsole.Clear();
                _firstRender = false;
            }
            else
            {
                Console.SetCursorPosition(0,0);
            }

            int scale = 5;

            Canvas canvas = new Canvas(game.Size * scale, game.Size *scale);

            for (int row = 0; row < game.Size; row++)
            {
                for (int col = 0; col < game.Size; col++)
                {
                    Color color = GetCellColor(game, row, col, selectorRow, selectorCol);
                    
                    for (int dy = 0; dy < scale; dy++)
                    {
                        for (int dx = 0; dx < scale; dx++)
                        {
                            canvas.SetPixel(col * scale + dx, row * scale + dy, color);
                        }
                    }
                }
            }
            canvas.Scale = false;
            AnsiConsole.Write(canvas);
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[grey]Moves: {game.Moves}[/]");
        }

        /// <summary>
        /// Reads a single key press from the player without displaying it.
        /// </summary>
        /// <returns>The <see cref="ConsoleKey"/> pressed by the player.</returns>
        public ConsoleKey ReadKey() => Console.ReadKey(intercept: true).Key;

        /// <summary>
        /// Displays the win message with the number of moves taken.
        /// </summary>
        /// <param name="moves">The number of moves the player took to win.</param>
        public void ShowWinMessage(int moves)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold green]You won in {moves} moves![/]");
        }

        /// <summary>
        /// Asks the player if they want to play again.
        /// </summary>
        /// <returns>True if the player wants to play again, false otherwise.</returns>
        public bool AskPlayAgain() => AnsiConsole.Confirm("Play again?");

        /// <summary>
        /// Returns the display color for a cell based on its state and whether it is selected.
        /// </summary>
        /// <param name="game">The game model to read the cell state from.</param>
        /// <param name="row">The row index of the cell.</param>
        /// <param name="col">The column index of the cell.</param>
        /// <param name="selectorRow">The row index of the currently selected cell.</param>
        /// <param name="selectorCol">The column index of the currently selected cell.</param>
        /// <returns>A <see cref="Color"/> representing the cell's display color.</returns>
        private Color GetCellColor(Game game, int row, int col, int selectorRow, int selectorCol)
        {
            if (row == selectorRow && col == selectorCol)
                return ColorSelected;
            return game.IsCellOn(row, col) ? ColorOn : ColorOff;
        }
    }
}