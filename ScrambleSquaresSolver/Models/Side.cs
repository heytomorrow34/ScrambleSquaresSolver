using ScrambleSquaresSolver.Constants;

namespace ScrambleSquaresSolver.Models
{
    public class Side
    {
        //TODO: Fix the serializer to get rid of these setters.

        /// <summary>
        /// There are two halves to each pattern and this indicates which one it is.
        /// </summary>
        public SidePattern SidePattern { get; set; }

        /// <summary>
        /// The primary pattern a side can have.
        /// </summary>
        public int Pattern { get; set; }

        public Side()
        {
            
        }

        public Side(SidePattern sidePattern, int pattern)
        {
            SidePattern = sidePattern;
            Pattern = pattern;
        }

        /// <summary>
        /// Returns true if the other side will be a legal position
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsLegalMatch(Side other)
        {
            // If null, there is no adjacent tile and it is legal
            if (other == null)
                return true;

            // A legal position matches the pattern but not the sidePattern
            return this.SidePattern != other.SidePattern && this.Pattern == other.Pattern;
        }

        public override string ToString()
        {
            return $"{Pattern}_{SidePattern}";
        }
    }
}
