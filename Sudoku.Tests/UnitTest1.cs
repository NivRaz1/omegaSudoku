using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System.ComponentModel.DataAnnotations;

namespace Sudoku.Tests
{
    [TestClass]
    public class SolverTests
    {
        [TestMethod]
        public void Solves_Valid_Sudoku()
        {
            string boardStr =
                "070000043" +
                "040009610" +
                "800634900" +
                "094052000" +
                "358460020" +
                "000800530" +
                "080070091" +
                "902100005" +
                "007040802";

            int[,] board = Formatting.boardToStr(boardStr);

            bool solved = Solver.solveSudoku(board);

            Assert.IsTrue(solved);
            Assert.IsTrue(validate.checkCorrectness(board));
        }

        [TestMethod]
        public void Detects_Unsolvable_Sudoku()
        {
            string boardStr =
                "627140503" +
                "345206971" +
                "089503602" +
                "000700364" +
                "793054018" +
                "460008059" +
                "056031097" +
                "971005836" +
                "834067555";

            int[,] board = Formatting.boardToStr(boardStr);

            bool solvable = validate.checkCorrectness(board);

            Assert.IsFalse(solvable);
        }

        [TestMethod]
        public void Solved_Board_Remains_Solved()
        {
            string boardStr =
                "534678912" +
                "672195348" +
                "198342567" +
                "859761423" +
                "426853791" +
                "713924856" +
                "961537284" +
                "287419635" +
                "345286179";

            int[,] board = Formatting.boardToStr(boardStr);

            bool solved = Solver.solveSudoku(board);

            Assert.IsTrue(solved);
            Assert.IsTrue(validate.checkCorrectness(board));
        }


    }
}