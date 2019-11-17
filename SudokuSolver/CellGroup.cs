using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class CellGroup
    {
        private readonly HashSet<Cell> cells = new HashSet<Cell>();

        public void AddCell(Cell cell)
        {
            if (cells.Count == 9)
                throw new InvalidOperationException("A Cell Group cannot contain more than 9 cells");

            cells.Add(cell);
        }

        public bool IsValuePossibleForCell(Cell cell, int value)
        {
            return cells.All(c => c.CurrentValue != value);
        }

        public void RestoreValueOnAllCells(int value)
        {
            foreach (var c in cells)
            {
                c.RestorePossibleValue(value);
            }
        }

        public void RemoveValueFromAllCells(int value)
        {
            foreach (var c in cells)
            {
                c.RemovePossibleValue(value);
            }
        }
    }

}
