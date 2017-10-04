using UnityEngine;

namespace Geometry
{
    /// <summary> Representation of 2D integer vectors and points. </summary>
    public struct Vector2I
    {
        #region Constants

        /// <summary> Shorthand for writing Vector2I(0, 0). </summary>
        public static readonly Vector2I zero = new Vector2I(0, 0);

        /// <summary> Shorthand for writing Vector2I(1, 1). </summary>
        public static readonly Vector2I one = new Vector2I(1, 1);

        /// <summary> Shorthand for writing Vector2I(1, 0). </summary>
        public static readonly Vector2I right = new Vector2I(1, 0);

        /// <summary> Shorthand for writing Vector2I(0, 1). </summary>
        public static readonly Vector2I up = new Vector2I(0, 1);

        /// <summary> Shorthand for writing Vector2I(-1, 0). </summary>
        public static readonly Vector2I left = new Vector2I(-1, 0);

        /// <summary> Shorthand for writing Vector2I(0, -1). </summary>
        public static readonly Vector2I down = new Vector2I(0, -1);

        #endregion

        #region Fields

        /// <summary> X component of the vector. </summary>
        public int x;

        /// <summary> Y component of the vector. </summary>
        public int y;

        #endregion

        #region Constructors

        /// <summary> Creates a new vector with given x, y components. </summary>
        public Vector2I(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary> Creates a new vector copy of the given vector. </summary>
        public Vector2I(Vector2I v) : this(v.x, v.y) { }

        #endregion

        #region Methods

        public bool Within(int x, int y, int width, int height)
        {
            return x <= this.x && this.x < width && y <= this.y && this.y < height;
        }

        public Vector2I RotateLeft()
        {
            return new Vector2I(-y, x);
        }

        public Vector2I RotateRight()
        {
            return new Vector2I(y, -x);
        }

        /// <summary> Returns a float point representation of this vector. </summary>
        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
        }

        /// <summary> Returns true if the given vector is exactly equal to this vector. </summary>
        public override bool Equals(object obj)
        {
            if (obj is Vector2I)
            {
                return this == (Vector2I) obj;
            }
            return base.Equals(obj);
        }

        /// <summary> Returns a hashcode. </summary>
        public override int GetHashCode()
        {
            return x.GetHashCode() + 7 * y.GetHashCode();
        }

        /// <summary> Returns a nicely formatted string for this vertex. </summary>
        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        #endregion

        #region Operators

        public static bool operator ==(Vector2I a, Vector2I b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vector2I a, Vector2I b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static Vector2I operator -(Vector2I a)
        {
            return new Vector2I(-a.x, -a.y);
        }

        public static Vector2I operator +(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.x + b.x, a.y + b.y);
        }

        public static Vector2I operator -(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.x - b.x, a.y - b.y);
        }

        public static Vector2I operator *(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.x * b.x, a.y * b.y);
        }

        public static Vector2I operator *(int n, Vector2I p)
        {
            return new Vector2I(n * p.x, n * p.y);
        }

        public static Vector2I operator *(Vector2I p, int n)
        {
            return n * p;
        }

        #endregion
    }

    /// <summary> Representation of 3D integer vectors and points. </summary>
    public struct Vector3I
    {
        #region Constants

        /// <summary> Shorthand for writing Vector3I(0, 0, 0). </summary>
        public static readonly Vector3I zero = new Vector3I(0, 0, 0);

        /// <summary> Shorthand for writing Vector3I(1, 1, 1). </summary>
        public static readonly Vector3I one = new Vector3I(1, 1, 1);

        /// <summary> Shorthand for writing Vector3I(1, 0, 0). </summary>
        public static readonly Vector3I right = new Vector3I(1, 0, 0);

        /// <summary> Shorthand for writing Vector3I(0, 1, 0). </summary>
        public static readonly Vector3I up = new Vector3I(0, 1, 0);

        /// <summary> Shorthand for writing Vector3I(0, 0, 1). </summary>
        public static readonly Vector3I forward = new Vector3I(0, 0, 1);

        /// <summary> Shorthand for writing Vector3I(-1, 0, 0). </summary>
        public static readonly Vector3I left = new Vector3I(-1, 0, 0);

        /// <summary> Shorthand for writing Vector3I(0, -1, 0). </summary>
        public static readonly Vector3I down = new Vector3I(0, -1, 0);

        /// <summary> Shorthand for writing Vector3I(0, 0, -1). </summary>
        public static readonly Vector3I back = new Vector3I(0, 0, -1);

        #endregion

        #region Fields

        /// <summary> X component of the vector. </summary>
        public int x;

        /// <summary> Y component of the vector. </summary>
        public int y;

        /// <summary> Z component of the vector. </summary>
        public int z;

        #endregion

        #region Constructors
        
        /// <summary> Creates a new vector with given x, y, z components. </summary>
        public Vector3I(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary> Creates a new vector copy of the given vector. </summary>
        public Vector3I(Vector3I point3) : this(point3.x, point3.y, point3.z) { }

        #endregion

        #region Methods

        /// <summary> Returns a nicely formatted string for this vertex. </summary>
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }

        #endregion

        #region Operators
        
        public static implicit operator Vector2I(Vector3I point)
        {
            return new Vector2I(point.x, point.y);
        }

        #endregion
    }
}