using Sudoku.Interfaces;
using Sudoku.Models;

namespace Sudoku.Algorithms
{
    public class SudokuFowardChecking : ISudokuSolver
    {
        public string Name => "Foward Checking";

        private SudokuHelper sudokuHelper;

        public SudokuFowardChecking()
        {
            sudokuHelper = new SudokuHelper();
        }

        public bool Solve(int[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] != 0)
                        continue;

                    var numberRange = NumberRange(board, i, j);
                    foreach (var num in numberRange)
                    {
                        board[i][j] = num;

                        if (Solve(board))
                        {
                            return true;
                        }

                        board[i][j] = 0;
                    }

                    return false;
                }
            }

            return true;
        }

        private IEnumerable<int> NumberRange(int[][] board, int i, int j)
        {
            var n = GetNeighbours(board, i, j);

            List<int> avaliableNumbers = new List<int>();
            for (int x = 1; x <= 9; x++)
            {
                if (Contains(n.BoxValues,x))
                    continue;

                avaliableNumbers.Add(x);
            }
            
            return avaliableNumbers;
        }

        private bool Contains(int[] list, int comparableValue)
        {
            foreach (var item in list)
            {
                if (item == comparableValue) return true;
            }

            return false;
        }

        private SudokuValues GetNeighbours(int[][] board, int i, int j)
        {
            //var horizontalValues = GetHorizontalValues(board,i);
            //var verticalValues = GetVerticalValues(board, j);
            var boxValues = GetBoxValues(board, i, j);
            return new SudokuValues
            {
                Position = (i, j),
                BoxValues = boxValues,
                //HorizontalValues = horizontalValues,
                //VerticalValues = verticalValues
            };
        }

        private int[] GetVerticalValues(int[][] matrix, int j)
         {
             var vertical = new int[9];

             for(int k = 0; k < 9; k++)
             {
                 vertical[k] = matrix[k][j];
             }

             return vertical;
         }
        private int[] GetBoxValues(int[][] board, int rowIndex, int columnIndex)
         {
             var rowRange = sudokuHelper.BoxRange(rowIndex);
             var colRange = sudokuHelper.BoxRange(columnIndex);

             var v = new int[9];
             int i = 0;

             for (int j = rowRange.lowerBound; j <= rowRange.upperBound; j++)
             {
                 for (int k = colRange.lowerBound; k <= colRange.upperBound; k++)
                 {
                     v[i++] = board[j][k];
                 }
             }

             return v;
         }
        private int[] GetHorizontalValues(int[][] matrix, int i)
        {
            return matrix[i];
        }
    }
}
