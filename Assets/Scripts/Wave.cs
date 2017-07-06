using UnityEngine;
using System.Collections.Generic;

namespace WaveFunctionCollapse
{
    public class Wave
    {
        Dictionary<Tile, int> tilesLookup;
        Tile[] tiles;
        bool[,,,] field;                        //values are inverted to save time on initialization

        public Wave(int sizeX, int sizeY, int sizeZ, Tile[] tiles)
        {
            this.tiles = tiles;
            tilesLookup = new Dictionary<Tile, int>();
            int tileIndex = 0;
            foreach (var tile in tiles)
            {
                tilesLookup[tile] = tileIndex;
                tileIndex++;
            }

            field = new bool[sizeX, sizeY, sizeZ, tiles.Length];
        }


        public void SetTileState(Position3D pos, Tile tile, bool possible)
        {
            SetTileState(pos.X, pos.Y, pos.Z, tile, possible);
        }

        public void SetTileState(int x, int y, int z, Tile tile, bool possible)
        {
            int tileIndex = tilesLookup[tile];

            field[x, y, z, tileIndex] = !possible;
        }


        public int GetPossibleTilesCount(Position3D pos)
        {
            return GetPossibleTilesCount(pos.X, pos.Y, pos.Z);
        }

        public int GetPossibleTilesCount(int x, int y, int z)
        {
            int result = 0;
            int totalTiles = tiles.Length;
            for (int i = 0; i < totalTiles; i++)
            {
                if (field[x, y, z, i] == false) result++;
            }

            return result;
        }


        public Tile[] GetPossibleTiles(Position3D pos)
        {
            return GetPossibleTiles(pos.X, pos.Y, pos.Z);
        }

        public Tile[] GetPossibleTiles(int x, int y, int z)
        {
            List<Tile> result = new List<Tile>();

            int totalTiles = tiles.Length;
            for (int i = 0; i < totalTiles; i++)
            {
                if (field[x, y, z, i] == false)
                {
                    result.Add(tiles[i]);
                }
            }

            return result.ToArray();
        }
    }
}
