namespace Ben.Demo.Purple.RobotToy.Core
{
    /// <summary>
    /// This class is for robot moving board. It has grid with row number and colum number.
    /// There is also a method for checking if the position of the toy is valid.
    /// </summary>
    public class ToyBoard
    {
        /// <summary>
        /// Store total rows and columns for the toy board.
        /// </summary>
        private int _rows;
        private int _columns;

        /// <summary>
        /// Constructor for the board for the grid with row number and column number.
        /// </summary>
        /// <param name="rows">Integer the number of rows for grid.</param>
        /// <param name="columns">Integer the number of columns for the grid.</param>
        public ToyBoard(int rows, int columns)
        {
            this._rows = rows;
            this._columns = columns;
        }

        /// <summary>
        /// Check if the robot placing position is valid.
        /// A valid position must be inside of Grid boundary.
        /// </summary>
        /// <param name="position">Position object for the Toy Robot.</param>
        /// <returns>True if it is inside the grid boundary else false.</returns>
        public bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X < _columns &&
                   position.Y >= 0 && position.Y < _rows;
        }
    }
}
