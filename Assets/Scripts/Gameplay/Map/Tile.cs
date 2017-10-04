using UnityEngine;

namespace Gameplay.Map
{
    public sealed class Tile : Region
    {
        public int depth;
        public Team team;
        public GameObject prop;

        private void Initialize(int depth, Team team, GameObject prop)
        {
            this.depth = depth;
            this.team = team;
            this.prop = prop;
        }

        public Tile(int depth, Team team, GameObject prop)
        {
            Initialize(depth, team, prop);
        }

        public Tile() : this(0, null, null) { }

        public override Tile GetTile(int x, int y)
        {
            return this;
        }

        public override int GetDepth(int x, int y)
        {
            return depth;
        }

        public override Team GetTeam(int x, int y)
        {
            return team;
        }

        public override void SetTile(int x, int y, Tile tile)
        {
            Initialize(tile.depth, tile.team, tile.prop);
        }

        public override void SetDepth(int x, int y, int depth)
        {
            this.depth = Mathf.Max(0, depth);
        }

        public override void SetTeam(int x, int y, Team team)
        {
            this.team = team;
        }
    }
}