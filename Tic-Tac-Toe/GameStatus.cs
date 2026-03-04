namespace Tic_Tac_Toe
{
    /// <summary>
    /// Represents the current state of a Tic Tac Toe game.
    /// </summary>
    public enum GameStatus
    {
        /// <summary>The game is still in progress.</summary>
        Pending,

        /// <summary>The first player (typically 'X') has achieved three in a row.</summary>
        Player1_won,

        /// <summary>The second player (typically 'O') has achieved three in a row.</summary>
        Player2_won,
    }
}