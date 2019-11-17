using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class SudokuGame
    {
        private List<Cell> allCells = new List<Cell>();
        private CellGroup[] rows = new CellGroup[9];
        private CellGroup[] columns = new CellGroup[9];
        private CellGroup[] quadrants = new CellGroup[9];

        public SudokuGame()
        {
            CreateCellGroups();

            BuildGameBoard();
        }

        /// <summary>
        /// Tells if the schema is complete.
        /// </summary>
        public bool IsSolved => allCells.All(c => c.CurrentValue != 0);

        public List<Cell> GetCells()
        {
            return allCells;
        }

        public void SetConstrainedCell(Coords coords, int value)
        {
            if (value < 1 || value > 9)
                throw new InvalidOperationException("Value must be in the range [1..9]");

            var cell = GetCellAt(coords);
            cell.SetConstrainedValue(value);
        }

        public void UnsetConstrainedCell(Coords coords)
        {
            var cell = GetCellAt(coords);
            cell.UnsetConstrained();
        }

        public Cell GetCellAt(Coords coords)
        {
            return allCells.Single(c => c.CellCoords.Equals(coords));
        }

        private void BuildGameBoard()
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    var coords = new Coords(x, y);

                    var newCell = new Cell(
                        rows[coords.Row],
                        columns[coords.Column],
                        quadrants[coords.Quadrant],
                        coords
                    );

                    rows[coords.Row].AddCell(newCell);
                    columns[coords.Column].AddCell(newCell);
                    quadrants[coords.Quadrant].AddCell(newCell);

                    allCells.Add(newCell);
                }
            }
        }

        private void CreateCellGroups()
        {
            for (int i = 0; i < 9; i++)
            {
                rows[i] = new CellGroup();
                columns[i] = new CellGroup();
                quadrants[i] = new CellGroup();
            }
        }
    }
}
