using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
    private int size = 1, gridOffsetX = 0, gridOffsetY = 0;
    private Region grid = new Tile();

    public bool Contains(int x, int y)
    {
        int gridX = x + gridOffsetX, gridY = y + gridOffsetY;
        return 0 <= gridX && gridX < size && 0 <= gridY && gridY < size;
    }

    public void ExpandMap(bool left, bool down)
    {
        int i = left ? 1 : 0, j = down ? 1 : 0;

        Quad quad = new Quad(size);
        quad.branches[i, j] = grid;
        grid = quad;

        gridOffsetX += i * size;
        gridOffsetY += j * size;
        size *= 2;
    }

    public void SetTile(int x, int y, Tile tile)
    {
        if (Contains(x, y))
        {
            grid.SetTile(x + gridOffsetX, y + gridOffsetY, tile);
        }
        else
        {
            ExpandMap(x < 0, y < 0);
            SetTile(x, y, tile);
        }
    }

    public Tile GetTile(int x, int y)
    {
        if (Contains(x, y))
        {
            return grid.GetTile(x + gridOffsetX, y + gridOffsetY);
        }
        else
        {
            return null;
        }
    }

    public void OnDrawGizmos()
    {
        GizmoDrawRegion(0, 0, grid);
    }

    // Returns an array of tiles from (x,y) with the given dimensions.
    //TODO Make it return null on null tile.
    public Tile[,] GetTiles(int x, int y, int width, int length)
    {
        Tile[,] tiles = new Tile[width, length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Tile tile = GetTile(x + i, y + j);
                if (tile == null)
                {
                    tile = new Tile();
                }
                tiles[i, j] = tile;
            }
        }
        return tiles;
    }

    public void GizmoDrawRegion(int x, int y, Region region)
    {
        if (region != null)
        {
            if (region is Tile)
            {
                Gizmos.DrawWireCube(new Vector3(x - gridOffsetX, 0f, y - gridOffsetY), Vector3.one);
            }
            else
            {
                Quad quad = region as Quad;
                Gizmos.DrawWireCube(new Vector3(x + quad.branchSize - gridOffsetX - 0.5f, 0, y + quad.branchSize - gridOffsetY - 0.5f), 2 * quad.branchSize * Vector3.one);
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        GizmoDrawRegion(x + i * quad.branchSize, y + j * quad.branchSize, quad.branches[i, j]);
                    }
                }
            }
        }
    }
}
