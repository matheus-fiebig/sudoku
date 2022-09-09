using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.ExcelReader
{
    public class ExcelReader
    {
        public static List<(int[][] Board, int[][] Answer)> Read(string fileName, int take = -1)
        {
            var boards = new List<(int[][] Board, int[][] Answer)>();

            using (var reader = new StreamReader($"{fileName}"))
            {
                int[][] matrix = InitializeMatrix();
                int[][] answer = InitializeMatrix();
                int l = 0, index = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (index == take)
                        break;

                    if (l++ == 0)
                        continue;

                    index++;
                    var splittedLines = line.Split(',');
                    var board = splittedLines[0]; 
                    for (int i = 0, j = -1; i < 81; i++)
                    {
                        if(i % 9 == 0) j++;

                        matrix[j][i%9] = int.Parse(board[i].ToString());
                    }

                    var a = splittedLines[1];
                    for (int i = 0, j = -1; i < 81; i++)
                    {
                        if (i % 9 == 0) j++;

                        answer[j][i % 9] = int.Parse(a[i].ToString());
                    }

                    boards.Add((matrix, answer));
                }
            }

            return boards;
        }

        public static int[][] InitializeMatrix()
        {
            return new int[9][]
            {
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
                new int[9] {0,0,0,0,0,0,0,0,0},
            };
        }
    }
}
