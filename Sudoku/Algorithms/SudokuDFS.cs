using Sudoku.Interfaces;

namespace Sudoku.Algorithms
{
    public class SudokuDFS : ISudokuSolver
    {
        private int[] numbersRange;
        private SudokuHelper sudokuHelper;
        public string Name => "DFS Backtracking";

        public SudokuDFS()
        {
            sudokuHelper = new SudokuHelper();
            numbersRange = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }


        public bool Solve(int[][] board)
        {           
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] != 0)
                        continue;


                    foreach (var num in numbersRange)
                    {
                        if (sudokuHelper.CheckCol(board, j, num) && sudokuHelper.CheckRow(board, i, num) 
                            && sudokuHelper.CheckBox(board, i, j, num))
                        {
                            board[i][j] = num;

                            if (Solve(board))
                            {
                                return true;
                            }

                            board[i][j] = 0;
                        }
                    }

                    return false;
                }
            }

            return true;
        }
    }
}
