using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCore = Ben.Demo.Purple.RobotToy.Core;

namespace Ben.Demo.Purple.RobotToy.UnitTest
{
    /// <summary>
    /// Test for Toy Robot class
    /// </summary>
    [TestClass]
    public class UnitTestToyRobot
    {
        /// <summary>
        /// Test Robot Rotation.
        /// </summary>
        [TestMethod]
        public void TestRobotRotation()
        {
            //Init Robot
            MyCore.ToyRobot robot = new MyCore.ToyRobot()
            {
                Direction = MyCore.Direction.South,
                Position = new MyCore.Position(3, 4)
            };

            //Run left rotation
            robot.Rotate(3);

            //Check
            MyCore.Direction expected = MyCore.Direction.East;
            MyCore.Direction actual = robot.Direction;
            Assert.AreEqual(actual, expected, "Direction wrong! Test fails when Toy Left Rotation! Fail in TestRobotRotation.");

            //Run right rotation
            robot.Rotate(-7);

            //Check
            expected = MyCore.Direction.West;
            actual = robot.Direction;
            Assert.AreEqual(actual, expected, "Direction wrong! Test fails when Toy Right Rotation! Fail in TestRobotRotation.");
        }

        /// <summary>
        /// Test when Toy Robot turn Left.
        /// </summary>
        [TestMethod]
        public void TestRobotTurnLeft()
        {
            //Init Robot
            MyCore.ToyRobot robot = new MyCore.ToyRobot() 
            { 
                Direction = MyCore.Direction.West, 
                Position = new MyCore.Position(2, 3) 
            };

            //Run
            robot.RotateLeft();

            //Check
            Assert.AreEqual(MyCore.Direction.South, robot.Direction, "Direction wrong! Test fails when Toy turn Left! Fail in TestRobotTurnLeft.");
            Assert.AreEqual(2, robot.Position.X, "Position X wrong! Test fails when Toy turn Left! Fail in TestRobotTurnLeft.");
            Assert.AreEqual(3, robot.Position.Y, "Position Y wrong! Test fails when Toy turn Left! Fail in TestRobotTurnLeft.");
        }

        /// <summary>
        /// Test when Robot turn Right.
        /// </summary>
        [TestMethod]
        public void TestRobotTurnRight()
        {
            //Init Robot
            MyCore.ToyRobot robot = new MyCore.ToyRobot()
            {
                Direction = MyCore.Direction.North,
                Position = new MyCore.Position(4, 1)
            };

            //Run
            robot.RotateRight();

            //Check
            Assert.AreEqual(MyCore.Direction.East, robot.Direction, "Direction wrong! Test fails when Toy turn Left! Fail in TestRobotTurnRight.");
            Assert.AreEqual(4, robot.Position.X, "Position X wrong! Test fails when Toy turn Left! Fail in TestRobotTurnRight.");
            Assert.AreEqual(1, robot.Position.Y, "Position Y wrong! Test fails when Toy turn Left! Fail in TestRobotTurnRight.");
        }

        /// <summary>
        /// Test when Robot Move, eg. Get Next/New Robot Position.
        /// </summary>
        [TestMethod]
        public void TestRobotMove()
        {
            //Init Robot
            MyCore.ToyRobot robot = new MyCore.ToyRobot()
            {
                Direction = MyCore.Direction.West,
                Position = new MyCore.Position(2, 4)
            };

            //Run
            var position = robot.Move();

            //Check
            Assert.AreEqual(1, position.X, "Test fails for Move Position X in TestRobotMove!");
            Assert.AreEqual(4, position.Y, "Test fails for Move Position Y in TestRobotMove!");
        }

        /// <summary>
        /// Test set Robot position and direction
        /// </summary>
        [TestMethod]
        public void TestRobotPositionAndDirection()
        {
            //Init Robot and Position
            var position = new MyCore.Position(2, 3);
            var robot = new MyCore.ToyRobot();

            //Run
            robot.Place(position, MyCore.Direction.East);

            //Check
            Assert.AreEqual(2, robot.Position.X, "Test fails for Robot Position X in TestRobotPositionAndDirection");
            Assert.AreEqual(3, robot.Position.Y, "Test fails for Robot Position Y in TestRobotPositionAndDirection");
            Assert.AreEqual(MyCore.Direction.East, robot.Direction, "Test fails for Robot Direction in TestRobotPositionAndDirection");
        }
    }
}
