using Geometry;

namespace Datastructures
{
    /// <summary> Abstract class used to implement a quadtree. </summary>
    public abstract class Region<T>
    {
        #region Methods

        protected readonly string ErrorMsgOutsideBounds = "Index coordinates is outside of quadtree bounds.";

        /// <summary> Returns whether the given x, y coordinates is within the tree. </summary>
        public abstract bool Contains(int x, int y);

        /// <summary> Returns the value stored at the given x, y coordinates. </summary>
        public abstract T GetValue(int x, int y);

        /// <summary> Stores the value at the given x, y coordinates. </summary>
        public abstract void SetValue(int x, int y, T value);

        /// <summary> Returns whether the given point is within the tree. </summary>
        public bool Contains(Vector2I point)
        {
            return Contains(point.x, point.y);
        }

        /// <summary> Returns the value stored at the given point. </summary>
        public T GetValue(Vector2I point)
        {
            return GetValue(point.x, point.y);
        }

        /// <summary> Stores the value at the given point. </summary>
        public void SetValue(Vector2I point, T value)
        {
            SetValue(point.x, point.y, value);
        }

        #endregion
    }
}