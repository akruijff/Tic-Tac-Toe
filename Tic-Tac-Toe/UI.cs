namespace Tic_Tac_Toe
{
    public class UI
    {
        public static Request GetInput()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            return key switch
            {
                ConsoleKey.Escape => Request.Exit,
                ConsoleKey.W or ConsoleKey.UpArrow => Request.Up,
                ConsoleKey.S or ConsoleKey.DownArrow => Request.Down,
                ConsoleKey.A or ConsoleKey.LeftArrow => Request.Left,
                ConsoleKey.D or ConsoleKey.RightArrow => Request.Right,
                ConsoleKey.Enter or ConsoleKey.Spacebar => Request.Mark,
                _ => Request.UnupportedRequest,
            };
        }

        public static void DrawFullScreen(Cell[,] cells, int cursorX, int cursorY, string errorMessage, bool isPlayer1Turn)
        {
            Console.CursorVisible = false;
            UI.Draw(cells, cursorX, cursorY);
            Console.WriteLine("");
            Console.WriteLine("Esc = exit | Array keys or AWSD = move | Enter or space = mark cell");

            if (errorMessage.Length != 0)
                Console.WriteLine(errorMessage);
            else if (isPlayer1Turn)
                Console.WriteLine("Your move");
            else
                Console.WriteLine("Player 2 turn");

        }

        public static void DrawGameOver(Cell[,] cells, int cursorX, int cursorY, GameStatus status)
        {
            UI.Draw(cells, cursorX, cursorY);
            UI.DrawGameOverFooter(status switch
            {
                GameStatus.Pending => "You're aborted the game.",
                GameStatus.Player1_won => "Player 1 won!",
                GameStatus.Player2_won => "Player 2 won!",
                GameStatus.Draw => "It was a draw!",
                _ => throw new NotImplementedException()
            });
        }

        public static void Draw(Cell[,] cells, int cursorX, int cursorY)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic Tac Toe");
            Console.WriteLine("");
            Console.WriteLine("+-------+");
            for (int y = 0; y < Logic.HEIGHT; ++y)
            {
                Console.Write("|");
                for (int x = 0; x < Logic.WIDTH; ++x)
                {
                    Console.Write(" ");
                    DrawCell(cells[x, y], x == cursorX && y == cursorY);
                }
                Console.WriteLine(" |");
            }
            Console.WriteLine("+-------+");
            Console.WriteLine("");
        }

        private static void DrawCell(Cell cell, bool IsCursor)
        {
            ConsoleColor fg = Console.ForegroundColor;
            ConsoleColor bg = Console.BackgroundColor;
            if (IsCursor)
            {
                Console.ForegroundColor = bg;
                Console.BackgroundColor = fg;
            }
            switch (cell)
            {
                case Cell.Player1:
                    Console.Write("X");
                    break;
                case Cell.Player2:
                    Console.Write("O");
                    break;
                default:
                    Console.Write("#");
                    break;
            }
            if (IsCursor)
            {
                Console.ForegroundColor = fg;
                Console.BackgroundColor = bg;
            }
        }

        public static void DrawGameOverFooter(string s)
        {
            Console.WriteLine("GAME OVER");
            Console.WriteLine();
            Console.WriteLine(s);
        }
    }
}
