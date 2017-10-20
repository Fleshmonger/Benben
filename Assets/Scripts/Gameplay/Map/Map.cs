using UnityEngine;
using Geometry;
using Datastructures;

namespace Gameplay.Map
{
    /// <summary> Data class that contains an expanding quad-tree of tiles. </summary>
    public sealed class Map : Singleton<Map>
    {
        #region Fields

        private QuadtreeExpanding<Tile> _quadtree = new QuadtreeExpanding<Tile>();

        #endregion

        #region Properties

        /// <summary>
        /// Current dimensions spanning the map.
        /// </summary>
        public Rect Dimensions { get; private set; }

        #endregion

        #region Methods

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

        /// <summary> Returns the dimensions of the map. </summary>
        public Size2I GetDimensions()
        {
            return _quadtree.Dimensions;
        }

        /// <summary> Returns the position of the top-left corner of the map. </summary>
        public Vector2I GetOrigin()
        {
            return _quadtree.Origin;
        }

        /// <summary> Sets the tile at the given x, y coordinates. </summary>
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
        private void GizmoDrawRegion(int x, int y, Region<Tile> region, float scale)
        {
            if (region == null)
            {
                return;
            }

            var split = region as Split<Tile>;
            if (split != null)
            {
                var dimensions = split.Dimensions;
                Gizmos.DrawWireCube(
                    new Vector2(scale * (x + dimensions.Width / 2f), scale * (y + dimensions.Height / 2f)),
                    (int)scale * dimensions);
                GizmoDrawRegion(x, y, split.FirstRegion, scale);
                if (split.Orientation == Split<Tile>.Partition.Horizontal)
                {
                    GizmoDrawRegion(x + split.SplitLength, y, split.SecondRegion, scale);
                }
                else
                {
                    GizmoDrawRegion(x, y + split.SplitLength, split.SecondRegion, scale);
                }
            }
            else
            {
                Gizmos.DrawWireCube(new Vector3(scale * (x + .5f), scale * (y + .5f)), scale * Vector3.one);
            }
        }
        
        /// <summary> Gizmo draw callback. </summary>
        private void OnDrawGizmos()
        {
            GizmoDrawRegion(_quadtree.Origin.x, _quadtree.Origin.y, _quadtree._values, 2);
        }

        #endregion
    }
}