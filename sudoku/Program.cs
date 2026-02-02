using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter sudoku board:");
            String boardStr = Console.ReadLine();
            int[,] board = Formatting.boardToStr(boardStr);

            Console.WriteLine("\nBefore solving:");
            Formatting.printBoard(board);
        }
    }
}
