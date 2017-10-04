namespace Geometry
{
    /// <summary> Data struct for a integer sized object. </summary>
    public struct Size
    {
        public int width;
        public int height;

        /// <summary> Constructor </summary>
        public Size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        /// <summary> Constructor </summary>
        public Size(object[,] array)
        {
            this.width = array.GetLength(0);
            this.height = array.GetLength(1);
        }
    }
}