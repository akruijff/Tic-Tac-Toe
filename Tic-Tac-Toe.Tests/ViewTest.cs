namespace Tic_Tac_Toe.Tests
{
    public class ViewTest
    {
        [Fact]
        public void Test1()
        {
            StringWriter writer = new();
            View view = new(writer);
            view.draw();
            string[] expected =
            {
            "Tic Tac Toe"
            ""
            "+-------+"
            "| # # # |"
            "| # # # |"
            "| # # # |"
            "+-------+"
            "";
            "Your move.";

        }
    }
    }
}
