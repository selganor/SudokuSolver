using SudokuSolver.Linear;

namespace SudokuSolver
{
    public static class SudokuGameExtensions
    {
        public static ICellIterator GetLinearIterator(this SudokuGame game)
        {
            return new LinearIterator(game);
        }
    }
}
