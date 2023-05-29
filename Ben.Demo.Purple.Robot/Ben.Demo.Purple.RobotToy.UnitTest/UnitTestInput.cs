using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCore = Ben.Demo.Purple.RobotToy.Core;

namespace Ben.Demo.Purple.RobotToy.UnitTest
{
    /// <summary>
    /// This unit test for user input parameter. It check command and parameters.
    /// </summary>
    [TestClass]
    public class UnitTestInput
    {
        /// <summary>
        /// Test invalid command input text.
        /// </summary>
        [TestMethod]
        public void TestInvalidCommandInpput()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "Go".Split(" ".ToCharArray());

            //Get result
            var exception = Assert.ThrowsException<ArgumentException>(delegate { parser.ParseCommand(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.InvalidCommandText), "Test fails for PLACE command in TestInvalidCommandInpput!");
        }

        /// <summary>
        /// Test the valid PLACE command inpput.
        /// </summary>
        [TestMethod]
        public void TestValidPlaceCommand()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "Place".Split(" ".ToCharArray());

            //Get result
            var cmd = parser.ParseCommand(input);

            //Check the result
            Assert.AreEqual(cmd, MyCore.Command.Place, "Test fails for PLACE command in TestValidPlaceCommand!");
        }

        /// <summary>
        /// Test invalid PLACE command input.
        /// </summary>
        [TestMethod]
        public void TestInvalidPlaceCommand()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "PlaceME".Split(" ".ToCharArray());

            //Get result
            var exception = Assert.ThrowsException<ArgumentException>(delegate { parser.ParseCommand(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.InvalidCommandText), "Test fails for PLACE command in TestInvalidPlaceCommand!");
        }

        /// <summary>
        /// Test valid first PLACE command and parameters.
        /// </summary>
        [TestMethod]
        public void TestValidPlaceCommandAndInpput()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "PLACE 3,3,NORTH".Split(" ".ToCharArray());

            //Get result
            parser.ParsePlaceCommandParameters(input);

            //Check the result
            Assert.AreEqual(3, parser.Position.X, "Test fails for parse Position X in TestValidPlaceCommandAndInpput!");
            Assert.AreEqual(3, parser.Position.Y, "Test fails for parse Position Y in TestValidPlaceCommandAndInpput!");
            Assert.AreEqual(parser.Direction, MyCore.Direction.North, "Test fails for PLACE command Direction in TestValidPlaceCommandAndInpput!");
        }

        /// <summary>
        /// Test PLACE command with Wrong direction Text.
        /// </summary>
        [TestMethod]
        public void TestPlaceCommandAndInvalidDirectionText()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "PLACE 3,1,GO".Split(" ".ToCharArray());

            //Get result
            var exception = Assert.ThrowsException<ArgumentException>(delegate { parser.ParsePlaceCommandParameters(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.InvalidDirectionText), "Test fails for PLACE command due to wrong Direction Text in TestPlaceCommandAndInvalidDirectionText!");
        }

        /// <summary>
        /// Test non-Integer entered for X or Y in PLACE command.
        /// </summary>
        [TestMethod]
        public void TestPlaceCommandNonIntegerXandY()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            //Non-Integer for X
            string[] input = "PLACE 3.5,1,WEST".Split(" ".ToCharArray());

            //Get result
            var exception = Assert.ThrowsException<ArgumentException>(delegate { parser.ParsePlaceCommandParameters(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.InvalidPlaceCommandXY), "Test fails for non-Integer X or Y in PLACE command in TestPlaceCommandNonIntegerXandY!");

            //Non-Integer for Y
            input = "PLACE 3,Y,NORTH".Split(" ".ToCharArray());

            //Get result
            exception = Assert.ThrowsException<ArgumentException>(delegate { parser.ParsePlaceCommandParameters(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.InvalidPlaceCommandXY), "Test fails for non-Integer X or Y in PLACE command in TestPlaceCommandNonIntegerXandY!");
        }

        /// <summary>
        /// Test first time PLACE command with an incomplete input
        /// </summary>
        [TestMethod]
        public void TestFirstTimePlaceCommandAndIncompleteInput()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "PLACE 3,1".Split(" ".ToCharArray());

            //Get result
            var exception = Assert.ThrowsException<ArgumentException>(delegate { parser.ParsePlaceCommandParameters(input); });

            //Check the result
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.IncompletePlaceCommandText), "Test fails for incomplete PLACE command in TestPlaceCommandAndIncompleteInput!");
        }

        /// <summary>
        /// Test the subsequent Place command without direction provided in the input text.
        /// </summary>
        [TestMethod]
        public void TestSequentPlaceCommandWithoutDirection()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "PLACE 3,1,SOUTH".Split(" ".ToCharArray());
            //first valid PLACE command issued
            parser.ParsePlaceCommandParameters(input);

            //second time Place command without direction
            input = "PLACE 2,4".Split(" ".ToCharArray());
            //Get result
            parser.ParsePlaceCommandParameters(input);

            //Check
            Assert.AreEqual(2, parser.Position.X, "Test fails for parse Position X in TestSequentPlaceCommandWithoutDirection!");
            Assert.AreEqual(4, parser.Position.Y, "Test fails for parse Position Y in TestSequentPlaceCommandWithoutDirection!");
            Assert.AreEqual(parser.Direction, MyCore.Direction.South, "Test fails for PLACE command Direction in TestSequentPlaceCommandWithoutDirection!");
        }

        /// <summary>
        /// Test the subsequent Place command with direction provided in the input text.
        /// </summary>
        [TestMethod]
        public void TestSequentPlaceCommandWithDirection()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "PLACE 3,1,SOUTH".Split(" ".ToCharArray());
            //first valid PLACE command issued
            parser.ParseCommand(input);
            //second time Place command with direction
            input = "PLACE 2,4,WEST".Split(" ".ToCharArray());
            parser.ParseCommand(input);

            //Get result
            parser.ParsePlaceCommandParameters(input);

            //Check
            Assert.AreEqual(2, parser.Position.X, "Test fails for parse Position X in TestSequentPlaceCommandWithDirection!");
            Assert.AreEqual(4, parser.Position.Y, "Test fails for parse Position Y in TestSequentPlaceCommandWithDirection!");
            Assert.AreEqual(parser.Direction, MyCore.Direction.West, "Test fails for PLACE command Direction in TestSequentPlaceCommandWithDirection!");
        }

        /// <summary>
        /// Test PLACE command with more than 3 parameters.
        /// </summary>
        [TestMethod]
        public void TestPlaceInputWithParameterMoreThanThree()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "PLACE 3,3,SOUTH,2".Split(" ".ToCharArray());

            //Get result
            var exception = Assert.ThrowsException<ArgumentException>(delegate { parser.ParsePlaceCommandParameters(input); });

            //Check
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.IncompletePlaceCommandText), "Test fails for incomplete PLACE command in TestPlaceInputWithParameterMoreThanThree!");
        }

        /// <summary>
        /// Test PLACE command with less than 2 parameters.
        /// </summary>
        [TestMethod]
        public void TestPlaceInputWithParameterLessThanTwo()
        {
            //Prepare for the test
            var parser = new MyCore.InputChecker(null, MyCore.Direction.East);
            string[] input = "PLACE 3,SOUTH".Split(" ".ToCharArray());

            //Get result
            var exception = Assert.ThrowsException<ArgumentException>(delegate { parser.ParsePlaceCommandParameters(input); });

            //Check
            Assert.IsTrue(exception.Message.Contains(MyCore.Constants.IncompletePlaceCommandText), "Test fails for incomplete PLACE command in TestPlaceInputWithParameterLessThanTwo!");
        }
    }
}
