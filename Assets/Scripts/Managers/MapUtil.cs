using UnityEngine;
using Geometry;

namespace Gameplay.Map
{
    public static class MapUtil
    {
        // Determines if the tiles are valid for placing a block with a given size from the given team.
        public static bool IsValid(int x, int y, int size, Team team, Map map)
        {
            var tiles = new Tile[size, size];
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    tiles[i, j] = map.GetTile(x + i, y + j);
                }
            }
            var grounded = IsEmpty(tiles) && (map.IsEmpty() || IsAdjacent(x, y, size, map));
            var stacked = IsLevel(tiles) && IsOwned(tiles, team) && IsComposite(tiles);
            return grounded || stacked;
        }

        public static bool IsValid(Vector2I point, int size, Team team, Map map)
        {
            return IsValid(point.x, point.y, size, team, map);
        }

        // Determines if all the tiles are empty.
        public static bool IsEmpty(Tile[,] tiles)
        {
            var size = new Size(tiles.GetLength(0), tiles.GetLength(1));
            for (var i = 0; i < size.Width; i++)
            {
                for (var j = 0; j < size.Height; j++)
                {
                    var tile = tiles[i, j];
                    if (tile.Height > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Determines if any of the tiles is adjacent to existing tiles.
        public static bool IsAdjacent(int x, int y, int size, Map map)
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
                    if (tile.Height > 0)
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
            var size = new Size(tiles.GetLength(0), tiles.GetLength(1));
            for (var i = 0; i < size.Width; i++)
            {
                for (var j = 0; j < size.Height; j++)
                {
                    var tile = tiles[i, j];
                    if (tile.Team == team)
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
            var size = new Size(tiles.GetLength(0), tiles.GetLength(1));
            var depth = tiles[0, 0].Height;
            for (var i = 0; i < size.Width; i++)
            {
                for (var j = 0; j < size.Height; j++)
                {
                    if (depth != tiles[i, j].Height)
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
            var size = new Size(tiles.GetLength(0), tiles.GetLength(1));
            GameObject prop = null;
            for (var i = 0; i < size.Width; i++)
            {
                for (var j = 0; j < size.Height; j++)
                {
                    var tile = tiles[i, j];
                    if (tile.Prop == null)
                    {
                        continue;
                    }
                    if (prop == null)
                    {
                        prop = tile.Prop;
                    }
                    else if (tile.Prop != prop)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}