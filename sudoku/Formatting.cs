using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    internal class Formatting
    {
        private const int SIZE = 9;
        private const int SQUARE_COUNT = 3;

        public static int[,] boardToStr(String boardStr)
        {
            int[,] board = new int[SIZE, SIZE];
            int i = 0, j = 0;

            for(i = 0; i < SIZE; i++)
            {
                for(j = 0; j < SIZE; j++)
                {
                    board[i, j] = boardStr[i * SIZE + j] - '0';
                }
            }

            return board;
        }

        public static void printBoard(int[,] board)
        {
            int i = 0, j = 0;

            for (i = 0; i < SIZE; i++)
            {
                if (i > 0 && i % (SIZE / SQUARE_COUNT) == 0)
                {
                    printHorizontal();
                }
                for (j = 0; j < SIZE; j++)
                {
                    if(j > 0 && j % (SIZE / SQUARE_COUNT) == 0)
                    {
                        Console.Write("| ");
                    }
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void printHorizontal()
        {
            int i = 0;

            for(i = 0; i < SIZE+2; i++)
            {
                Console.Write("--");
            }
            Console.WriteLine();
        }
    }
}
