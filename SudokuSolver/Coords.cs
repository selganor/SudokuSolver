using System;

namespace SudokuSolver
{
    public class Coords:IEquatable<Coords>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int LinearPosition => X * Y;
        public int Row => Y;
        public int Column => X;
        public int Quadrant
        {
            get
            {
                if (X >= 0 && X <= 2 && Y >= 0 && Y <= 2) return 0;
                if (X >= 3 && X <= 5 && Y >= 0 && Y <= 2) return 1;
                if (X >= 6 && X <= 8 && Y >= 0 && Y <= 2) return 2;
                if (X >= 0 && X <= 2 && Y >= 3 && Y <= 5) return 3;
                if (X >= 3 && X <= 5 && Y >= 3 && Y <= 5) return 4;
                if (X >= 6 && X <= 8 && Y >= 3 && Y <= 5) return 5;
                if (X >= 0 && X <= 2 && Y >= 6 && Y <= 8) return 6;
                if (X >= 3 && X <= 5 && Y >= 6 && Y <= 8) return 7;
                if (X >= 6 && X <= 8 && Y >= 6 && Y <= 8) return 8;
                return -1;
            }
        }

        public Coords(int x, int y)
        {
            if (x < 0 || x > 8 || y < 0 || y > 8)
                throw new InvalidOperationException($"Both x and y must be between 0 and 8 [{x}, {y}]");

            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public bool Equals(Coords other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coords) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }

}
