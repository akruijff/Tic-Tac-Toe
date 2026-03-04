namespace Tic_Tac_Toe
{
    public class View(Board board)
    {
        public bool IsPlayer1Turn = true;
        public int CursorX 
        { 
            get;
            set => field = Math.Clamp(value, 0, Board.WIDTH - 1);
        } = 1;
        public int CursorY
        {
            get;
            set => field = Math.Clamp(value, 0, Board.HEIGHT - 1);
        } = 1;

        /// <summary>
        /// Draws the board and cursor to the console.
        /// </summary>
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("Welkom to Tic Tac Toe");
            Console.WriteLine("");
            Console.WriteLine("+-------+");
            for (int y = 0; y < Board.HEIGHT; ++y)
            {
                Console.Write("|");
                for (int x = 0; x < Board.WIDTH; ++x)
                {
                    Console.Write(" ");
                    if (x == CursorX && y == CursorY)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    switch(board[x, y])
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
                    if (x == CursorX && y == CursorY)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                Console.WriteLine(" |");
            }
            Console.WriteLine("+-------+");
            Console.WriteLine("");
            if (IsPlayer1Turn)
                Console.WriteLine("Your move");
            else
                Console.WriteLine("Player 2 turn");
            Console.WriteLine("");
            Console.WriteLine("Esc = exit | Array keys or AWSD = move | Enter or space = mark cell");
        }
    }
}
