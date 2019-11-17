namespace SudokuSolver
{
    /// <summary>
    /// Represents the last cell in the iteration
    /// </summary>
    public class AfterLastCell : Cell {
        public AfterLastCell() : base(null, null, null, null)
        {
        }
    }

    /// <summary>
    /// Represents the last cell in the iteration
    /// </summary>
    public class BeforeFirstCell : Cell
    {
        public BeforeFirstCell() : base(null, null, null, null)
        {
        }
    }

    /// <summary>
    /// Represents a way to traverse all cells of a SudokuGame
    /// </summary>
    public interface ICellIterator
    {
        /// <summary>
        /// Returns the next cell. If reached the end 
        /// </summary>
        /// <returns></returns>
        Cell GetNext();

        /// <summary>
        /// Returns the previous value in the iteration
        /// </summary>
        /// <returns></returns>
        Cell GetPrevious();
    }
}
