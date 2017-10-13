using UnityEngine;

namespace Geometry
{
    /// <summary> Representation of 2D integer rectangle. </summary>
    public class Size
    {
        #region Constants

        /// <summary> Shorthand for writing Size(0, 0). </summary>
        public static readonly Size zero = new Size(0, 0);

        /// <summary> Shorthand for writing Size(1, 1). </summary>
        public static readonly Size one = new Size(1, 1);

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

        public Size() : this(0, 0) { }

        /// <summary> Creates a new size with given width, height components. </summary>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
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

        #endregion

        #region Operators

        public static Size operator -(Size a, Size b)
        {
            return new Size(a.Width - b.Width, a.Height - b.Height);
        }

        public static Size operator -(Size size, Vector2I vector)
        {
            return new Size(size.Width - vector.x, size.Height - vector.y);
        }

        public static Size operator *(Size a, Size b)
        {
            return new Size(a.Width * b.Width, a.Height * b.Height);
        }

        public static Size operator *(Size size, Vector2I vector)
        {
            return new Size(size.Width * vector.x, size.Height * vector.y);
        }

        public static Size operator /(Size size, int n)
        {
            return new Size(size.Width / n, size.Height / n);
        }

        #endregion

        #region Conversions

        public static implicit operator Vector2I(Size size)
        {
            return new Vector2I(size.Width, size.Height);
        }

        public static implicit operator Vector3I(Size size)
        {
            return (Vector2I)size;
        }

        public static implicit operator Vector2(Size size)
        {
            return new Vector2(size.Width, size.Height);
        }

        public static implicit operator Vector3(Size size)
        {
            return (Vector2)size;
        }

        #endregion
    }
}