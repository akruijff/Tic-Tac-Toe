namespace Tic_Tac_Toe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Logic board = new();
            UI controller = new(board);
            controller.Start();
        }
    }
}
