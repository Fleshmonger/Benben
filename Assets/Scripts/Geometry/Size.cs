using System;
using UnityEngine;

namespace Geometry
{
    /// <summary> Representation of 2D integer rectangle. </summary>
    public struct Size2I
    {
        #region Constants

        /// <summary> Shorthand for writing Size(0, 0). </summary>
        public static readonly Size2I zero = new Size2I(0, 0);

        /// <summary> Shorthand for writing Size(1, 1). </summary>
        public static readonly Size2I one = new Size2I(1, 1);

        #endregion

        #region Fields

        private int _width;
        private int _height;

        #endregion

        #region Properties

        /// <summary> The width component of the rectangle. </summary>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = Mathf.Max(value, 0);
            }
        }

        /// <summary> The height component of the rectangle. </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = Mathf.Max(value, 0);
            }
        }

        /// <summary> The surface area of the rectangle. </summary>
        public int Area
        {
            get
            {
                return Width * Height;
            }
        }

        #endregion

        #region Constructors
        
        /// <summary> Creates a new size with given width, height components. </summary>
        public Size2I(int width, int height)
        {
            _width = Math.Max(width, 0);
            _height = Math.Max(height, 0);
        }

        #endregion

        #region Methods

        /// <summary> Returns whether the given x, y coordinates are within the dimensions (exclusive) </summary>
        public bool Contains(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        /// <summary> Returns whether the given point is within the dimensions (exclusive) </summary>
        public bool Contains(Vector2I point)
        {
            return Contains(point.x, point.y);
        }

        /// <summary> Returns the offset of given x, y coordinates from the size's container. </summary>
        public Vector2I Offset(int x, int y)
        {
            return new Vector2I(Compare(x, Width), Compare(y, Height));
        }

        /// <summary> Returns the numerical distance of the value from the interval [0, length]. </summary>
        private static int Compare(int value, int length)
        {
            return value - Mathf.Clamp(value, 0, length - 1);
        }

        #endregion

        #region Operators

        public static Size2I operator -(Size2I a, Size2I b)
        {
            return new Size2I(a.Width - b.Width, a.Height - b.Height);
        }

        public static Size2I operator -(Size2I size, Vector2I vector)
        {
            return new Size2I(size.Width - vector.x, size.Height - vector.y);
        }

        public static Size2I operator *(int i, Size2I size)
        {
            return new Size2I(i * size.Width, i * size.Height);
        }

        public static Size2I operator *(Size2I a, Size2I b)
        {
            return new Size2I(a.Width * b.Width, a.Height * b.Height);
        }

        public static Size2I operator *(Size2I size, Vector2I vector)
        {
            return new Size2I(size.Width * vector.x, size.Height * vector.y);
        }

        public static Size2I operator /(Size2I size, int n)
        {
            return new Size2I(size.Width / n, size.Height / n);
        }

        #endregion

        #region Conversions

        public static implicit operator Vector2I(Size2I size)
        {
            return new Vector2I(size.Width, size.Height);
        }

        public static implicit operator Vector3I(Size2I size)
        {
            return (Vector2I)size;
        }

        public static implicit operator Vector2(Size2I size)
        {
            return new Vector2(size.Width, size.Height);
        }

        public static implicit operator Vector3(Size2I size)
        {
            return (Vector2)size;
        }

        #endregion
    }
}