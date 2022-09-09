using Sudoku.Algorithms;
using Sudoku.Interfaces;

namespace Sudoku.Factory
{
    public class AlgorithmFactory
    {
        public static ISudokuSolver Create(int type)
        {
            switch (type)
            {
                case 1: 
                    return new SudokuDFS();
                case 2:
                    return new SudokuFowardChecking(); 
                case 3:
                    return new SudokuSimulatedAnnealing(); 
                default:
                    throw new ArgumentException("not found");
            }
        }
    }
}
