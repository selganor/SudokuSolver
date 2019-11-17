using System;

namespace SudokuSolver
{
    public enum SolveResults
    {
        Solved,
        NoMoreValuesForCell
    }
    public class SudokuGameSolver
    {
        private readonly ICellIterator _iterator;
        private readonly SudokuGame _game;

        public SudokuGameSolver(ICellIterator iterator, SudokuGame game)
        {
            _iterator = iterator;
            _game = game;
        }

        public SolveResults Solve()
        {
            var cell = _iterator.GetNext();
            if (cell is AfterLastCell)
            {
                if (_game.IsSolved)
                    return SolveResults.Solved;

                throw new InvalidOperationException("Should not happen...");
            }

            do
            {

                while (cell.PossibleValuesCount > 0)
                {
                    cell.TryNextValue();
                    var result = Solve();
                    if (result == SolveResults.Solved)
                        return SolveResults.Solved;

                }
            } while (cell.PossibleValuesCount > 0);

            if (_game.IsSolved)
                return SolveResults.Solved;

            cell.Unset();
            _iterator.GetPrevious();
            return SolveResults.NoMoreValuesForCell;
        }
    }
}
