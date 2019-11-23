using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuSolver.UI
{
    class GameAdapter
    {
        private SudokuGame game;
        private List<TextBox> allCells = new List<TextBox>();
        private bool UserMode = true;

        public GameAdapter()
        {
            game = new SudokuGame();
        }

        public void BuildGame(Control container)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    TextBox cell = CreateCell(x, y);
                    allCells.Add(cell);
                    cell.Tag = game.GetCellAt(new Coords(x, y));
                    container.Controls.Add(cell);
                }
            }

        }

        public TextBox CreateCell(int x, int y)
        {
            int topOffset = 20;
            int leftOffset = 20;
            int xoffset = ((x / 3) % 3) * 5;
            int yoffset = ((y / 3) % 3) * 5;
            int size = 55;

            TextBox cell = new TextBox();

            cell.Font = new Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cell.Location = new Point(
                leftOffset + xoffset + x * size,
                topOffset + yoffset + y * size
                );
            cell.Name = $"{x}.{y}";
            cell.Size = new Size(size, size);
            cell.TabIndex = y * 9 + y;
            cell.TextAlign = HorizontalAlignment.Center;
            cell.MaxLength = 1;

            cell.TextChanged += (sender, e) =>
            {
                if (!UserMode) return;

                var textcell = (TextBox)sender;
                int newVal;

                textcell.BackColor = Color.White;
                if (string.IsNullOrWhiteSpace(textcell.Text))
                {
                    newVal = 0;
                }
                else
                {
                    var isnumeric = int.TryParse(textcell.Text, out newVal);
                    if (!isnumeric)
                    {
                        textcell.Text = "";
                        newVal = 0;
                    }
                }
                var gameCell = game.GetCellAt(new Coords(x, y));
                gameCell.UnsetConstrained();
                try
                {
                    if (newVal != 0)
                    {
                        gameCell.SetConstrainedValue(newVal);
                        textcell.BackColor = Color.LightGray;
                    }
                    else
                    {
                        gameCell.UnsetConstrained();
                    }
                }
                catch
                {
                    textcell.Text = "";
                }
            };

            return cell;
        }

        public void Solve()
        {
            game.SolveUsingLinearIterator();
            RefreshAllCellsValue();
        }

        private void RefreshAllCellsValue()
        {
            UserMode = false;
            foreach (var c in allCells)
            {
                var gc = (Cell)c.Tag;

                c.Text = gc.CurrentValue.ToString();
            }
            UserMode = true;
        }
    }
}
