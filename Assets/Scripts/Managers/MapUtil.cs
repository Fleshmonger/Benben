using UnityEngine;

namespace GameLogic
{
    public static class MapUtil
    {
        // Determines if the tiles are valid for placing a block with a given size from the given team.
        public static bool IsValid(int x, int y, int size, Team team, MapManager map)
        {
            var tiles = map.GetTiles(x, y, size, size);
            var grounded = IsEmpty(tiles) && (map.IsEmpty() || IsAdjacent(x, y, size, map));
            var stacked = IsLevel(tiles) && IsOwned(tiles, team) && IsComposite(tiles);
            return grounded || stacked;
        }

        // Determines if all the tiles are empty.
        public static bool IsEmpty(Tile[,] tiles)
        {
            var size = new Size(tiles);
            for (var i = 0; i < size.width; i++)
            {
                for (var j = 0; j < size.height; j++)
                {
                    var tile = tiles[i, j];
                    if (tile != null && tile.height > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Determines if any of the tiles is adjacent to existing tiles.
        public static bool IsAdjacent(int x, int y, int size, MapManager map)
        {
            var sides = new Vector2[4];
            sides[0] = new Vector2(x - 1, y - 1);
            sides[1] = new Vector2(x + size, y - 1);
            sides[2] = new Vector2(x + size, y + size);
            sides[3] = new Vector2(x - 1, y + size);
            var directions = new Vector2[4];
            directions[0] = Vector2.right;
            directions[1] = Vector2.up;
            directions[2] = Vector2.left;
            directions[3] = Vector2.down;
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    sides[j] += directions[j];
                    var point = sides[j];
                    var tile = map.GetTile((int)point.x, (int)point.y);
                    if (tile != null && tile.height > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Determines if any of the tiles match the team.
        public static bool IsOwned(Tile[,] tiles, Team team)
        {
            var size = new Size(tiles);
            for (var i = 0; i < size.width; i++)
            {
                for (var j = 0; j < size.height; j++)
                {
                    var tile = tiles[i, j];
                    if (tile != null && tile.team == team)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Determines if the tiles are of the same height.
        public static bool IsLevel(Tile[,] tiles)
        {
            var size = new Size(tiles);
            var depth = GetTileDepth(tiles[0, 0]);
            for (var i = 0; i < size.width; i++)
            {
                for (var j = 0; j < size.height; j++)
                {
                    if (depth != GetTileDepth(tiles[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Determines if the tiles contains two different blocks.
        public static bool IsComposite(Tile[,] tiles)
        {
            var size = new Size(tiles);
            GameObject prop = null;
            for (var i = 0; i < size.width; i++)
            {
                for (var j = 0; j < size.height; j++)
                {
                    var tile = tiles[i, j];
                    if (tile == null || tile.prop == null)
                    {
                        continue;
                    }
                    if (prop == null)
                    {
                        prop = tile.prop;
                    }
                    else if (tile.prop != prop)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static int GetTileDepth(Tile tile)
        {
            if (tile != null)
            {
                return tile.height;
            }
            return 0;
        }
    }
}