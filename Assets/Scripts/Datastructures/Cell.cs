using System;

namespace Datastructures
{
    /// <summary> Quadtree element that stores a single value. </summary>
    public sealed class Cell<T> : Region<T>
    {
        #region Properties

        /// <summary> The value stored in the cell. </summary>
        public T Value { get; set; }

        #endregion
        
        #region Methods

        /// <summary>
        /// Returns whether the cell contains the x, y coordinates.
        /// See <see cref="Region{T}.Contains(int, int)"/>.
        /// </summary>
        public override bool Contains(int x, int y)
        {
            return x == 0 && y == 0;
        }

        /// <summary> Returns the value stored in the cell. See <see cref="Region{T}.GetValue(int, int)"/>. </summary>
        public override T GetValue(int x, int y)
        {
            if (Contains(x, y))
            {
                return Value;
            }
            throw new IndexOutOfRangeException(ErrorMsgOutsideBounds);
        }

        /// <summary> Stores the value in the cell. See <see cref="Region{T}.SetValue(int, int, T)"/>. </summary>
        public override void SetValue(int x, int y, T value)
        {
            if (Contains(x, y))
            {
                Value = value;
            }
            else
            {
                throw new IndexOutOfRangeException(ErrorMsgOutsideBounds);
            }
        }

        #endregion
    }
}