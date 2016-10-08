using UnityEngine;
using System.Collections;

public class RulesManager : MonoBehaviour
{
    public MapManager mapManager;

    private int GetTileHeight(Tile tile)
    {
        if (tile == null)
        {
            return Tile.NULL_HEIGHT;
        }
        else
        {
            return tile.height;
        }
    }

    // Determines if the tiles are valid for placing a block with a given size from the given team.
    public bool IsValid(int x, int y, int size, Team team)
    {
        Tile[,] tiles = mapManager.GetTiles(x, y, size, size);
        return (IsEmpty(tiles)) || (IsLevel(tiles) && IsOwned(tiles, team) && IsComposite(tiles));
    }

    // Determines if all the tiles are empty.
    public bool IsEmpty(Tile[,] tiles)
    {
        int width = tiles.GetLength(0), length = tiles.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Tile tile = tiles[i, j];
                if (tile != null && tile.height > 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Determines if any of the tiles is adjacent to existing tiles.
    public bool IsAdjacent(int x, int y, int size)
    {
        Vector2[] sides = new Vector2[4];
        sides[0] = new Vector2(x - 1, y - 1);
        sides[1] = new Vector2(x - 1, y + size);
        sides[2] = new Vector2(x + size, y + size);
        sides[3] = new Vector2(x + size, y - 1);
        Vector2[] directions = new Vector2[4];
        directions[0] = Vector2.right;
        directions[1] = Vector2.up;
        directions[2] = Vector2.left;
        directions[3] = Vector2.down;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                sides[j] += directions[j];
                Vector2 point = sides[j];
                Tile tile = mapManager.GetTile((int)point.x, (int)point.y);
                if (tile != null && tile.height > Tile.NULL_HEIGHT)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Determines if any of the tiles match the team.
    public bool IsOwned(Tile[,] tiles, Team team)
    {
        int width = tiles.GetLength(0), length = tiles.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Tile tile = tiles[i, j];
                if (tile != null && tile.team == team)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Determines if the tiles are of the same height.
    public bool IsLevel(Tile[,] tiles)
    {
        int width = tiles.GetLength(0), length = tiles.GetLength(1);
        int height = GetTileHeight(tiles[0, 0]);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                if (height != GetTileHeight(tiles[i, j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Determines if the tiles contains two different blocks.
    public bool IsComposite(Tile[,] tiles)
    {
        int width = tiles.GetLength(0), length = tiles.GetLength(1);
        GameObject prop = null;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Tile tile = tiles[i, j];
                if (tile != null && tile.prop != null)
                {
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
        }
        return false;
    }
}
