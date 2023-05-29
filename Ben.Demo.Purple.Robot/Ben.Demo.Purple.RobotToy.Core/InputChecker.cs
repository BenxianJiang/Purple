using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Demo.Purple.RobotToy.Core
{
    public class InputChecker
    {
        //Max Number of parameters for "PLACE" Command.
        private const int _placeParamCount = 3;
        //Expected number of input items from user.
        private const int _cmdInputCount = 2;

        public Position Position { get; set; }
        public Direction Direction { get; set; }

        /// <summary>
        /// Constructor for object InputChecker.
        /// </summary>
        /// <param name="position">Position object.</param>
        /// <param name="direction">Direction object.</param>
        public InputChecker(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        /// <summary>
        /// get the command from input parameter
        /// </summary>
        /// <param name="input">string[] with the first item contains the command text, eg. one of PLACE|MOVE|LEFT|RIGHT|REPORT|EXIT.</param>
        /// <returns>A valid command.</returns>
        /// <exception cref="ArgumentException">Exception object if the commmand is not a valid Command.</exception>
        public Command ParseCommand(string[] input)
        {
            Command cmd;

            //check if valid command
            if (!Enum.TryParse(input[0], true, out cmd))
            {
                throw new ArgumentException(Constants.InvalidCommandText);
            }

            //must issue valid Place command first
            if (cmd != Command.Place)
            {
                if (Position == null)
                {
                    throw new ArgumentException(Constants.IssueFirstValidPlaceCommandText);
                }
                else
                {
                    if(Position.X < 0 || Position.X > 5 || Position.Y < 0 || Position.Y > 5)
                    {
                        throw new ArgumentException(Constants.IssueFirstValidPlaceCommandText);
                    }
                }
            }

            return cmd;
        }

        /// <summary>
        /// Parse Place command with its new position and the direction by examining the parameters.
        /// If the place command is valid one, the object will contain new Direction and Position is 
        /// </summary>
        /// <param name="input">string[] the input contains two items - "PLACE" and "X,Y,F"</param>
        /// <exception cref="ArgumentException">Exception object if the input does not pass the check.</exception>
        public void ParsePlaceCommandParameters(string[] input)
        {
            //use existing direction if user not provide
            Direction direction = Direction;

            //Checks that Place command and its valid command parameters, eg. "X,Y,F".
            if (input.Length != _cmdInputCount)
            {
                throw new ArgumentException(Constants.IncompletePlaceCommandText);
            }

            //Checks if three command parameters are provided for the PLACE command.   
            string[] commandParams = input[1].Split(',');

            //It must not be large than 3 and less than 2 parameters for Place command
            if (commandParams.Length > _placeParamCount || commandParams.Length < _placeParamCount - 1)
            {
                throw new ArgumentException(Constants.IncompletePlaceCommandText);
            }

            //It must provide three parameters when it is the first PLACE command issued;
            if (commandParams.Length != _placeParamCount && Position == null)
            {
                throw new ArgumentException(Constants.IncompletePlaceCommandText);
            }

            //Use the existing direction if it does not provide facing direction for consequent PLACE command
            if (commandParams.Length == _placeParamCount - 1)
            {
                //a valid Place has not yet issued.
                if (Position == null)
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
            if (!int.TryParse(commandParams[0], out int x))
            {
                throw new ArgumentException(Constants.InvalidPlaceCommandXY);
            }

            if (!int.TryParse(commandParams[1], out int y))
            {
                throw new ArgumentException(Constants.InvalidPlaceCommandXY);
            }

            //assign the new position and diction
            Position = new Position(x, y);           
            Direction = direction;
        }
    }
}
