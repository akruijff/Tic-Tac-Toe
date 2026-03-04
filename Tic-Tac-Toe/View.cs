namespace Tic_Tac_Toe
{
    public class View(Board board)
    {
        /// <summary>
        /// Indicates whether it is currently Player 1's turn.
        /// </summary>
        public bool IsPlayer1Turn = true;

        /// <summary>
        /// Gets or sets the horizontal cursor position, clamped between 0 and the board's <see cref="Board.WIDTH"/> - 1.
        /// </summary>
        public int CursorX 
        { 
            get;
            set => field = Math.Clamp(value, 0, Board.WIDTH - 1);
        } = 1;

        /// <summary>
        /// Gets or sets the vertical cursor position, clamped between 0 and the board's <see cref="Board.HEIGHT"/> - 1.
        /// </summary>
        public int CursorY
        {
            get;
            set => field = Math.Clamp(value, 0, Board.HEIGHT - 1);
        } = 1;

        /// <summary>
        /// Gets or sets an error message to be displayed below the board (e.g., for invalid moves).
        /// </summary>
        public string ErrorMessage { get; internal set; } = "";

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
            if (ErrorMessage.Length != 0)
                Console.WriteLine(ErrorMessage);
            else if (IsPlayer1Turn)
                Console.WriteLine("Your move");
            else
                Console.WriteLine("Player 2 turn");
            Console.WriteLine("");
            Console.WriteLine("Esc = exit | Array keys or AWSD = move | Enter or space = mark cell");
        }
    }
}
