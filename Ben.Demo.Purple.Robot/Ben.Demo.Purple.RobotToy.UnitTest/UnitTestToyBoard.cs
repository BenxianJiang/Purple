using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCore = Ben.Demo.Purple.RobotToy.Core;

namespace Ben.Demo.Purple.RobotToy.UnitTest
{
    /// <summary>
    /// Test for Toy Board.
    /// </summary>
    [TestClass]
    public class UnitTestToyBoard
    {
        /// <summary>
        /// Test when put the robot toy outside the boundary of the board.
        /// </summary>
        [TestMethod]
        public void TestBoardPositionOutsideBoundary()
        {
            //Prepare for the test
            var board = new MyCore.ToyBoard(6, 6);
            var position = new MyCore.Position(7, 6);

            //Get result
            var result = board.IsValidPosition(position);
            
            //Check the result
            Assert.IsFalse(result, "Try to put toy outside the board. Test fails in TestBoardPositionOutsideBoundary!");
        }

        /// <summary>
        /// Test when put the robot toy inside the boundary of the board.
        /// </summary>
        [TestMethod]
        public void TestBoardPositionInsideBoundary()
        {
            //Prepare for the test
            var board = new MyCore.ToyBoard(6, 6);
            var position = new MyCore.Position(3, 5);

            //Get result
            var result = board.IsValidPosition(position);

            //Check the result
            Assert.IsTrue(result, "Try to put toy inside the board. Test fails in TestBoardPositionInsideBoundary!");
        }
    }
}
