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

    public bool ValidPlayerMove(int x, int y, Team team)
    {
        return IsValid(mapManager.GetQuadTiles(x, y), team);
    }

    // Determines if the tiles are valid for placing a block on from the given team.
    public bool IsValid(Tile[,] tiles, Team team)
    {
        return IsEmpty(tiles) || (IsLevel(tiles) && IsOwned(tiles, team) && IsComposite(tiles));
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
