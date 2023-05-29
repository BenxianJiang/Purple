using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Demo.Purple.RobotToy.Core
{
    /// <summary>
    /// Simulate the movement for Toy Robot.
    /// This class is used for processing different commands entered by user.
    /// </summary>
    public class Simulator
    {
        public IToyRobot ToyRobot { get; private set; }
        public ToyBoard ToyBoard { get; private set; }
        public InputChecker InputChecker { get; private set; }

        /// <summary>
        /// Constructor. Initialize Simulator object.
        /// </summary>
        /// <param name="toyRobot">ToyRobot object.</param>
        /// <param name="ToyBoard">ToyBoard object.</param>
        /// <param name="inputParser">InputParser object for checking input parameters.</param>
        public Simulator(IToyRobot toyRobot, ToyBoard toyBoard, InputChecker inputChecker)
        {
            ToyRobot = toyRobot;
            ToyBoard = toyBoard;
            InputChecker = inputChecker;
        }

        /// <summary>
        /// Process Robots different commands.
        /// </summary>
        /// <param name="input">Input items conatins Command text and parameters text.</param>
        /// <returns>String for current status when process REPORT command. Otherwise it return empty string.</returns>
        public string ProcessCommand(string[] input)
        {
            Command cmd = InputChecker.ParseCommand(input);

            //return immediately if first Place command has not been issued.
            if (cmd != Command.Place && ToyRobot.Position == null) return string.Empty;

            switch (cmd)
            {
                case Command.Place:
                    //get current Position and Direction and parse the input text.
                    InputChecker.Direction = ToyRobot.Direction;
                    InputChecker.Position = ToyRobot.Position;

                    //parse the new Position and Direction
                    InputChecker.ParsePlaceCommandParameters(input);

                    //set Robot to new position and direction if it is valid.
                    if (ToyBoard.IsValidPosition(InputChecker.Position))
                    {
                        ToyRobot.Place(InputChecker.Position, InputChecker.Direction);
                    }
                    else
                    {
                        throw new ArgumentException(Constants.PlaceRobotOutsideBoundaryText);
                    }

                    break;

                case Command.Move:
                    var newPosition = ToyRobot.Move();

                    //Move only when inside the board boundary.
                    if (ToyBoard.IsValidPosition(newPosition))
                    {
                        ToyRobot.Position = newPosition;
                    }
                    else
                    {
                        throw new ArgumentException(Constants.MoveRobotOutsideBoundaryText);
                    }

                    break;

                case Command.Left:
                    ToyRobot.RotateLeft();
                    break;

                case Command.Right:
                    ToyRobot.RotateRight();
                    break;

                case Command.Report:
                    return GetReport();
            }

            return string.Empty;
        }

        /// <summary>
        /// Report the current status.
        /// </summary>
        /// <returns>String in the format "Output: X,Y,F"</returns>
        public string GetReport()
        {
            return string.Format("Output: {0},{1},{2}", ToyRobot.Position.X,
                ToyRobot.Position.Y, ToyRobot.Direction.ToString().ToUpper());
        }
    }
}
