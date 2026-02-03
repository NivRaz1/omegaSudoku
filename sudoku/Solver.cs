using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    internal class Solver
    {
        private const int SIZE = 9;
        private const int SQUARE_SIZE = 3;
        private static bool isSafe(int[,] board, int i, int j, int num,
                           int[] row, int[] col, int[] box)
        {
            if ((row[i] & (1 << num)) != 0 || (col[j] & (1 << num)) != 0 ||
                (box[i / 3 * 3 + j / 3] & (1 << num)) != 0)
            {
                return false;
            }
            return true;
        }

        private static bool sudokuSolverRec(int[,] board, int i, int j,
                                    int[] row, int[] col, int[] box)
        {
            int n = board.GetLength(0);

            // base case: Reached nth column of last row
            if (i == n - 1 && j == n)
            {
                return true;
            }

            // If reached last column of the row, go to next row
            if (j == n)
            {
                i++;
                j = 0;
            }

            // If cell is already occupied, then move forward
            if (board[i, j] != 0)
            {
                return sudokuSolverRec(board, i, j + 1, row, col, box);
            }

            for (int num = 1; num <= n; num++)
            {
                // If it is safe to place num at current position
                if (isSafe(board, i, j, num, row, col, box))
                {
                    board[i, j] = num;

                    // Update masks for the corresponding row, column, and box
                    row[i] |= (1 << num);
                    col[j] |= (1 << num);
                    box[i / 3 * 3 + j / 3] |= (1 << num);

                    if (sudokuSolverRec(board, i, j + 1, row, col, box))
                    {
                        return true;
                    }

                    // Unmask the number num in the corresponding row, column and box masks
                    board[i, j] = 0;
                    row[i] &= ~(1 << num);
                    col[j] &= ~(1 << num);
                    box[i / 3 * 3 + j / 3] &= ~(1 << num);
                }
            }
            return false;
        }

        public static bool solveSudoku(int[,] board)
        {
            int n = board.GetLength(0);
            int[] row = new int[n];
            int[] col = new int[n];
            int[] box = new int[n];

            // Set the bits in bitmasks for values that are initially present
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i, j] != 0)
                    {
                        row[i] |= (1 << board[i, j]);
                        col[j] |= (1 << board[i, j]);
                        box[(i / 3) * 3 + j / 3] |= (1 << board[i, j]);
                    }
                }
            }
            return sudokuSolverRec(board, 0, 0, row, col, box);
        }

        private static int[] findDomain(int[,] board, int i, int j)
        {
            HashSet<int> domain = new HashSet<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            HashSet<int> used = new HashSet<int>();
            int row = 0, col = 0;
            
            for(col = 0; col < SIZE; col++)
            {
                used.Add(board[i, col]);
            }

            for(row = 0; row < SIZE; row++)
            {
                used.Add(board[row, j]);
            }

            row = (i / SQUARE_SIZE) * SQUARE_SIZE;
            col = (j / SQUARE_SIZE) * SQUARE_SIZE;

            for(int a = 0; a < SQUARE_SIZE; a++)
            {
                for(int b = 0;  b < SQUARE_SIZE; b++)
                {
                    used.Add(board[row + a, col + b]);
                }
            }

            domain.ExceptWith(used);
            return domain.ToArray();
        }
        private static bool solved(int[,] board)
        {
            int i = 0, j = 0;

            for(i = 0; i < SIZE; i++)
            {
                for(j = 0; j < SIZE; j++)
                {
                    if (board[i, j] == 0) return false;
                }
            }
            return true;
        }

        private static int[] findMrvCell(int[,] board, ref int row, ref int col)
        {
            int i = 0, j = 0;
            int[] domain = null, minDomain = null;

            for(i = 0; i < SIZE; i ++)
            {
                for(j = 0; j < SIZE; j ++)
                {
                    if (board[i, j] == 0)
                    {
                        domain = findDomain(board, i, j);

                        if(domain.Length == 0)
                        {
                            row = i; col = j;
                            return domain;
                        }

                        if(minDomain == null || domain.Length < minDomain.Length)
                        {
                            minDomain = domain;
                            row = i; col = j;
                        }
                    }
                }
            }
            return minDomain;
        }

        public static bool solveSudokuMRV(int[,] board)
        {
            int row = 0, col = 0;
            int[] domain = null;

            if(solved(board))
            {
                return true;
            }

            domain = findMrvCell(board, ref row, ref col);

            foreach ( int num in  domain )
            {
                board[row, col] = num;
                if(solveSudokuMRV(board))
                {
                    return true;
                }
                board[row, col] = 0;
            }
            return false;
        }
    }
}
