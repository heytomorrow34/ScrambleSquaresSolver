using System;
using System.Collections.Generic;
using System.Linq;
using ScrambleSquaresSolver.Models;

namespace ScrambleSquaresSolver.Application
{
    /// <summary>
    /// Methods to solve the puzzle
    /// </summary>
    public class Solver
    {
        /// <summary>
        /// Returns the first complete grid that is found
        /// </summary>
        /// <param name="currentGrid"></param>
        /// <param name="remainingTiles"></param>
        /// <param name="iteration">The total count of the number of steps this has taken</param>
        /// <returns></returns>
        public static Grid SolveSync(Grid currentGrid, List<Tile> remainingTiles, ref int iteration)
        {
            var nextSpot = currentGrid.FindNextEmptySpot();
            // Puzzle is solved
            if (nextSpot == null)
                return currentGrid;

            var legalTiles = remainingTiles.FindAll(t =>
                currentGrid.IsLegalSpot(nextSpot.Item1, nextSpot.Item2, t));

            //This approach fails, fall back until another legal combination can be found
            if (!legalTiles.Any())
            {
                return null;
            }

            foreach (var tile in legalTiles)
            {
                Console.WriteLine($"Placing Tile {tile} in {nextSpot.Item1},{nextSpot.Item2}");
                iteration++;
                currentGrid[nextSpot.Item1, nextSpot.Item2].PlaceTile(tile);
                // Send all of the tiles that do not match the Id of the one that is placed to the next step
                var result = SolveSync(currentGrid, remainingTiles.FindAll(t => t.Id != tile.Id), ref iteration);
                
                // A solution has been found
                if (result != null)
                    return currentGrid;

                currentGrid[nextSpot.Item1, nextSpot.Item2].RemoveTile();
            }

            // There is no solution from this branch
            return null;
        }
    }
}
