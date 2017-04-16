public struct Size
{
    public int width;
    public int height;

    public Size(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public Size(object[,] array)
    {
        this.width = array.GetLength(0);
        this.height = array.GetLength(1);
    }
}