namespace Tic_Tac_Toe
{
    internal class View
    {
        /// <summary>
        /// Draws the board to the console.
        /// </summary>
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("Welkom to Tic Tac Toe");
            Console.WriteLine("");
            Console.WriteLine("+-------+");
            Console.WriteLine("| # # # |");
            Console.WriteLine("| # # # |");
            Console.WriteLine("| # # # |");
            Console.WriteLine("+-------+");
            Console.WriteLine("");
            Console.WriteLine("Your move");
        }
    }
}
