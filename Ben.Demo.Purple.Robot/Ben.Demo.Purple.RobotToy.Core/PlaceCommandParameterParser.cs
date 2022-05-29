using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Demo.Purple.RobotToy.Core
{
    /// <summary>
    /// This class checks and parses inpput parameters for "PLACE" command.
    /// </summary>
    public class PlaceCommandParameterParser
    {        
        //Max Number of parameters for "PLACE" Command.
        private const int _paramCount = 3;
        
        //Expected number of input items from user.
        private const int _cmdInputCount = 2;

        /// <summary>
        /// Form initial position and the direction it is going to be facing.
        /// Check user input parameters also.
        /// </summary>
        /// <param name="input">string[] the input contains two items - "PLACE" and "X,Y,F"</param>
        /// <param name="currentDirection">Direction the existing direction.</param>
        /// <param name="currentPosition">Position the existing position. It can be null if there is not a valid Place command issued before.</param>
        /// <returns>PlaceCommandParameter object with valid Position and Direction.</returns>
        /// <exception cref="ArgumentException">Exception object if the input does not pass the check.</exception>
        public PlaceCommandParameter ParseParameters(string[] input, Direction currentDirection, Position currentPosition)
        {
            Direction direction;
            Position position = null;

            //Checks that Place command and its valid command parameters, eg. "X,Y,F".
            if (input.Length != _cmdInputCount)
            {
                throw new ArgumentException(Constants.IncompletePlaceCommandText);
            }

            //Checks if three command parameters are provided for the PLACE command.   
            string[] commandParams = input[1].Split(',');

            //It must not be large than 3 and less than 2 parameters for Place command
            if(commandParams.Length > _paramCount || commandParams.Length < _paramCount -1)
            {
                throw new ArgumentException(Constants.IncompletePlaceCommandText);
            }

            //It must provide three parameters when it is the first PLACE command issued;
            if (commandParams.Length != _paramCount && currentPosition == null)
            {
                throw new ArgumentException(Constants.IncompletePlaceCommandText);
            }

            //Use the existing direction if it does not provide facing direction for consequent PLACE command
            if (commandParams.Length == _paramCount - 1)
            {
                if (currentPosition != null)
                {
                    //A valid PLACE issued before, so use the current Direction.
                    direction = currentDirection;
                }
                else
                {
                    throw new ArgumentException(Constants.IncompletePlaceCommandText);
                }
            }
            else
            {
                //using the provided direction in the parameter
                if (!Enum.TryParse(commandParams[commandParams.Length - 1], true, out direction))
                { 
                    throw new ArgumentException(Constants.InvalidDirectionText);
                }
            }

            //construct PlaceCommandParameter object.
            if(!int.TryParse(commandParams[0], out int x))
            {
                throw new ArgumentException(Constants.InvalidPlaceCommandXY);
            }

            if (!int.TryParse(commandParams[1], out int y))
            {
                throw new ArgumentException(Constants.InvalidPlaceCommandXY);
            }

            position = new Position(x, y);

            return new PlaceCommandParameter(position, direction);
        }
    }
}
