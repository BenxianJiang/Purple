using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Demo.Purple.RobotToy.Core
{
    /// <summary>
    /// Store Position and Direction for the parameters for the "PLACE" command.
    /// </summary>
    public class PlaceCommandParameter
    {
        public Position Position { get; set; }
        public Direction Direction { get; set; }

        /// <summary>
        /// Constructor for object PlaceCommandParameter.
        /// </summary>
        /// <param name="position">Position object.</param>
        /// <param name="direction">Direction object.</param>
        public PlaceCommandParameter(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}
