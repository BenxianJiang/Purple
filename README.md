# Purple
############################################################################
##                 Toy Robot Simulator                                    ##
##                                                                        ##
##  A simulation of a toy robot moving on a 6 x 6 square grid tabletop    ##
############################################################################

How to play the application:

1: At the begining, place the toy on a 6 x 6 grid by issuing the following command:

    PLACE X,Y,F 
                         
    where X and Y are integers indicating toy's location on the grid. 
    X= 0, Y = 0 is SOUTH WEST corner and X = 5, Y = 5 is NORTH EAST corner.
    It will discard the command if X, Y is not between 0 and 5.
    F is Toy's facing direction text and it must be either NORTH, SOUTH, EAST or WEST.
    Example: PLACE 2,3,WEST - this will put robot on the table grid 2(column) and 3(rows) and facing WEST.

2: When the toy robot is placed on the table grid, subsequently you can issue the following commands, in any order:
                
    REPORT – Shows the current status of the toy robot. 
    PLACE X,Y,F - Replace toy's location on the grid where F is optional. 
                - When F is not provided, the robot moves to the new coordinates without changing the direction.
    LEFT   – turns the toy robot 90 degrees left.
    RIGHT  – turns the toy robot 90 degrees right.
    MOVE   – Moves the toy robot 1 unit in the facing direction.
    EXIT   – Closes the toy robot Simulator.

3: The simulator will discard all commands in the sequence until a valid PLACE command has been issued.
                      
4: The simulator will discard MOVE command when it is on the boundary grid and facing direction is outside the boundary grid.

5: The command and facing direction text are case insensitive.
                      
############################################################################