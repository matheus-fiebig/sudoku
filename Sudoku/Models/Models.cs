using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Models
{
    public class SudokuValues
    {
        public (int x, int y) Position { get; set; }

        public int[] HorizontalValues { get; set; }
        
        public int[] VerticalValues { get; set; }

        public int[] BoxValues { get; set; }
    }

    public class SudokuRange
    {
        public (int x, int y) Position { get; set; }
        public List<int> NumberRange { get; set; }
    }
}
