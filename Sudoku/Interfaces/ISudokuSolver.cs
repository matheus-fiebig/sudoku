namespace Sudoku.Interfaces
{
    public interface ISudokuSolver
    {
        string Name { get; }
        bool Solve(int[][] matrix);
    }
}
