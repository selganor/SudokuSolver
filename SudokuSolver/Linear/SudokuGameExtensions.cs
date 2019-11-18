using SudokuSolver.Linear;

namespace SudokuSolver
{
    public static class SudokuGameExtensions
    {
        public static ICellIterator GetLinearIterator(this SudokuGame game)
        {
            return new LinearIterator(game);
        }

        public static SolveResults SolveUsingLinearIterator(this SudokuGame game)
        {
            var iterator = game.GetLinearIterator();
            var solver = new SudokuGameSolver(iterator, game);
            return solver.Solve();
        }
    }
}
