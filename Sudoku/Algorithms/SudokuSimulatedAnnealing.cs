using Sudoku.Extensions;
using Sudoku.Interfaces;

namespace Sudoku.Algorithms
{
    public class SudokuSimulatedAnnealing : ISudokuSolver
    {
        public string Name => "Simulated Annealing";
        private SudokuHelper _sudokuHelper;
        private Random random;
        private int[][] immutableValues;

        private const int maxIterations = 60000;

        public SudokuSimulatedAnnealing()
        {
            _sudokuHelper = new SudokuHelper();
            random = new Random();
        }

        public bool Solve(int[][] matrix)
        {
            immutableValues = matrix.Copy();

            double temperature = 0.3d;
            GenerateRandomState(matrix);

            int bestCost, currentCost, it = 0;
            currentCost = bestCost = CostFunction(matrix);

            while (it++ < maxIterations)
            {
                var possibleNewMatrix = SwapTwoCells(matrix);
                int cost = CostFunction(possibleNewMatrix);

                if (Math.Exp(-(currentCost - cost)) - random.NextDouble() > 0)
                {
                    currentCost = cost;
                    matrix = possibleNewMatrix;
                }

                if (currentCost > bestCost)
                {
                    bestCost = cost;
                }

                if (bestCost == 162)
                    return true;

                temperature *= .99;
            }

            return false;
        }

        private void GenerateRandomState(int[][] matrix, int col = 0, int row = 0)
        {
            if (col >= 9 || row >= 9)
                return;

            var rowRange = _sudokuHelper.BoxRange(col);
            var colRange = _sudokuHelper.BoxRange(row);
            List<int> avaliableNumbers = GetAvaliableNumbers(matrix, rowRange, colRange);

            for (int i = rowRange.lowerBound; i <= rowRange.upperBound; i++)
            {
                for (int j = colRange.lowerBound; j <= colRange.upperBound; j++)
                {
                    if (matrix[i][j] > 0)
                        continue;

                    matrix[i][j] = avaliableNumbers[random.Next(0, avaliableNumbers.Count - 1)];
                    avaliableNumbers.Remove(matrix[i][j]);
                }
            }

            if (col < 9)
                GenerateRandomState(matrix, col + 3, row);

            if (row < 9)
                GenerateRandomState(matrix, col, row + 3);
        }

        private int[][] SwapTwoCells(int[][] matrix)
        {
            var newMatrix = ExcelReader.ExcelReader.InitializeMatrix();
            newMatrix = matrix.Copy();

            try
            {
                var avaliableBlocks = GetAvaliableBlocks(immutableValues);
                var col = avaliableBlocks[random.Next(avaliableBlocks.Count)];
                var row = avaliableBlocks[random.Next(avaliableBlocks.Count)];

                var colRange = _sudokuHelper.BoxRange(col);
                var rowRange = _sudokuHelper.BoxRange(row);

                var avaliableToSwap = GetAvaliableIndexes(immutableValues, rowRange, colRange);

                if(avaliableToSwap.Count == 0)
                {
                    return matrix;
                }

                var c1 = avaliableToSwap[random.Next(0, avaliableToSwap.Count)];
                var c2 = avaliableToSwap[random.Next(0, avaliableToSwap.Count)];

                var aux = newMatrix[c1.i][c1.j];
                newMatrix[c1.i][c1.j] = newMatrix[c2.i][c2.j];
                newMatrix[c2.i][c2.j] = aux;
                
                return newMatrix;
            }
            catch 
            { 
                return newMatrix;
            }
        }

        private int CostFunction(int[][] matrix)
        {
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                Dictionary<int, int> uniqueXValues = new();
                Dictionary<int, int> uniqueYValues = new();

                for (int j = 0; j < 9; j++)
                {
                    int keyX = matrix[i][j];
                    AddValue(uniqueXValues, keyX);

                    int keyY = matrix[j][i];
                    AddValue(uniqueYValues, keyY);
                }

                foreach (var dict in uniqueXValues)
                {
                    if (dict.Value >= 1) sum++;
                }

                foreach (var dict in uniqueYValues)
                {
                    if (dict.Value >= 1) sum++;
                }

            }


            return sum;
        }

        private List<int> GetAvaliableNumbers(int[][] matrix, (int lowerBound, int upperBound) rowRange, (int lowerBound, int upperBound) colRange)
        {
            var avaliableNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = rowRange.lowerBound; i <= rowRange.upperBound; i++)
            {
                for (int j = colRange.lowerBound; j <= colRange.upperBound; j++)
                {
                    if (matrix[i][j] > 0)
                    {
                        avaliableNumbers.Remove(matrix[i][j]);
                    }

                }
            }

            return avaliableNumbers;
        }

        private List<(int i, int j)> GetAvaliableIndexes(int[][] matrix, (int lowerBound, int upperBound) rowRange, (int lowerBound, int upperBound) colRange)
        {
            var avaliableNumbers = new List<(int i, int j)>();
            for (int i = rowRange.lowerBound; i <= rowRange.upperBound; i++)
            {
                for (int j = colRange.lowerBound; j <= colRange.upperBound; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        avaliableNumbers.Add((i, j));
                    }

                }
            }

            return avaliableNumbers;
        }

        private List<int> GetAvaliableBlocks(int[][] matrix)
        {
            var avaliable = new Dictionary<int,int>();
            
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        continue;
                    }

                    avaliable.TryAdd(_sudokuHelper.GetBlockRegion(i,j), 1);
                }
            }

            var avaliableList = new List<int>();
            foreach (var item in avaliable)
            {
                avaliableList.Add(item.Key);
            }

            return avaliableList;
        }

        private void AddValue(Dictionary<int, int> dictionary, int key)
        {
            int actualValue = 0;
            dictionary.TryGetValue(key, out actualValue);
            if (actualValue == 0)
                dictionary[key] = 1;
            else
                dictionary[key]++;
        }
    }
}
