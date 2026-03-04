namespace Tic_Tac_Toe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            View view = new();
            Controller controller = new();

            view.Draw();
            controller.Start();
        }
    }
}
