namespace Tic_Tac_Toe
{
    /// <summary>
    /// Represents the occupant of a single cell on the Tic Tac Toe board.
    /// </summary>
    public enum Cell
    {
        /// <summary>The cell is occupied by Player 1 (typically 'X').</summary>
        Player1,

        /// <summary>The cell is occupied by Player 2 (typically 'O').</summary>
        Player2,

        /// <summary>The cell is currently empty and available to be taken.</summary>
        Untaken,
    }
}
