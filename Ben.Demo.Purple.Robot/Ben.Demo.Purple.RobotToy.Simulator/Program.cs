﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCore = Ben.Demo.Purple.RobotToy.Core;

namespace Purple.Robot.Simulator
{
    class Program
    {
            /// <summary>
            /// Entry funtion for the console application.
            /// </summary>
            /// <param name="args">Parameters for the console application if any.</param>
        static void Main(string[] args)
            {
                //Display application instruction text to user
                Console.WriteLine(MyCore.Constants.ApplicationInstructionText);

                //Prepare 6 X 6 grid board and initilize simulator.
                MyCore.ToyBoard board = new MyCore.ToyBoard(6, 6);
                MyCore.InputChecker inputChecker = new MyCore.InputChecker(null, MyCore.Direction.North);
                MyCore.IToyRobot robot = new MyCore.ToyRobot();
                var simulator = new MyCore.Simulator(robot, board, inputChecker);

                //Accept user's commands until "Exit" command received;
                bool endApp = false;
                while (!endApp)
                {
                    var cmd = Console.ReadLine();
                    if (cmd == null) continue;

                    if (cmd.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                    {
                        endApp = true;
                    }
                    else
                    {
                        try
                        {
                            string output = simulator.ProcessCommand(cmd.Split(' '));

                            if (!string.IsNullOrWhiteSpace(output))
                            {
                                Console.WriteLine(output);
                            }
                        }
                        catch (ArgumentException exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }
                }
            }
    }
}
