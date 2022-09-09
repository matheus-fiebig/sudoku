using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Algorithms
{
    public class SudokuHelper
    {
        public SudokuHelper()
        {

        }

        public bool CheckRow(int[][] board, int i, int n)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] == n) return false;
            }

            return true;
        }

        public bool CheckCol(int[][] board, int i, int n)
        {
            for (int j = 0; j < board.Length; j++)
            {
                if (board[j][i] == n)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckBox(int[][] board, int rowIndex, int columnIndex, int n)
        {
            var rowRange = BoxRange(rowIndex);
            var colRange = BoxRange(columnIndex);

            for (int j = rowRange.lowerBound; j <= rowRange.upperBound; j++)
            {
                for (int k = colRange.lowerBound; k <= colRange.upperBound; k++)
                {
                    if (board[j][k] == n)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public (int lowerBound, int upperBound) BoxRange(int i)
        {
            if (i < 3)
            {
                return (0, 2);
            }

            if (i < 6)
            {
                return (3, 5);
            }

            return (6, 8);
        }

        public int[] GetVerticalValues(int[][] matrix, int j)
        {
            var vertical = new int[9];

            for (int k = 0; k < 9; k++)
            {
                vertical[k] = matrix[k][j];
            }

            return vertical;
        }
        public int[] GetBoxValues(int[][] board, int rowIndex, int columnIndex)
        {
            var rowRange = BoxRange(rowIndex);
            var colRange = BoxRange(columnIndex);

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
        public int[] GetHorizontalValues(int[][] matrix, int i)
        {
            return matrix[i];
        }

        public int GetBlockRegion(int i, int j)
        {
            var rowRange = BoxRange(i);
            var colRange = BoxRange(j);

            if(rowRange.lowerBound == 0)
            {
                if (colRange.upperBound == 2)
                    return 0;
                if(colRange.upperBound == 5)
                    return 1;
                if(colRange.upperBound == 8)
                    return 2;
            }

            if (rowRange.lowerBound == 3)
            {
                if (colRange.upperBound == 2)
                    return 3;
                if (colRange.upperBound == 5)
                    return 4;
                if (colRange.upperBound == 8)
                    return 5;
            }

            if (rowRange.lowerBound == 6)
            {
                if (colRange.upperBound == 2)
                    return 6;
                if (colRange.upperBound == 5)
                    return 7;
                if (colRange.upperBound == 8)
                    return 8;
            }

            return -1;
        }
    }
}
