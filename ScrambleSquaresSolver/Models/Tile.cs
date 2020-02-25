using System;

namespace ScrambleSquaresSolver.Models
{
    /// <summary>
    /// A tile which has 4 sides
    /// </summary>
    public class Tile
    {
        // TODO : Fix the serializer to remove all these setters
        public int Id { get; set; }
        public Side TopSide { get; set; }
        public Side BottomSide { get; set; }
        public Side LeftSide { get; set; }
        public Side RightSide { get; set; }
        public Orientation Orientation { get; set; }

        public Tile()
        {
            // For Deserialization
        }

        public Tile(int id, Side topSide, Side rightSide, Side bottomSide, Side leftSide, Orientation orientation)
        {
            Id = id;
            TopSide = topSide;
            BottomSide = bottomSide;
            LeftSide = leftSide;
            RightSide = rightSide;
            Orientation = orientation;
        }

        public override string ToString()
        {
            return $"{Id}_{Orientation}";
        }
    }

}
