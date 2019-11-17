using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    public class Cell
    {
        public Coords CellCoords;
        // zero means unset
        public int CurrentValue { get; private set; } = 0;
        public bool IsConstrained { get; private set; } = false;

        private SortedSet<int> PossibleValues = new SortedSet<int>();
        public int PossibleValuesCount => PossibleValues.Count(n => n > CurrentValue);

        private CellGroup row;
        private CellGroup column;
        private CellGroup quadrant;

        public Cell(CellGroup row, CellGroup column, CellGroup quadrant, Coords cellCoords)
        {
            this.row = row;
            this.column = column;
            this.quadrant = quadrant;
            this.CellCoords = cellCoords;

            for (int i = 9; i > 0; i--)
            {
                PossibleValues.Add(i);
            }
        }

        public void RemovePossibleValue(int value)
        {
            if (value == CurrentValue)
                return;

            PossibleValues.Remove(value);
        }

        public void RestorePossibleValue(int value)
        {
            if (value == CurrentValue)
                return;

            if (ValueIsPossible(value))
                PossibleValues.Add(value);
        }

        private bool ValueIsPossible(int value)
        {
            if (!row.IsValuePossibleForCell(this, value)) return false;
            if (!column.IsValuePossibleForCell(this, value)) return false;
            if (!quadrant.IsValuePossibleForCell(this, value)) return false;

            return true;
        }

        public void TryNextValue()
        {
            int valueToTry;
            try
            {
                valueToTry = PossibleValues.First(n => n > CurrentValue);
            }
            catch
            {
                throw new InvalidOperationException("No more values ahead.");
            }

            SetCellValue(valueToTry);
        }

        private void SetCellValue(int value)
        {
            if (IsConstrained)
                throw new InvalidOperationException("Value cannot be set on constrained cells.");

            if (value != 0 && PossibleValues.All(v => v != value))
                throw new InvalidOperationException($"Value {value} must be within the list of the possible values [{string.Join(", ",PossibleValues)}]");

            var oldValue = CurrentValue;
            CurrentValue = 0;

            row.RestoreValueOnAllCells(oldValue);
            column.RestoreValueOnAllCells(oldValue);
            quadrant.RestoreValueOnAllCells(oldValue);

            CurrentValue = value;

            row.RemoveValueFromAllCells(CurrentValue);
            column.RemoveValueFromAllCells(CurrentValue);
            quadrant.RemoveValueFromAllCells(CurrentValue);
        }

        public void Unset()
        {
            SetCellValue(0);
        }

        public void SetConstrainedValue(int value)
        {
            SetCellValue(value);
            IsConstrained = true;
        }

        public void UnsetConstrained()
        {
            IsConstrained = false;
            SetCellValue(0);
        }

        public override string ToString()
        {
            return $"{CellCoords} - CurrentValue: {CurrentValue} - PossibleValues: [{string.Join(", ", PossibleValues)}] ";
        }

    }
}
