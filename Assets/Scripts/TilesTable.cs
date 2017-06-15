using System.Collections.Generic;
using WaveFunctionCollapse.MeshTools;

namespace WaveFunctionCollapse
{
    public class TilesTable
    {
        Dictionary<int, List<Tile>> edgesHashtable;

        public TilesTable()
        {
            edgesHashtable = new Dictionary<int, List<Tile>>();
        }

        public void Add(Tile tile)
        {
            foreach (var edge in tile.Edges)
            {
                RegisterEdge(edge, tile);
            }
        }

        public void AddRange(IEnumerable<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                Add(tile);
            }
        }

        public Tile[] Get(int edgeHash)
        {
            if (edgesHashtable.ContainsKey(edgeHash))
            {
                return edgesHashtable[edgeHash].ToArray();
            }

            return new Tile[0];
        }

        private void RegisterEdge(MeshEdge edge, Tile tile)
        {
            if (edge == null || edge.Empty) return;

            int edgeHash = edge.GetHashCode();

            if (edgesHashtable.ContainsKey(edgeHash) == false)
            {
                edgesHashtable[edgeHash] = new List<Tile>();
            }

            edgesHashtable[edgeHash].Add(tile);
        }
    }
}
