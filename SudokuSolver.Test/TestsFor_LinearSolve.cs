using NUnit.Framework;

namespace SudokuSolver.Test
{
    [TestFixture]
    public class TestsFor_LinearSolve
    {

        [Test]
        public void An_empty_game_should_be_solved()
        {
            var game = new SudokuGame();
            var iterator = game.GetLinearIterator();
            var solver = new SudokuGameSolver(iterator, game);
            var result = solver.Solve();
            Assert.AreEqual(SolveResults.Solved, result);
        }

        [Test]
        public void A_game_with_some_constraints_should_be_solved()
        {
            var game = new SudokuGame();
            game.SetConstrainedCell(new Coords(2, 2), 5);
            game.SetConstrainedCell(new Coords(5, 5), 5);
            game.SetConstrainedCell(new Coords(8, 8), 5);
            game.UnsetConstrainedCell(new Coords(8, 8));

            game.SetConstrainedCell(new Coords(1, 1), 1);
            game.SetConstrainedCell(new Coords(3, 1), 2);
            game.SetConstrainedCell(new Coords(5, 1), 3);
            game.SetConstrainedCell(new Coords(1, 3), 4);
            game.SetConstrainedCell(new Coords(3, 3), 6);
            game.SetConstrainedCell(new Coords(5, 3), 7);

            var iterator = game.GetLinearIterator();
            var solver = new SudokuGameSolver(iterator, game);
            var result = solver.Solve();

            Assert.AreEqual(SolveResults.Solved, result);
            Assert.AreEqual(5, game.GetCellAt(new Coords(2, 2)).CurrentValue);
            Assert.AreEqual(5, game.GetCellAt(new Coords(5, 5)).CurrentValue);
            Assert.AreNotEqual(5, game.GetCellAt(new Coords(8, 8)).CurrentValue);
        }

        [Test]
        public void It_must_resolve_a_real_sudoku()
        {
            var game = new SudokuGame();
            game.SetConstrainedCell(new Coords(0, 0), 4);
            game.SetConstrainedCell(new Coords(0, 3), 3);
            game.SetConstrainedCell(new Coords(0, 5), 7);
            game.SetConstrainedCell(new Coords(1, 2), 6);
            game.SetConstrainedCell(new Coords(1, 3), 1);
            game.SetConstrainedCell(new Coords(1, 8), 3);
            game.SetConstrainedCell(new Coords(2, 4), 4);
            game.SetConstrainedCell(new Coords(2, 5), 8);
            game.SetConstrainedCell(new Coords(2, 8), 7);
            game.SetConstrainedCell(new Coords(3, 0), 7);
            game.SetConstrainedCell(new Coords(3, 1), 8);
            game.SetConstrainedCell(new Coords(3, 4), 6);
            game.SetConstrainedCell(new Coords(3, 6), 1);
            game.SetConstrainedCell(new Coords(3, 8), 9);
            game.SetConstrainedCell(new Coords(4, 2), 9);
            game.SetConstrainedCell(new Coords(4, 6), 4);
            game.SetConstrainedCell(new Coords(5, 0), 2);
            game.SetConstrainedCell(new Coords(5, 2), 1);
            game.SetConstrainedCell(new Coords(5, 4), 8);
            game.SetConstrainedCell(new Coords(5, 7), 7);
            game.SetConstrainedCell(new Coords(5, 8), 6);
            game.SetConstrainedCell(new Coords(6, 0), 9);
            game.SetConstrainedCell(new Coords(6, 3), 8);
            game.SetConstrainedCell(new Coords(6, 4), 1);
            game.SetConstrainedCell(new Coords(7, 0), 8);
            game.SetConstrainedCell(new Coords(7, 5), 6);
            game.SetConstrainedCell(new Coords(7, 6), 9);
            game.SetConstrainedCell(new Coords(8, 3), 7);
            game.SetConstrainedCell(new Coords(8, 5), 2);
            game.SetConstrainedCell(new Coords(8, 8), 5);

            var result = game.SolveUsingLinearIterator();

            Assert.AreEqual(SolveResults.Solved, result);
        }
    }
}
