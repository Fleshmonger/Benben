using UnityEngine;

public class Tile : Region
{
    public const int NULL_HEIGHT = 0;

    public int height = 0;
    public Team team;
    public GameObject prop;

    private void Initialize(int height, Team team, GameObject prop)
    {
        this.height = height;
        this.team = team;
        this.prop = prop;
    }

    public Tile(int height, Team team, GameObject prop)
    {
        Initialize(height, team, prop);
    }

    public Tile() : this(0, null, null) { }

    public override Tile GetTile(int x, int y)
    {
        return this;
    }

    public override int GetHeight(int x, int y)
    {
        return height;
    }

    public override Team GetTeam(int x, int y)
    {
        return team;
    }

    public override void SetTile(int x, int y, Tile tile)
    {
        Initialize(tile.height, tile.team, tile.prop);
    }

    public override void SetHeight(int x, int y, int height)
    {
        this.height = Mathf.Max(0, height);
    }

    public override void SetTeam(int x, int y, Team team)
    {
        this.team = team;
    }
}