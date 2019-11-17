using System.Collections.Generic;

namespace SudokuSolver.Linear
{
    public class LinearIterator : ICellIterator
    {
        private readonly SudokuGame _game;
        private int current = -1;
        private List<Cell> cells;

        public LinearIterator(SudokuGame game)
        {
            _game = game;
            cells = game.GetCells();
        }

        public Cell GetNext()
        {
            do
            {
                if (current >= 80)
                {
                    current = 81;
                    return new AfterLastCell();
                }

                current++;
            } while (cells[current].IsConstrained);

            return cells[current];
        }

        public Cell GetPrevious()
        {
            do
            {
                if (current <= 0)
                {
                    current = -1;
                    return new BeforeFirstCell();
                }

                current--;
            } while (cells[current].IsConstrained);

            return cells[current];
        }
    }
}
