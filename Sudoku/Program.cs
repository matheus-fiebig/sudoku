using Sudoku.ExcelReader;
using Sudoku.Factory;
using Sudoku.Interfaces;

var dfs = AlgorithmFactory.Create(1);
var heuristic = AlgorithmFactory.Create(2);
var sa = AlgorithmFactory.Create(3);

int[] correctAnswers = new int[3];
double[] answerTime = new double[3];

var boards = ExcelReader.Read("sudoku_test.csv",300000);

var rnd = new Random();
var randomizedBoards = boards.OrderBy(item => rnd.Next());

foreach (var b in randomizedBoards.Select((item,i) => new {item, i}))
{
    Console.WriteLine("\n#Test" + b.i + 1);
    var boardDFS = Copy(b.item);
    var boardHeuristic = Copy(b.item);
    var boardSimulatedAnnealing = Copy(b.item);

    Test(dfs, boardDFS, 0);
    Test(heuristic, boardHeuristic, 1);
    Test(sa, boardSimulatedAnnealing, 2);
    Console.WriteLine();
}


Console.WriteLine("Informação da Execução");
Console.WriteLine($"{dfs.Name} - Precisão: {correctAnswers[0]}/{boards.Count} - Media de Resposta: {answerTime[0] / boards.Count}");
Console.WriteLine($"{heuristic.Name} - Precisão: {correctAnswers[1]}/{boards.Count} - Media de Resposta: {answerTime[1] / boards.Count}");
Console.WriteLine($"{sa.Name} - Precisão: {correctAnswers[2]}/{boards.Count} - Media de Resposta: {answerTime[2]/boards.Count}");


//Test Function
void Test(ISudokuSolver solver, (int[][] Board, int[][] Answer) item, int solverIndex)
{
    Console.WriteLine("."+solver.Name);
    DateTime startTime = DateTime.Now;

    var solved = solver.Solve(item.Board);
    correctAnswers[solverIndex] += solved ? 1 : 0;
    Console.WriteLine("Solved ? " + solved);

    var endTime = DateTime.Now;
    var executionTime = endTime.Subtract(startTime);
    Console.WriteLine("Tempo de execução: " + executionTime);

    answerTime[solverIndex] += executionTime.TotalMilliseconds;
}

//Utilitarios
(int[][] Board, int[][] Answer) Copy((int[][] Board, int[][] Answer) item)
{
    int[][] board = ExcelReader.InitializeMatrix();
    int[][] answer = ExcelReader.InitializeMatrix();
    for(int i = 0; i < 9; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            board[i][j] = item.Board[i][j];
            answer[i][j] = item.Answer[i][j];
        }
    }

    return (board, answer);
}
