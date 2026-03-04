namespace Tic_Tac_Toe
{
    public class View
    {
        private const int width = 3, height = 3;
        private const int cursorX = 1, cursorY = 1;

        /// <summary>
        /// Draws the board and cursor to the console.
        /// </summary>
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("Welkom to Tic Tac Toe");
            Console.WriteLine("");
            Console.WriteLine("+-------+");
            for (int y = 0; y < height; ++y)
            {
                Console.Write("|");
                for (int x = 0; x < width; ++x)
                {
                    Console.Write(" ");
                    if (x == cursorX && y == cursorY)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.Write("#");
                    if (x == cursorX && y == cursorY)
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
            Console.WriteLine("Esc = exit");
        }
    }
}
