using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String boardStr = "";
            int[,] board;
            Stopwatch sw;

            while (true)
            {
                //getting the sudoku from user and converting the string to matrix
                Console.WriteLine("Enter sudoku board(enter END to exit):\n");
                boardStr = Console.ReadLine();

                if (boardStr.ToUpper().Equals("END")) break;
                if(!validate.checkBoard(boardStr))
                {
                    Console.WriteLine("Invalid board");
                    continue;
                }

                board = Formatting.boardToStr(boardStr);

                if(!validate.checkCorrectness(board))
                {
                    Console.WriteLine("The board is unsolvable");
                    continue;
                }

                Console.WriteLine("\nBefore solving:");
                Formatting.printBoard(board);


                sw = Stopwatch.StartNew();
                Solver.solveSudoku(board);
                sw.Stop();

                Console.WriteLine("\nAfter solving:");
                Formatting.printBoard(board);
                Console.WriteLine($"Took the program " +
                    $"{sw.Elapsed.TotalMilliseconds:F3} " +
                    $"ms to solve the sudoku\n");
            }
        }
    }
}
