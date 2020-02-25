using System;
using System.Text;

namespace ScrambleSquaresSolver.Models
{
    /// <summary>
    /// The square grid that contains spots which can contain tiles
    /// </summary>
    public class Grid
    {
        private readonly int _sideLength;
        private readonly Spot[,] _grid;
        public Grid(int size)
        {
            _sideLength = size;
            _grid = new Spot[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _grid[i, j] = new Spot();
                }
            }
        }

        public Spot this[int i, int j] => _grid[i, j];

        /// <summary>
        /// Returns the coordinates of the next empty spot, or null if there is none.
        /// </summary>
        /// <returns></returns>
        public Tuple<int,int> FindNextEmptySpot()
        {
            for (int i = 0; i < _sideLength; i++)
            {
                for (int j = 0; j < _sideLength; j++)
                {
                    if (!_grid[i, j].HasTile)
                        return new Tuple<int, int>(i, j);
                }
            }
            return null;
        }

        /// <summary>
        /// Returns true if the coordinates are a legal spot for the provided tile.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public bool IsLegalSpot(int x, int y, Tile tile)
        {
            if (GetSpotAbove(x, y)?.BottomSide?.IsLegalMatch(tile.TopSide) == false)
                return false;

            if (GetSpotBelow(x, y)?.TopSide?.IsLegalMatch(tile.BottomSide) == false)
                return false;

            if (GetSpotRight(x, y)?.LeftSide?.IsLegalMatch(tile.RightSide) == false)
                return false;

            if (GetSpotLeft(x, y)?.RightSide?.IsLegalMatch(tile.LeftSide) == false)
                return false;

            return true;
        }

        public Grid Clone()
        {
            var newGrid = new Grid(_sideLength);
            for (int i = 0; i < _sideLength; i++)
            {
                for (int j = 0; j < _sideLength; j++)
                {
                    newGrid[i,j].PlaceTile(this[i,j].Tile);
                }
            }

            return newGrid;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int j = 0; j < _sideLength; j++)
            {
                for (int i = 0; i < _sideLength; i++)
                {
                    if (_grid[i,j].HasTile)
                        sb.Append(_grid[i,j].Tile + "\t");
                    else
                    {
                        sb.Append("     ");
                    }
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }

        private bool ValidateIndices(int x, int y)
        {
            if (x < 0 || x >= _sideLength)
                return false;
            if (y < 0 || y >= _sideLength)
                return false;
            return true;
        }
        private Spot GetSpotAbove(int x, int y)
        {
            if (!ValidateIndices(x, y))
                return null;

            if (y == 0)
                return null;
            return _grid[x, y - 1];
        }

        private Spot GetSpotBelow(int x, int y)
        {
            if (!ValidateIndices(x, y))
                return null;

            if (y == _sideLength - 1)
                return null;
            return _grid[x, y + 1];
        }

        private Spot GetSpotRight(int x, int y)
        {
            if (!ValidateIndices(x, y))
                return null;

            if (x == _sideLength - 1)
                return null;
            return _grid[x + 1, y];
        }
        private Spot GetSpotLeft(int x, int y)
        {
            if (!ValidateIndices(x, y))
                return null;

            if (x == 0)
                return null;
            return _grid[x - 1, y];
        }
    }
}
