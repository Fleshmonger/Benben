using System;
using UnityEngine;
using Geometry;

namespace Datastructures
{
    /// <summary> Dynamically sized quadtree implemented using a k-d tree. </summary>
    public sealed class QuadtreeExpanding<T> : Region<T>
    {
        #region Fields

        //TODO Make private.
        public Region<T> _values;

        #endregion

        #region Properties

        /// <summary> The size of the quadtree. </summary>
        public Size2I Dimensions { get; private set; }

        /// <summary> The local top-left corner of the quadtree. </summary>
        public Vector2I Origin { get; private set; }

        #endregion

        #region Constructors

        public QuadtreeExpanding() : this(new Size2I()) { }

        /// <summary> Creates a new expanding quadtree with the given initial dimensions. </summary>
        public QuadtreeExpanding(Size2I dimensions)
        {
            Dimensions = dimensions;
            var area = dimensions.Area;
            if (area > 1)
            {
                _values = new Split<T>(dimensions);
            }
            else
            {
                Dimensions = Size2I.one;
                _values = new Cell<T>();
            }
        }

        /// <summary> Creates a new expanding quadtree with the given initial dimensions and origin. </summary>
        public QuadtreeExpanding(Size2I dimensions, Vector2I origin) : this(dimensions)
        {
            Origin = origin;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns whether the quadtree currently contains the given x, y coordinates.
        /// See <see cref="Region{T}.Contains(int, int)"/>.
        /// </summary>
        public override bool Contains(int x, int y)
        {
            return Dimensions.Contains(x - Origin.x, y - Origin.y);
        }

        /// <summary>
        /// Returns the value at the given x, y coordinates or default if the quadtree does not contain it.
        /// See <see cref="Region.GetValue(int, int)"/>.
        /// </summary>
        public override T GetValue(int x, int y)
        {
            if (Contains(x, y))
            {
                return _values.GetValue(x - Origin.x, y - Origin.y);
            }
            return default(T);
        }

        /// <summary>
        /// Stores the given value at the given x, y coordinates, expanding the quadtree if necessary.
        /// See <see cref="Region.SetValue(int, int, T)"/>.
        /// </summary>
        public override void SetValue(int x, int y, T value)
        {
            ExpandTree(x, y);
            _values.SetValue(x - Origin.x, y - Origin.y, value);
        }

        /// <summary> Expands the tree until it contains the given x, y coordinates. </summary>
        private void ExpandTree(int x, int y)
        {
            while (!Contains(x, y))
            {
                var offsetX = 0;
                var offsetY = 0;
                var width = Dimensions.Width;
                var height = Dimensions.Height;
                var distance = Dimensions.Offset(x - Origin.x, y - Origin.y);
                int splitLength;
                Split<T>.Partition orientation;
                Region<T> first = null;
                Region<T> second = null;
                if (Math.Abs(distance.x) >= Math.Abs(distance.y))
                {
                    if (distance.x < 0)
                    {
                        offsetX = -width;
                    }
                    splitLength = width;
                    width *= 2;
                    orientation = Split<T>.Partition.Horizontal;
                }
                else
                {
                    if (distance.y < 0)
                    {
                        offsetY = -height;
                    }
                    splitLength = height;
                    height *= 2;
                    orientation = Split<T>.Partition.Vertical;
                }
                if (offsetX == 0 && offsetY == 0)
                {
                    first = _values;
                }
                else
                {
                    second = _values;
                }
                Dimensions = new Size2I(width, height);
                _values = new Split<T>(Dimensions, orientation, splitLength, first, second);
                Origin += new Vector2I(offsetX, offsetY);
            }
        }

        #endregion
    }
}