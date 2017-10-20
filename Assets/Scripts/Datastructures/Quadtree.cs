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
        public Size2I Size { get; private set; }

        #endregion

        #region Constructors

        /// <summary> Creates a new quadtree with given width, height dimensions. </summary>
        /// <param name="width"> The width of the quadtree. </param>
        /// <param name="height"> The height of the quadtree. </param>
        public Quadtree(int width, int height) : this(new Size2I(width, height)) { }

        /// <summary> Creates a new quadtree with a given size. </summary>
        /// <param name="dimensions"> The dimensions of the quadtree. </param>
        public Quadtree(Size2I size)
        {
            Size = size;
            var area = size.Area;
            if (area > 1)
            {
                _values = new Split<T>(size);
            }
            else if (area == 1)
            {
                _values = new Cell<T>();
            }
        }

        #endregion

        #region Methods

        /// <summary> Checks if the x, y coordinates is within the quadtree boundaries. </summary>
        /// <param name="x"> The x coordinate to check. </param>
        /// <param name="y"> The y coordinate to check. </param>
        /// <returns>
        /// True if the coordinates are within the quadtree dimensions (exclusive); false otherwise.
        /// </returns>
        public override bool Contains(int x, int y)
        {
            return Size.Contains(x, y);
        }

        /// <summary> Returns the value stored at the x, y coordinates. </summary>
        /// <param name="x"> The x coordinate of the position. </param>
        /// <param name="y"> The y coordinate of the position. </param>
        /// <returns>
        /// The value stored at the coordinates if within quadtree boundaries;
        /// otherwise throws an IndexOutOfRangeException.
        /// </returns>
        public override T GetValue(int x, int y)
        {
            if (Contains(x, y))
            {
                return _values.GetValue(x, y);
            }
            throw new IndexOutOfRangeException(ErrorMsgOutsideBounds);
        }

        /// <summary> Stores the value at the x, y coordinates. </summary>
        /// <param name="x"> The x coordinate of the position. </param>
        /// <param name="y"> The y coordinate of the position. </param>
        /// <param name="value"> The value to store in the tree. </param>
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