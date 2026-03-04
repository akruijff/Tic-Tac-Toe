namespace Tic_Tac_Toe
{
    public class View
    {
        private const int WIDTH = 3, HEIGHT = 3;
        public int CursorX 
        { 
            get;
            set => field = Math.Clamp(value, 0, WIDTH - 1);
        } = 1;
        public int CursorY
        {
            get;
            set => field = Math.Clamp(value, 0, HEIGHT - 1);
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
            for (int y = 0; y < HEIGHT; ++y)
            {
                Console.Write("|");
                for (int x = 0; x < WIDTH; ++x)
                {
                    Console.Write(" ");
                    if (x == CursorX && y == CursorY)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.Write("#");
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
            Console.WriteLine("Your move");
            Console.WriteLine("");
            Console.WriteLine("Esc = exit | Array keys or AWSD = move");
        }
    }
}
