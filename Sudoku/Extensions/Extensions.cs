using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Extensions
{
    public static class Extensions
    {
        public static int[][] Copy(this int[][] item)
        {
            int[][] board = ExcelReader.ExcelReader.InitializeMatrix();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i][j] = item[i][j];
                }
            }

            return board;
        }
    }
}
