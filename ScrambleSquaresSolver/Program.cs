using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using ScrambleSquaresSolver.Application;
using ScrambleSquaresSolver.Models;

namespace ScrambleSquaresSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            var text = File.ReadAllText(path);

            var configuredTiles = JsonSerializer.Deserialize<TilesCollection>(text, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            // Get all combinations of tiles
            var tilesToProcess = BuildAllOrientations(configuredTiles.Tiles);
            var i = 0;
            var sw = new Stopwatch();
            sw.Start();

            // Will recursively call itself until a solution is found or all possible attempts have been exhausted.
            var solution = Solver.SolveSync(new Grid(3), tilesToProcess, ref i);
            sw.Stop();

            if (solution == null)
                Console.WriteLine("No Solution Found");
            else
            {
                Console.WriteLine($"We did it in {i} tries - {sw.ElapsedMilliseconds}ms");
                Console.Write(solution.ToString());
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Takes the input file of tiles, and returns all orientations of that tile.
        /// </summary>
        /// <param name="inputTiles"></param>
        /// <returns></returns>
        private static List<Tile> BuildAllOrientations(List<Tile> inputTiles)
        {
            var tilesToProcess = new List<Tile>();
            foreach (var tile in inputTiles)
            {
                tilesToProcess.Add(tile);
                tilesToProcess.Add(new Tile(
                    tile.Id,
                    tile.LeftSide,
                    tile.TopSide,
                    tile.RightSide,
                    tile.BottomSide,
                    Orientation.D90));
                tilesToProcess.Add(new Tile(
                    tile.Id,
                    tile.BottomSide,
                    tile.LeftSide,
                    tile.TopSide,
                    tile.RightSide,
                    Orientation.D180));
                tilesToProcess.Add(new Tile(
                    tile.Id,
                    tile.RightSide,
                    tile.BottomSide,
                    tile.LeftSide,
                    tile.TopSide,
                    Orientation.D270));
            }

            return tilesToProcess;
        }
    }
}
