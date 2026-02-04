Omega sudoku solver project in c#.
The project feature a sudoku solver that use those next things in it's algorithm:
Backtracking search - the base of the algorithm, try to insert numbers in the box and check the next cells and if it reach and dead end it returns backward.
MRV(Minimum remaining values) - after the algorithm tried to put a number in a cell it will search the next cell with least possible options and go from there.
Forword checking - after the algorithm tried a number it will check it's neighboring numbers in the same row, col and box to see if any one of them reached 0 possible moves
and will find dead ends early.
Bitmasking - each row, col and box use bits to represent if a number is already used in their domain or not, if the number is used the bit is on in the masking if it's not then the bit is off.
In addition the program also checks if the given board was invalid or unsolvable and returns a message accordingly.
