using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCore = Ben.Demo.Purple.RobotToy.Core;

namespace Ben.Demo.Purple.RobotToy.UnitTest
{
    /// <summary>
    /// Class for testing process command in Simulator object
    /// </summary>
    [TestClass]
    public class UnitTestSimulator
    {
        /// <summary>
        /// Test a invalid command "GO"
        /// </summary>
        [TestMethod]
        public void TestSimulatorInvalidCommand()
        {
            //Prepare Simulator
            MyCore.ToyBoard board = new MyCore.ToyBoard(6, 6);
            MyCore.InputChecker inputParser = new MyCore.InputChecker(null, MyCore.Direction.South);
            MyCore.ToyRobot robot = new MyCore.ToyRobot();

            var simulator = new MyCore.Simulator(robot, board, inputParser);

            //Get result for command "GO".
            var input = "GO 1,4,EAST".Split(' ');
            var exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.InvalidCommandText), "Test fails for command 'GO' in TestSimulatorInvalidCommand!");
        }

        /// <summary>
        /// Test a valid PLACE command.
        /// </summary>
        [TestMethod]
        public void TestSimulatorValidPlaceCommand()
        {
            //Prepare Simulator
            MyCore.ToyBoard board = new MyCore.ToyBoard(6, 6);
            MyCore.InputChecker inputParser = new MyCore.InputChecker(null, MyCore.Direction.South);
            MyCore.ToyRobot robot = new MyCore.ToyRobot();

            var simulator = new MyCore.Simulator(robot, board, inputParser);

            //Start
            simulator.ProcessCommand("PLACE 1,4,EAST".Split(' '));

            //Check
            Assert.AreEqual(1, robot.Position.X, "Test fails - invalid X(column) in TestSimulatorValidPlaceCommand!");
            Assert.AreEqual(4, robot.Position.Y, "Test fails - invalid Y(row) in TestSimulatorValidPlaceCommand!");
            Assert.AreEqual(MyCore.Direction.East, robot.Direction, "Test fails - wrong Direction in TestSimulatorValidPlaceCommand!");
        }

        /// <summary>
        /// Test simulator multiple place commands
        /// </summary>
        [TestMethod]
        public void TestSimulatorMutiplePlaceCommand()
        {
            //Prepare Simulator
            MyCore.ToyBoard toyBoard = new MyCore.ToyBoard(6, 6);
            MyCore.InputChecker inputParser = new MyCore.InputChecker(null, MyCore.Direction.South);
            MyCore.ToyRobot robot = new MyCore.ToyRobot();

            var simulator = new MyCore.Simulator(robot, toyBoard, inputParser);

            //Start
            simulator.ProcessCommand("PLACE 1,4,SOUTH".Split(' '));
            //Second Place command
            simulator.ProcessCommand("PLACE 2,3,WEST".Split(' '));

            //Check
            Assert.AreEqual(2, robot.Position.X, "Test fails - invalid X(column) in TestSimulatorMutiplePlaceCommand!");
            Assert.AreEqual(3, robot.Position.Y, "Test fails - invalid Y(row) in TestSimulatorMutiplePlaceCommand!");
            Assert.AreEqual(MyCore.Direction.West, robot.Direction, "Test fails - wrong Direction in TestSimulatorMutiplePlaceCommand!");
        }

        /// <summary>
        /// Test simulator subsequent Place command without Direction text provided.
        /// </summary>
        [TestMethod]
        public void TestSimulatorSubsequentPlaceCommandWithoutDirection()
        {
            //Prepare Simulator
            MyCore.ToyBoard toyBoard = new MyCore.ToyBoard(6, 6);
            MyCore.InputChecker inputParser = new MyCore.InputChecker(null, MyCore.Direction.South);
            MyCore.ToyRobot robot = new MyCore.ToyRobot();

            var simulator = new MyCore.Simulator(robot, toyBoard, inputParser);

            //Start
            simulator.ProcessCommand("PLACE 5,4,SOUTH".Split(' '));
            //Second Place command without direction - it defaults to the previous Place command direction.
            simulator.ProcessCommand("PLACE 3,1".Split(' '));

            //Check
            Assert.AreEqual(3, robot.Position.X, "Test fails - invalid X(column) in TestSimulatorSubsequentPlaceCommandWithoutDirection!");
            Assert.AreEqual(1, robot.Position.Y, "Test fails - invalid Y(row) in TestSimulatorSubsequentPlaceCommandWithoutDirection!");
            Assert.AreEqual(MyCore.Direction.South, robot.Direction, "Test fails - wrong Direction in TestSimulatorSubsequentPlaceCommandWithoutDirection!");
        }

        /// <summary>
        /// Test Place command places outside the Toy board boundary.
        /// </summary>
        [TestMethod]
        public void TestSimulatorPlaceCommandOutsideBoundary()
        {
            //Prepare Simulator
            MyCore.ToyBoard toyBoard = new MyCore.ToyBoard(6, 6);
            MyCore.InputChecker inputParser = new MyCore.InputChecker(null, MyCore.Direction.South);
            MyCore.ToyRobot robot = new MyCore.ToyRobot();

            var simulator = new MyCore.Simulator(robot, toyBoard, inputParser);

            //Start to put Place outside boundary.
            var exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand("PLACE 6,0,EAST".Split(' ')); });

            //Check - it shall not set the position.
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.PlaceRobotOutsideBoundaryText), "Test fails in TestSimulatorPlaceCommandOutsideBoundary!");
        }

        /// <summary>
        /// Test first Place command without Direction text provided.
        /// </summary>
        [TestMethod]
        public void TestSimulatorFirstPlaceCommandWithoutDirection()
        {
            //Prepare Simulator
            MyCore.ToyBoard toyBoard = new MyCore.ToyBoard(6, 6);
            MyCore.InputChecker inputParser = new MyCore.InputChecker(null, MyCore.Direction.South);
            MyCore.ToyRobot robot = new MyCore.ToyRobot();

            var simulator = new MyCore.Simulator(robot, toyBoard, inputParser);

            //Get result for first Place command without direction.
            var input = "PLACE 1,4".Split(' ');
            var exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.IncompletePlaceCommandText), "Test fails for first Place command in TestSimulatorFirstPlaceCommandWithoutDirection!");
        }

        /// <summary>
        /// Test other valid commands 'MOVE', 'LEFT' and 'RIGHT' without first Place command issued.
        /// </summary>
        [TestMethod]
        public void TestSimulatorCommandsWithoutPlaceCommandFirst()
        {
            //Prepare Simulator
            MyCore.ToyBoard toyBoard = new MyCore.ToyBoard(6, 6);
            MyCore.InputChecker inputParser = new MyCore.InputChecker(null, MyCore.Direction.South);
            MyCore.ToyRobot robot = new MyCore.ToyRobot();

            var simulator = new MyCore.Simulator(robot, toyBoard, inputParser);

            //Get result
            var input = "MOVE".Split(' ');
            var exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.IssueFirstValidPlaceCommandText), "Test fails for command 'MOVE' without first Place command in TestSimulatorCommandsWithoutPlaceCommandFirst!");

            //Get result
            input = "LEFT".Split(' ');
            exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.IssueFirstValidPlaceCommandText), "Test fails for command 'LEFT' without first Place command in TestSimulatorCommandsWithoutPlaceCommandFirst!");

            //Get result
            input = "RIGHT".Split(' ');
            exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.IssueFirstValidPlaceCommandText), "Test fails for command 'RIGHT' without first Place command in TestSimulatorCommandsWithoutPlaceCommandFirst!");
        }

        /// <summary>
        /// Test MOVE command without change direction inside boundaries.
        /// </summary>
        [TestMethod]
        public void TestSimulatorMoveInsideBoundariesWithoutChangeDirection()
        {
            //Prepare Simulator
            var simulator = Common.InitSimulator();

            //Start Process
            simulator.ProcessCommand("PLACE 1,3,EAST".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            //Check the result
            string expected = "Output: 3,3,EAST";
            string actual = simulator.GetReport();
            Assert.AreEqual(actual, expected, "Test fails in TestSimulatorMoveInsideBoundariesWithoutChangeDirection!");
        }

        /// <summary>
        /// Test Move to Left
        /// </summary>
        [TestMethod]
        public void TestSimulatorMoveLeft()
        {
            //Prepare
            var simulator = Common.InitSimulator();

            // Start
            simulator.ProcessCommand("PLACE 1,3,NORTH".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            //Check
            string expected = "Output: 0,5,WEST";
            string actual = simulator.GetReport();
            Assert.AreEqual(actual, expected, "Test fails in TestSimulatorMoveLeft!");
        }

        /// <summary>
        /// Test MOVE to Right
        /// </summary>
        [TestMethod]
        public void TestSimulatorMoveRight()
        {
            //Prepare
            var simulator = Common.InitSimulator();

            //Start
            simulator.ProcessCommand("PLACE 2,3,WEST".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("RIGHT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            //Check
            string expected = "Output: 1,4,NORTH";
            string actual = simulator.GetReport();
            Assert.AreEqual(actual, expected, "Test fails in TestSimulatorMoveRight!");
        }

        /// <summary>
        /// Test Move Left and Move to right.
        /// </summary>
        [TestMethod]
        public void TestSimulatorMoveLeftThenRight()
        {
            //Prepare
            var simulator = Common.InitSimulator();

            //Start - It shall ignore MOVE command when Robot goes out of the Board boundaries.
            simulator.ProcessCommand("PLACE 2,3,SOUTH".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("RIGHT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            //Check
            string expected = "Output: 3,1,SOUTH";
            string actual = simulator.GetReport();
            Assert.AreEqual(actual, expected, "Test fails in TestSimulatorMoveLeftThenRight!");
        }

        /// <summary>
        /// Test Move Right and then Left.
        /// </summary>
        [TestMethod]
        public void TestSimulatorMoveRightThenLeft()
        {
            //Prepare
            var simulator = Common.InitSimulator();

            //Start
            simulator.ProcessCommand("PLACE 1,3,EAST".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("RIGHT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            //Check
            string expected = "Output: 3,1,EAST";
            string actual = simulator.GetReport();
            Assert.AreEqual(actual, expected, "Test fails in TestSimulatorMoveRightThenLeft!");
        }

        /// <summary>
        /// Test MOVE command toward outside boundaries when robot place NORTH, WEST, SOUTH and EAST boundary grid.
        /// </summary>
        [TestMethod]
        public void TestSimulatorMoveOutsideBoundaries()
        {
            //Prepare Simulator
            var simulator = Common.InitSimulator();

            //Start
            //put Robot on NORTH boundary grid
            simulator.ProcessCommand("PLACE 1,5,NORTH".Split(' '));
            //Move toward outside boundary
            //simulator.ProcessCommand("MOVE".Split(' '));
            var exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand("MOVE".Split(' ')); });

            //Check - the robot shall stay on the boundary grid.
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.MoveRobotOutsideBoundaryText), "Test fails Move to outside 'North' boundary in TestSimulatorMoveOutsideBoundaries!");

            //put Robot on WEST boundary grid
            simulator.ProcessCommand("PLACE 0,3,WEST".Split(' '));
            //Move toward outside boundary
            exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand("MOVE".Split(' ')); });

            //Check - the robot shall stay on the boundary grid.
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.MoveRobotOutsideBoundaryText), "Test fails Move to outside 'West' boundary in TestSimulatorMoveOutsideBoundaries!");

            //put Robot on SOUTH boundary grid
            simulator.ProcessCommand("PLACE 4,0,SOUTH".Split(' '));
            //Move toward outside boundary
            exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand("MOVE".Split(' ')); });

            //Check - the robot shall stay on the boundary grid.
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.MoveRobotOutsideBoundaryText), "Test fails Move to outside 'South' boundary in TestSimulatorMoveOutsideBoundaries!");

            //put Robot on EAST boundary grid
            simulator.ProcessCommand("PLACE 5,2,EAST".Split(' '));
            //Move toward outside boundary
            exception = Assert.ThrowsException<ArgumentException>(delegate { simulator.ProcessCommand("MOVE".Split(' ')); });

            //Check - the robot shall stay on the boundary grid.
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.MoveRobotOutsideBoundaryText), "Test fails Move to outside 'East' boundary in TestSimulatorMoveOutsideBoundaries!");
        }

        /// <summary>
        /// Test robot rotates before Move.
        /// </summary>
        [TestMethod]
        public void TestSimulatorRotateBeforeMove()
        {
            //Prepare
            var simulator = Common.InitSimulator();

            //Start
            simulator.ProcessCommand("PLACE 2,3,NORTH".Split(' '));
            simulator.ProcessCommand("RIGHT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            //Check
            string expected = "Output: 3,3,WEST";
            string actual = simulator.GetReport();
            Assert.AreEqual(actual, expected, "Test fails in TestSimulatorRotateBeforeMove!");
        }

        /// <summary>
        /// Test robot returns the starting point after 4 Left turns.
        /// </summary>
        [TestMethod]
        public void TestSimulatorReturnStartPointByLeftCommands()
        {
            //Prepare
            var simulator = Common.InitSimulator();

            //Start
            simulator.ProcessCommand("PLACE 2,3,NORTH".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("LEFT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            //Check
            string expected = "Output: 2,3,NORTH";
            string actual = simulator.GetReport();
            Assert.AreEqual(actual, expected, "Test fails in TestSimulatorReturnStartPointByLeftCommands!");
        }

        /// <summary>
        /// Test robot returns the starting point after 4 Right turns.
        /// </summary>
        [TestMethod]
        public void TestSimulatorReturnStartPointByRightCommands()
        {
            //Prepare
            var simulator = Common.InitSimulator();

            //Start 
            simulator.ProcessCommand("PLACE 4,3,EAST".Split(' '));
            simulator.ProcessCommand("RIGHT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("RIGHT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("RIGHT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));
            simulator.ProcessCommand("RIGHT".Split(' '));
            simulator.ProcessCommand("MOVE".Split(' '));

            //Check
            string expected = "Output: 4,3,EAST";
            string actual = simulator.GetReport();
            Assert.AreEqual(actual, expected, "Test fails in TestSimulatorReturnStartPointByRightCommands!");
        }
    }
}

