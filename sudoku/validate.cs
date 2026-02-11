using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class validate
    {
        private const int SIZE = 9;
        private const int CELL_OCCUPIED = 0;
        private const int SQUARE_SIZE = 3;
        private const int BIT_ON = 1;
        public static bool checkBoard(String boardStr)
        {
            //checking if the board input is in the correct length
            if (boardStr.Length != SIZE * SIZE) return false;

            //checking if all the values we got are digits
            if(!boardStr.All(char.IsDigit) ) return false;

            return true;
        }

        public static bool checkCorrectness(int[,] board)
        {
            int value = 0, bit = 0;
            int[] row = new int[SIZE];
            int[] col = new int[SIZE];
            int[] box = new int[SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    value = board[i, j];

                    if (value == CELL_OCCUPIED) continue;

                    bit = BIT_ON << value;

                    //checking if the bit is already on and if
                    //so then the board is unsolvable
                    if ((row[i] & bit) != 0 ||
                        (col[j] & bit) != 0 ||
                        (box[(i / SQUARE_SIZE) * SQUARE_SIZE + j / SQUARE_SIZE] & bit) != 0)
                    {
                        return false;
                    }

                    //mapping the current bit in place number to be on
                    row[i] |= bit;
                    col[j] |= bit;
                    box[(i / SQUARE_SIZE) * SQUARE_SIZE + j / SQUARE_SIZE] |= bit;
                }
            }
            return true;
        }
    }
}
