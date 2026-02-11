using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Solver
    {
        private const int SIZE = 9;
        private const int SQUARE_SIZE = 3;
        private const int FULL_MASK = 0b1111111110;
        private const int MIN_COUNT = 1;
        private const int DEAD_END = 0;
        private const int BOARD_FINISHED = 0;
        private const int BIT_ON = 1;
        private const int CELL_OCCUPIED = 0;

        private static int countBits(int domain)
        {
            int count = 0;
            //counting the number of bits that are on
            while (domain != CELL_OCCUPIED)
            {
                domain &= domain - 1;
                count++;
            }
            return count;
        }
        private static bool findMrvCell(int[,] board, int[] row, int[] col,
            int[] box, ref int rowIndex, ref int colIndex, ref int minDomain)
        {
            int i = 0, j = 0, boxIndex = 0;
            int domain = 0, count = 0, minCount = int.MaxValue;
            bool found = false;

            for (i = 0; i < SIZE; i++)
            {
                for (j = 0; j < SIZE; j++)
                {
                    // the cell is not empty
                    if (board[i, j] != 0) continue;

                    boxIndex = (i / SQUARE_SIZE) * SQUARE_SIZE + (j / SQUARE_SIZE);
                    //calculating the domain of avilable numbers for the cell
                    domain = FULL_MASK & ~(row[i] | col[j] | box[boxIndex]);
                    //counting the domain length
                    count = countBits(domain);

                    //if there are 0 avilable moves then its a dead end
                    if (count == DEAD_END)
                    {
                        minDomain = 0;
                        return false;
                    }

                    //if there are less remaining values
                    if (count < minCount)
                    {
                        //saving the cell and it's domain information
                        minCount = count;
                        rowIndex = i;
                        colIndex = j;
                        minDomain = domain;
                        found = true; // found an empty cell

                        //if the cell has only 1 legal move
                        //then it's the strongest choice
                        if (count == MIN_COUNT)
                        {
                            return true;
                        }
                    }
                }
            }

            //if didn't found an empty cell then board is complete
            if (!found)
            {
                minDomain = 0;
                return true;
            }
            //found cell and is not a dead end
            return true;
        }

        private static bool checkNeighbors(int[,] board, int[] row, int[] col, int[] box, int rowIndex, int colIndex)
        {
            int i = 0, j = 0, rowStart = 0, colStart = 0, domain = 0;

            //checking for numbers in the same row
            for(j = 0; j < SIZE; j++)
            {
                if (board[rowIndex, j] != 0) { continue; }

                domain = FULL_MASK & ~(row[rowIndex] | col[j] | 
                    box[(rowIndex / SQUARE_SIZE) * SQUARE_SIZE + (j / SQUARE_SIZE)]);
                if (domain == 0)
                {
                    return false;
                }
            }

            //checking for number in the same col
            for (i = 0; i < SIZE; i++)
            {
                if (board[i, colIndex] != CELL_OCCUPIED) { continue; }

                domain = FULL_MASK & ~(row[i] | col[colIndex] | 
                    box[(i / SQUARE_SIZE) * SQUARE_SIZE + (colIndex / SQUARE_SIZE)]);
                if (domain == DEAD_END)
                {
                    return false;
                }
            }

            rowStart = (rowIndex / SQUARE_SIZE) * SQUARE_SIZE;
            colStart = (colIndex / SQUARE_SIZE) * SQUARE_SIZE;
            //checking for numbers in the same box
            for(i = 0; i < SQUARE_SIZE; i+= 2)
            {
                for(j = 0; j < SQUARE_SIZE; j+= 2)
                {
                    int r = rowStart + i;
                    int c = colStart + j;

                    if (board[r, c] != 0) { continue; }

                    domain = FULL_MASK & ~(row[r] | col[c] |
                        box[(r / SQUARE_SIZE) * SQUARE_SIZE + (c / SQUARE_SIZE)]);
                    if (domain == DEAD_END)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private static bool sudokuSolverRec(int[,] board, int[] row, int[] col, int[] box)
        {
            int rowIndex = 0, colIndex = 0, boxIndex = 0;
            int domain = 0, bit = 0, num = 0;

            //checking if the board is solved
            if (!findMrvCell(board, row, col, box, ref rowIndex, ref colIndex, ref domain))
            {
                return false;
            }

            //if the domain is 0 and we didn't get false
            //on findMrvCell then the board is complete
            if (domain == BOARD_FINISHED)
            {
                return true;
            }

            boxIndex = (rowIndex / SQUARE_SIZE) * SQUARE_SIZE + (colIndex / SQUARE_SIZE);

            for (num = 1; num <= SIZE; num++)
            {
                //checking if the bit for the number is on
                bit = BIT_ON << num;
                if ((domain & bit) == 0) continue;

                //Update masks for the corresponding row, column, and box
                board[rowIndex, colIndex] = num;
                row[rowIndex] |= bit;
                col[colIndex] |= bit;
                box[boxIndex] |= bit;

                if (checkNeighbors(board, row, col, box, rowIndex, colIndex) &&
                    sudokuSolverRec(board, row, col, box))
                {
                    return true;
                }

                //Unmask the number num in the corresponding row, column and box masks
                board[rowIndex, colIndex] = 0;
                row[rowIndex] &= ~bit;
                col[colIndex] &= ~bit;
                box[boxIndex] &= ~bit;
            }
            return false;
        }

        public static bool solveSudoku(int[,] board)
        {
            int value = 0, bit = 0;
            int[] row = new int[SIZE];
            int[] col = new int[SIZE];
            int[] box = new int[SIZE];

            // Set the bits in bitmasks for values that are initially present
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    value = board[i, j];
                    if (value == CELL_OCCUPIED) { continue; }

                    bit = BIT_ON << value;
                    row[i] |= bit;
                    col[j] |= bit;
                    box[(i / SQUARE_SIZE) * SQUARE_SIZE + j / SQUARE_SIZE] |= bit;
                }
            }
            return sudokuSolverRec(board, row, col, box);
        }
    }
}
