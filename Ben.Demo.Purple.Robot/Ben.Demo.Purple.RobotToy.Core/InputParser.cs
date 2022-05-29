using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Demo.Purple.RobotToy.Core
{
    /// <summary>
    /// This classe parse the input from user.
    /// </summary>
    public class InputParser
    {
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
            
            return cmd;
        }

        /// <summary>
        /// Extracts the parameters from the user input and 
        /// returns PlaceCommandParameter object
        /// </summary>
        /// <param name="input">string[] with first item "PLACE" command and second item in the format "X,Y,F"</param>
        /// <param name="currentDirection">Robot current Direction object.</param>
        /// <param name="currentPosition">Robot current Position object. Can be null if not a valid Place issued.</param>
        /// <returns>PlaceCommandParameter object</returns>
        public PlaceCommandParameter ParseCommandParameter(string[] input, Direction currentDirection, Position currentPosition)
        {
            var placeCmdParam = new PlaceCommandParameterParser();     
            return placeCmdParam.ParseParameters(input, currentDirection, currentPosition);
        }     
    }
}