namespace Tic_Tac_Toe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Board board = new();
            View view = new(board);
            Controller controller = new(board, view);
            controller.Start();
        }
    }
}
