using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour
{
    private struct Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Translate(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public void Translate(Point other)
        {
            Translate(other.x, other.y);
        }
    }

    private int size = 1;
    private Point mapOffset = new Point(0, 0);
    private Region map = new Tile();

    private int WorldToMapX(int x)
    {
        return x - mapOffset.x;
    }

    private int WorldToMapY(int y)
    {
        return y - mapOffset.y;
    }

    public bool IsEmpty()
    {
        return size == 1;
    }

    public bool Contains(int x, int y)
    {
        int mapX = WorldToMapX(x), mapY = WorldToMapY(y);
        return 0 <= mapX && mapX < size && 0 <= mapY && mapY < size;
    }

    public void ExpandMap(bool left, bool down)
    {
        int i = left ? 1 : 0, j = down ? 1 : 0;

        Quad quad = new Quad(size);
        quad.branches[i, j] = map;
        map = quad;

        mapOffset.Translate(-i * size, -j * size);
        size *= 2;
    }

    public void SetTile(int x, int y, Tile tile)
    {
        if (Contains(x, y))
        {
            map.SetTile(WorldToMapX(x), WorldToMapY(y), tile);
        }
        else
        {
            ExpandMap(WorldToMapX(x) < 0, WorldToMapY(y) < 0);
            SetTile(x, y, tile);
        }
    }

    public Tile GetTile(int x, int y)
    {
        if (Contains(x, y))
        {
            return map.GetTile(WorldToMapX(x), WorldToMapY(y));
        }
        else
        {
            return null;
        }
    }

    public void OnDrawGizmos()
    {
        GizmoDrawRegion(0, 0, map);
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
                Gizmos.DrawWireCube(new Vector3(x + mapOffset.x, 0f, y + mapOffset.y), Vector3.one);
            }
            else
            {
                Quad quad = region as Quad;
                Gizmos.DrawWireCube(new Vector3(x + quad.branchSize + mapOffset.x - 0.5f, 0, y + quad.branchSize + mapOffset.y - 0.5f), 2 * quad.branchSize * Vector3.one);
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
