using Geometry;

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

        public Tile GetTile(Vector2I point)
        {
            return GetTile(point.x, point.y);
        }

        public int GetDepth(Vector2I point)
        {
            return GetDepth(point.x, point.y);
        }

        public Team GetTeam(Vector2I point)
        {
            return GetTeam(point.x, point.y);
        }

        public void SetTile(Vector2I point, Tile tile)
        {
            SetTile(point.x, point.y, tile);
        }

        public void SetDepth(Vector2I point, int depth)
        {
            SetDepth(point.x, point.y, depth);
        }

        public void SetTeam(Vector2I point, Team team)
        {
            SetTeam(point.x, point.y, team);
        }
    }
}