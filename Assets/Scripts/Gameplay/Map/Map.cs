using UnityEngine;
using Geometry;

namespace Gameplay.Map
{
    /// <summary> Data class that contains an expanding quad-tree of tiles. </summary>
    public class Map : Singleton<Map>
    {
        private int _size = 1;
        private Vector2I _mapOffset;
        private Region _tiles = new Tile();

        /// <summary> Returns whether the map contains any tiles. </summary>
        public bool IsEmpty()
        {
            return _size == 1;
        }

        /// <summary> Returns the tile at the given position. </summary>
        public Tile GetTile(int x, int y)
        {
            if (Contains(x, y))
            {
                return _tiles.GetTile(WorldToMapX(x), WorldToMapY(y));
            }
            return null;
        }

        /// <summary> Returns the tile at the given position. </summary>
        public Tile GetTile(Vector2I point)
        {
            return GetTile(point.x, point.y);
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

        /// <summary> Returns the depth of a tile at the given position. </summary>
        public int GetDepth(int x, int y)
        {
            if (Contains(x, y))
            {
                return _tiles.GetDepth(WorldToMapX(x), WorldToMapY(y));
            }
            return 0;
        }

        /// <summary> Returns the depth of a tile at the given position. </summary>
        public int GetDepth(Vector2I point)
        {
            return GetDepth(point.x, point.y);
        }

        /// <summary> Sets the tile at a given position. </summary>
        public void SetTile(Tile tile, int x, int y)
        {
            while (!Contains(x, y))
            {
                ExpandMap(WorldToMapX(x) < 0, WorldToMapY(y) < 0);
            }
            _tiles.SetTile(WorldToMapX(x), WorldToMapY(y), tile);
        }

        /// <summary> Sets the tile at a given position. </summary>
        public void SetTile(Tile tile, Vector2I point)
        {
            SetTile(tile, point.x, point.y);
        }
        
        /// <summary> Draws Gizmos of a region and its contents recursively </summary>
        public void GizmoDrawRegion(int x, int y, Region region)
        {
            if (region == null)
            {
                return;
            }

            var quad = region as Quad;
            if (quad != null)
            {
                Gizmos.DrawWireCube(new Vector3(x + quad.branchSize + _mapOffset.x - 0.5f, y + quad.branchSize + _mapOffset.y - 0.5f, 0), 2 * quad.branchSize * Vector3.one);
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
                Gizmos.DrawWireCube(new Vector3(x + _mapOffset.x, y + _mapOffset.y, 0), Vector3.one);
            }
        }
        
        /// <summary> Expands the quadtree in given directions. </summary>
        private void ExpandMap(bool left, bool down)
        {
            var i = left ? 1 : 0;
            var j = down ? 1 : 0;

            var quad = new Quad(_size);
            quad.branches[i, j] = _tiles;
            _tiles = quad;

            _mapOffset -= new Vector2I(i * _size, j * _size);
            _size *= 2;
        }

        private int WorldToMapX(int x)
        {
            return x - _mapOffset.x;
        }

        private int WorldToMapY(int y)
        {
            return y - _mapOffset.y;
        }

        /// <summary> Returns whether the quadtree contains the point. </summary>
        private bool Contains(int x, int y)
        {
            var mapX = WorldToMapX(x);
            var mapY = WorldToMapY(y);
            return 0 <= mapX && mapX < _size && 0 <= mapY && mapY < _size;
        }

        /// <summary> Returns whether the quadtree contains the point. </summary>
        private bool Contains(Vector2I point)
        {
            return Contains(point.x, point.y);
        }

        /// <summary> Gizmo draw callback. </summary>
        private void OnDrawGizmos()
        {
            GizmoDrawRegion(0, 0, _tiles);
        }
    }

}