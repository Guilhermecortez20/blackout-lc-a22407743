# BLACKOUT
## Authors
**Guilherme Negrinho**<br>
**Guilherme Cortez**
## Git Repository
https://github.com/bread-stealer/Blackout.git

## UML

```mermaid
classDiagram
    class Game {
        -int Size
        -int Moves
        -bool[,] Grid
        +Game(Difficulty difficulty)
        +bool IsCellOn(int row, int col)
        +void ToggleCell(int row, int col)
        +bool HasWon()
    }

    class GameView {
        +void RenderGrid(Game game, int selectorRow, int selectorCol)
        +ConsoleKey ReadKey()
        +Difficulty AskDifficulty()
        +void ShowWinMessage(int moves)
        +bool AskPlayAgain()
        -Color GetCellColor(Game game, int row, int col, int selectorRow, int selectorCol)
    }

    class Difficulty {
        <<enumeration>>
        Easy
        Medium
        Hard
    }

    class Program {
        +Main()
    }

    Program --> Game
    Program --> GameView
    GameView --> Game
    Game --> Difficulty
```
## References
- Spectre.Console Documentation - Canvas Widget. (n.d.). https://spectreconsole.net/console/widgets/canvas<br>
- Pinto, F. (2025). Introdução à Computação- Sintaxe Markdown.