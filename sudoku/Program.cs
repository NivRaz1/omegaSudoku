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

            //getting the sudoku from user and converting the string to matrix
            Console.WriteLine("Enter sudoku board:");
            boardStr = Console.ReadLine();
            board = Formatting.boardToStr(boardStr);

            Console.WriteLine("\nBefore solving:");
            Formatting.printBoard(board);


            sw = Stopwatch.StartNew();
            Solver.solveSudokuMRV(board);
            sw.Stop();

            Console.WriteLine("\nAfter solving:");
            Formatting.printBoard(board);
            Console.WriteLine($"Took the program " +
                $"{sw.Elapsed.TotalMilliseconds:F3} " +
                $"ms to solve the sudoku");
        }
    }
}
