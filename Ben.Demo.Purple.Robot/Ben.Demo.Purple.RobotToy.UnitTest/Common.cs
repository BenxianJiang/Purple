using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCore = Ben.Demo.Purple.RobotToy.Core;

namespace Ben.Demo.Purple.RobotToy.UnitTest
{
    /// <summary>
    /// Provide support function for the Unit test project.
    /// </summary>
    internal static class Common
    {
        /// <summary>
        /// Initialize the robot simulator used by many unit tests.
        /// </summary>
        /// <returns>Simulator object with toy board, input parser and robot objects initialized.</returns>
        public static MyCore.Simulator InitSimulator()
        {
            //Prepare Simulator for unit test methods.
            MyCore.ToyBoard toyBoard = new MyCore.ToyBoard(6, 6);
            MyCore.InputChecker inputParser = new MyCore.InputChecker(null, MyCore.Direction.South);
            MyCore.ToyRobot robot = new MyCore.ToyRobot();

            var simulator = new MyCore.Simulator(robot, toyBoard, inputParser);

            return simulator;
        }
    }
}
