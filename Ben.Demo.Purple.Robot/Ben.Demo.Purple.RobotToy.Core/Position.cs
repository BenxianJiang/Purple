using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Demo.Purple.RobotToy.Core
{
    /// <summary>
    /// This class represents the position of the toy on the board.
    /// </summary>
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Constructor to set the robot position.
        /// </summary>
        /// <param name="x">Integer column number on the board.</param>
        /// <param name="y">Integer row number on the board.</param>
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
