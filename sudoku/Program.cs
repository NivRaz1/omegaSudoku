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

                //checking if the user want to exit
                if (boardStr.ToUpper().Equals("END")) break;
                //checking if the board input from the user is valid
                if(!validate.checkBoard(boardStr))
                {
                    Console.WriteLine("Invalid board");
                    continue;
                }

                //converting the board input to sudoku board
                board = Formatting.boardToStr(boardStr);

                //checks if the board is solvable
                if(!validate.checkCorrectness(board))
                {
                    Console.WriteLine("The board is unsolvable");
                    continue;
                }

                Console.WriteLine("\nBefore solving:");
                Formatting.printBoard(board);

                //solving the board and counting time
                sw = Stopwatch.StartNew();
                Solver.solveSudoku(board);
                sw.Stop();

                Console.WriteLine("\nAfter solving:");
                Formatting.printBoard(board);
                Console.WriteLine($"Took the program " +
                    $"{sw.Elapsed.TotalMilliseconds:F3} " +
                    $"ms or {sw.Elapsed.TotalSeconds:F6}" +
                    $" seconds to solve the sudoku\n");
            }
        }
    }
}
