public abstract class Region
{
    public abstract Tile GetTile(int x, int y);

    public abstract int GetHeight(int x, int y);

    public abstract Team GetTeam(int x, int y);

    public abstract void SetTile(int x, int y, Tile tile);

    public abstract void SetHeight(int x, int y, int height);

    public abstract void SetTeam(int x, int y, Team team);
}