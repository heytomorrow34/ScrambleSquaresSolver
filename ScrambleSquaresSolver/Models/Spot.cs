namespace ScrambleSquaresSolver.Models
{
    /// <summary>
    /// A spot which can contain a tile
    /// </summary>
   public class Spot
    {
        public Tile Tile { get; private set; }

        public bool HasTile => Tile != null;

        public Side TopSide => Tile?.TopSide;
        public Side BottomSide => Tile?.BottomSide;
        public Side LeftSide => Tile?.LeftSide;
        public Side RightSide => Tile?.RightSide;

        public Spot()
        {

        }

        public void PlaceTile(Tile tile)
        {
            Tile = tile;
        }

        public void RemoveTile()
        {
            Tile = null;
        }


    }
}
