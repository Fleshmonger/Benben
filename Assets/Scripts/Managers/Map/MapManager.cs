using UnityEngine;
using System.Collections;

public class MapManager : Singleton<MapManager>
{
    private int size = 1;
    private Point2 mapOffset;
    private Region tiles = new Tile();

    public bool IsEmpty()
    {
        return size == 1;
    }

    public bool Contains(int x, int y)
    {
        var mapX = WorldToMapX(x);
        var mapY = WorldToMapY(y);
        return 0 <= mapX && mapX < size && 0 <= mapY && mapY < size;
    }

    public bool Contains(Point2 point)
    {
        return Contains(point.x, point.y);
    }

    public Tile GetTile(int x, int y)
    {
        if (Contains(x, y))
        {
            return tiles.GetTile(WorldToMapX(x), WorldToMapY(y));
        }
        return null;
    }

    // Returns an array of tiles from (x, y) with the given dimensions.
    //TODO Make it return null on null tile.
    public Tile[,] GetTiles(int x, int y, int width, int length)
    {
        var tiles = new Tile[width, length];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < length; j++)
            {
                var tile = GetTile(x + i, y + j);
                if (tile == null)
                {
                    tile = new Tile();
                }
                tiles[i, j] = tile;
            }
        }
        return tiles;
    }

    public void SetTile(int x, int y, Tile tile)
    {
        if (Contains(x, y))
        {
            tiles.SetTile(WorldToMapX(x), WorldToMapY(y), tile);
        }
        else
        {
            ExpandMap(WorldToMapX(x) < 0, WorldToMapY(y) < 0);
            SetTile(x, y, tile);
        }
    }

    public void SetTile(Point2 point, Tile tile)
    {
        SetTile(point.x, point.y, tile);
    }

    // Recursive gizmo draw method. Draws a region and whatever it contains.
    public void GizmoDrawRegion(int x, int y, Region region)
    {
        if (region == null)
        {
            return;
        }

        var quad = region as Quad;
        if (quad != null)
        {
            Gizmos.DrawWireCube(new Vector3(x + quad.branchSize + mapOffset.x - 0.5f, 0, y + quad.branchSize + mapOffset.y - 0.5f), 2 * quad.branchSize * Vector3.one);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    GizmoDrawRegion(x + i * quad.branchSize, y + j * quad.branchSize, quad.branches[i, j]);
                }
            }
        }
        else
        {
            Gizmos.DrawWireCube(new Vector3(x + mapOffset.x, 0f, y + mapOffset.y), Vector3.one);
        }
    }

    // Expands the map size.
    private void ExpandMap(bool left, bool down)
    {
        var i = left ? 1 : 0;
        var j = down ? 1 : 0;

        var quad = new Quad(size);
        quad.branches[i, j] = tiles;
        tiles = quad;

        mapOffset -= new Point2(i * size, j * size);
        size *= 2;
    }

    private int WorldToMapX(int x)
    {
        return x - mapOffset.x;
    }

    private int WorldToMapY(int y)
    {
        return y - mapOffset.y;
    }

    // Gizmo draw callback.
    public void OnDrawGizmos()
    {
        GizmoDrawRegion(0, 0, tiles);
    }
}
