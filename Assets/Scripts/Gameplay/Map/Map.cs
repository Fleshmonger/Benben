using UnityEngine;
using Geometry;
using Datastructures;

namespace Gameplay.Map
{
    /// <summary> Data class that contains an expanding quad-tree of tiles. </summary>
    public class Map : Singleton<Map>
    {
        private QuadtreeExpanding<Tile> _quadtree = new QuadtreeExpanding<Tile>();

        /// <summary> Returns whether the map contains any tiles. </summary>
        public bool IsEmpty()
        {
            return _quadtree.Dimensions.Area <= 1;
        }

        /// <summary> Returns the tile at the given x, y coordinates. </summary>
        public Tile GetTile(int x, int y)
        {
            return _quadtree.GetValue(x, y);
        }

        /// <summary> Returns the tile at the given position. </summary>
        public Tile GetTile(Vector2I point)
        {
            return GetTile(point.x, point.y);
        }

        /// <summary>
        /// Sets the tile at the given x, y coordinates.
        /// </summary>
        public void SetTile(Tile tile, int x, int y)
        {
            _quadtree.SetValue(x, y, tile);
        }

        /// <summary> Sets the tile at a given point. </summary>
        public void SetTile(Tile tile, Vector2I point)
        {
            SetTile(tile, point.x, point.y);
        }

        /// <summary> Draws Gizmos of a region and its contents recursively </summary>
        private void GizmoDrawRegion(int x, int y, Region<Tile> region)
        {
            if (region == null)
            {
                return;
            }

            var split = region as Split<Tile>;
            if (split != null)
            {
                var dimensions = split.Dimensions;
                Gizmos.DrawWireCube(new Vector2(x + dimensions.Width / 2f, y + dimensions.Height / 2f), dimensions);
                GizmoDrawRegion(x, y, split.FirstRegion);
                if (split.Orientation == Split<Tile>.Partition.Horizontal)
                {
                    GizmoDrawRegion(x + split.SplitLength, y, split.SecondRegion);
                }
                else
                {
                    GizmoDrawRegion(x, y + split.SplitLength, split.SecondRegion);
                }
            }
            else
            {
                Gizmos.DrawWireCube(new Vector3(x + .5f, y + .5f), Vector3.one);
            }
        }
        
        /// <summary> Gizmo draw callback. </summary>
        private void OnDrawGizmos()
        {
            GizmoDrawRegion(_quadtree.Origin.x, _quadtree.Origin.y, _quadtree._values);
        }
    }

}