namespace Geometry
{
    /// <summary> An integer, 2D rectangle defined by X and Y position, width and height. </summary>
    public struct Area2I
    {
        #region Fields

        private Vector2I _position;

        private Size2I _size;

        #endregion

        #region Properties

        /// <summary> The X, Y coordinates of the area. </summary>
        public Vector2I Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary> The size of the area. </summary>
        public Size2I Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary> The X coordinate of the area. </summary>
        public int X
        {
            get { return _position.x; }
            set { _position.x = value; }
        }

        /// <summary> The Y coordinate of the area. </summary>
        public int Y
        {
            get { return _position.x; }
            set { _position.y = value; }
        }

        /// <summary> The width of the area. </summary>
        public int Width
        {
            get { return _size.Width; }
            set { _size.Width = value; }
        }

        /// <summary> The height of the area. </summary>
        public int Height
        {
            get { return _size.Height; }
            set { _size.Height = value; }
        }

        #endregion

        #region Constructors

        /// <summary> Creates a new area cloned of the given area. </summary>
        /// <param name="source"> The source area to clone from. </param>
        public Area2I(Area2I source)
        {
            _position = source._position;
            _size = source._size;
        }

        /// <summary> Creates a new area with given X and Y coordinates, width and height. </summary>
        /// <param name="x"> The X coordinate of the area. </param>
        /// <param name="y"> The Y coordinate of the area. </param>
        /// <param name="width"> The width of the area. </param>
        /// <param name="height"> The height of the area. </param>
        public Area2I(int x, int y, int width, int height)
        {
            _position = new Vector2I(x, y);
            _size = new Size2I(width, height);
        }

        /// <summary> Creates a new area with given position and size. </summary>
        /// <param name="position"> The position of the minimum corner of the area. </param>
        /// <param name="dimensions"> The width and height of the area. </param>
        public Area2I(Vector2I position, Size2I size)
        {
            _position = position;
            _size = size;
        }

        #endregion

        #region Methods

        /// <summary> Checks if the given x, y coordinates is within the boundaries. </summary>
        /// <param name="x"> The x coordinate to check. </param>
        /// <param name="y"> The y coordinate to check. </param>
        /// <returns> True if the coordinates is within the area, false otherwise. </returns>
        public bool Contains(int x, int y)
        {
            return X <= x && x < X + Width && Y <= y && y < Height;
        }

        /// <summary> Checks is the given point is within the boundaries. </summary>
        /// <param name="point"> The point to check. </param>
        /// <returns> True if the point is within the area, false otherwise. </returns>
        public bool Contains(Vector2I point)
        {
            return Contains(point.x, point.y);
        }

        #endregion
    }
}