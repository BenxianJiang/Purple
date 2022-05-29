using System;

namespace Ben.Demo.Purple.RobotToy.Core
{

    public class ToyRobot : IToyRobot
    {
        public Direction Direction { get; set; }
        public Position Position { get; set; }

        /// <summary>
        /// Place Robot on the toy board, eg. set robot Position and Direction.
        /// </summary>
        /// <param name="position">Position object</param>
        /// <param name="direction">Direction object</param>
        public void Place(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        /// <summary>
        /// Move 1 unit to new/next position based on the Position and direction of Robot.
        /// </summary>
        /// <returns>The next/new Positon of robot.</returns>
        public Position Move()
        {
            var result = new Position(Position.X, Position.Y);
            switch (Direction)
            {
                case Direction.North:
                    result.Y += 1;
                    break;

                case Direction.East:
                    result.X += 1;
                    break;

                case Direction.South:
                    result.Y -= 1;
                    break;

                case Direction.West:
                    result.X -= 1;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Turn toy 90 degreesto the left.
        /// </summary>
        public void RotateLeft()
        {
            Rotate(-1);
        }

        /// <summary>
        /// Turn toy 90 degreesto the right.
        /// </summary>
        public void RotateRight()
        {
            Rotate(1);
        }
        
        /// <summary>
        /// Set the new/next direction of robot. The next/new direction is based on 
        /// current direction and the rotation command either Left or Rigth.
        /// </summary>
        /// <param name="rotationNumber">Integer the rotation number, 
        /// eg. 1 for right turn or -1 for left turn.
        /// </param>
        public void Rotate(int rotationNumber)
        {
            //turn to direction array used for calculate next direction
            var directions = (Direction[])Enum.GetValues(typeof(Direction));
            int countDirection = directions.Length;
            Direction newDirection;

            //calculate next direction after rotation.
            if ((Direction + rotationNumber) < 0)
            {
                newDirection = directions[countDirection - 1];
            }
            else
            {
                int index = ((int)(Direction + rotationNumber)) % countDirection;
                newDirection = directions[index];
            }

            Direction = newDirection;
        }       
    }

    /// <summary>
    /// Define Interface for ToyRobot
    /// </summary>
    public interface IToyRobot
    {
        Direction Direction { get; set; }
        Position Position { get; set; }

        /// <summary>
        /// Sets the robot position and direction.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        void Place(Position position, Direction direction);

        /// <summary>
        /// Move 1 unit to the new/next position of robot based on its current direction.
        /// </summary>
        /// <returns>New/next position of the robot.</returns>
        Position Move();

        /// <summary>
        /// Turn toy 90 degreesto the left.
        /// </summary>
        void RotateLeft();

        /// <summary>
        /// Turn toy 90 degreesto the right.
        /// </summary>
        void RotateRight();     

        /// <summary>
        /// Set the new/next direction of robot. The next/new direction is based on 
        /// current direction and the rotation command either Left or Rigth. 
        /// </summary>
        /// <param name="rotationNumber">Integer the rotation number, 
        /// eg. 1 for right turn or -1 for left turn.
        /// </param>
        void Rotate(int rotationNumber);
    }
}
