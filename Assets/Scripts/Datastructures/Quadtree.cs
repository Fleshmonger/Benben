using System;
using Geometry;

namespace Datastructures
{
    /// <summary> Quadtree datastructure implemented using a two-dimensional k-d tree. </summary>
    public sealed class Quadtree<T> : Region<T>
    {
        #region Fields

        private Region<T> _values;

        #endregion

        #region Properties

        /// <summary> The size of the quadtree. </summary>
        public Size Dimensions { get; private set; }

        #endregion

        #region Constructors

        /// <summary> Creates a new quadtree with the given width, height dimensions. </summary>
        public Quadtree(int width, int height) : this(new Size(width, height)) { }

        /// <summary> Creates a new quadtree with the given dimensions. </summary>
        public Quadtree(Size dimensions)
        {
            Dimensions = dimensions;
            var area = dimensions.Area;
            if (area > 1)
            {
                _values = new Split<T>(dimensions);
            }
            else if (area == 1)
            {
                _values = new Cell<T>();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns whether the quadtree contains the given x, y coordinates.
        /// See <see cref="Region{T}.Contains(int, int)"/>.
        /// </summary>
        public override bool Contains(int x, int y)
        {
            return Dimensions.Contains(x, y);
        }

        /// <summary>
        /// Returns the value stored at the given x, y coordinates. See <see cref="Region{T}.GetValue(int, int)/>.
        /// </summary>
        public override T GetValue(int x, int y)
        {
            if (Contains(x, y))
            {
                return _values.GetValue(x, y);
            }
            throw new IndexOutOfRangeException(ErrorMsgOutsideBounds);
        }

        /// <summary>
        /// Stores the given value at the given x, y coordinates. See <see cref="Region{T}.SetValue(int, int, T)"/>.
        /// </summary>
        public override void SetValue(int x, int y, T value)
        {
            if (Contains(x, y))
            {
                _values.SetValue(x, y, value);
            }
            else
            {
                throw new IndexOutOfRangeException(ErrorMsgOutsideBounds);
            }
        }

        #endregion
    }
}