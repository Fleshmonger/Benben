namespace Gameplay.Map
{
    public abstract class Region
    {
        public abstract Tile GetTile(int x, int y);

        public abstract int GetDepth(int x, int y);

        public abstract Team GetTeam(int x, int y);

        public abstract void SetTile(int x, int y, Tile tile);

        public abstract void SetDepth(int x, int y, int depth);

        public abstract void SetTeam(int x, int y, Team team);

        public Tile GetTile(Point2 point)
        {
            return GetTile(point.x, point.y);
        }

        public int GetDepth(Point2 point)
        {
            return GetDepth(point.x, point.y);
        }

        public Team GetTeam(Point2 point)
        {
            return GetTeam(point.x, point.y);
        }

        public void SetTile(Point2 point, Tile tile)
        {
            SetTile(point.x, point.y, tile);
        }

        public void SetDepth(Point2 point, int depth)
        {
            SetDepth(point.x, point.y, depth);
        }

        public void SetTeam(Point2 point, Team team)
        {
            SetTeam(point.x, point.y, team);
        }
    }
}