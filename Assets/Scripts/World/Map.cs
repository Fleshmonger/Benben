using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    private int size = 1, offsetX = 0, offsetY = 0;
    private Region grid = new Tile();

    public bool Contains(int x, int y)
    {
        int gridX = x + offsetX, gridY = y + offsetY;
        return 0 <= gridX && gridX < size && 0 <= gridY && gridY < size;
    }

    public void ExpandMap(bool left, bool down)
    {
        int i = left ? 1 : 0, j = down ? 1 : 0;

        Quad quad = new Quad(size);
        quad.branches[i, j] = grid;
        grid = quad;

        offsetX += i * size;
        offsetY += j * size;
        size *= 2;
    }

    public void SetTile(int x, int y, Tile tile)
    {
        if (Contains(x, y))
        {
            grid.SetTile(x + offsetX, y + offsetY, tile);
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
            return grid.GetTile(x + offsetX, y + offsetY);
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

    public void GizmoDrawRegion(int x, int y, Region region)
    {
        if (region != null)
        {
            if (region is Tile)
            {
                Gizmos.DrawWireCube(new Vector3(x - offsetX, 0f, y - offsetY), Vector3.one);
            }
            else
            {
                Quad quad = region as Quad;
                Gizmos.DrawWireCube(new Vector3(x + quad.branchSize - offsetX - 0.5f, 0, y + quad.branchSize - offsetY - 0.5f), 2 * quad.branchSize * Vector3.one);
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
