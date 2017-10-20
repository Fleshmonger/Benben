using Geometry;
using System;
using System.Collections.Generic;

namespace Datastructures
{
    /// <summary> Quadtree element that stores two subtrees. </summary>
    public sealed class Split<T> : Region<T>
    {
        #region Definitions

        /// <summary> Defines the orientation of a quadtree partition. </summary>
        public enum Partition { Horizontal, Vertical }

        #endregion

        #region Constants
        
        private static readonly Dictionary<Partition, Vector2I> _partitionVector = new Dictionary<Partition, Vector2I>
        {
            { Partition.Horizontal, Vector2I.right },
            { Partition.Vertical, Vector2I.up }
        };

        #endregion

        #region Properties

        /// <summary> The size of the subtree space. </summary>
        public Size2I Dimensions { get; private set; }

        /// <summary> The orientation of the partition. </summary>
        public Partition Orientation { get; private set; }

        /// <summary> The length of the first subtree in the partitioned dimension. </summary>
        public int SplitLength { get; private set; }

        /// <summary> Dimensions of the first subtree. </summary>
        public Size2I FirstDimensions
        {
            get
            {
                return Dimensions - (Dimensions - Vector2I.one * SplitLength) * _partitionVector[Orientation];
            }
        }

        /// <summary> Dimensions of the second subtree. </summary>
        public Size2I SecondDimensions
        {
            get
            {
                return Dimensions - SplitLength * _partitionVector[Orientation];
            }
        }

        /// <summary> The first subtree. </summary>
        public Region<T> FirstRegion { get; private set; }

        /// <summary> The second subtree. </summary>
        public Region<T> SecondRegion { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new split with given dimensions and a split at the center of the longest dimension.
        /// </summary>
        public Split(Size2I dimensions)
        {
            var orientation = dimensions.Width >= dimensions.Height ? Partition.Horizontal : Partition.Vertical;
            var splitLength = (orientation == Partition.Horizontal ? dimensions.Width : dimensions.Height) / 2;
            Dimensions = dimensions;
            Orientation = orientation;
            SplitLength = splitLength;
        }
        
        /// <summary> Creates a new split with given dimensions and subtrees. </summary>
        public Split(Size2I dimension, Region<T> firstRegion, Region<T> secondRegion) : this(dimension)
        {
            FirstRegion = firstRegion;
            SecondRegion = secondRegion;
        }

        /// <summary> Creates a new split with given dimensions, orientation, and split length. </summary>
        public Split(Size2I dimensions, Partition orientation, int splitLength)
        {
            Dimensions = dimensions;
            Orientation = orientation;
            SplitLength = splitLength;
        }

        /// <summary> Creates a new split with given dimensions, orientation, split length and subtrees. </summary>
        public Split(
            Size2I dimension,
            Partition orientation,
            int splitLength,
            Region<T> firstRegion,
            Region<T> secondRegion)
            : this(dimension, orientation, splitLength)
        {
            FirstRegion = firstRegion;
            SecondRegion = secondRegion;
        }

        #endregion

        #region Methods

        /// <summary> See <see cref="Region{T}.Contains(int, int)"./> </summary>
        public override bool Contains(int x, int y)
        {
            return Dimensions.Contains(x, y);
        }

        /// <summary>
        /// Returns the value stored in the subtree at the given x, y coordinates.
        /// See <see cref="Region{T}.GetValue(int, int)"/>.
        /// </summary>
        public override T GetValue(int x, int y)
        {
            if (FirstDimensions.Contains(x, y))
            {
                if (FirstRegion == null)
                {
                    return default(T);
                }
                return FirstRegion.GetValue(x, y);
            }

            var point = new Vector2I(x, y) - _partitionVector[Orientation] * SplitLength;
            if (SecondDimensions.Contains(point))
            {
                if (SecondRegion == null)
                {
                    return default(T);
                }
                return SecondRegion.GetValue(point);
            }
            throw new IndexOutOfRangeException(ErrorMsgOutsideBounds);
        }

        /// <summary>
        /// Stores the value in the subtree at the given x, y coordinates.
        /// See <see cref="Region{T}.SetValue(int, int, T)"/>.
        /// </summary>
        public override void SetValue(int x, int y, T value)
        {
            if (FirstDimensions.Contains(x, y))
            {
                if (FirstRegion == null)
                {
                    FirstRegion = CreateSubRegion(FirstDimensions);
                }
                FirstRegion.SetValue(x, y, value);
            }
            else
            {
                var point = new Vector2I(x, y) - _partitionVector[Orientation] * SplitLength;
                if (SecondDimensions.Contains(point))
                {
                    if (SecondRegion == null)
                    {
                        SecondRegion = CreateSubRegion(SecondDimensions);
                    }
                    SecondRegion.SetValue(point, value);
                }
                else
                {
                    throw new IndexOutOfRangeException(ErrorMsgOutsideBounds);
                }
            }
        }

        /// <summary> Constructs a new subtree with given width, height dimensions. </summary>
        private static Region<T> CreateSubRegion(Size2I dimensions)
        {
            var area = dimensions.Area;
            if (area > 1)
            {
                return new Split<T>(dimensions);
            }
            else if (area == 1)
            {
                return new Cell<T>();
            }
            return null;
        }

        #endregion
    }
}