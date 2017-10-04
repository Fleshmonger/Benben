using UnityEngine.SceneManagement;
using Gameplay.Map;
using Geometry;

namespace Gameplay
{
    public class GameMaster : Singleton<GameMaster>
    {
        public int goalDepth = 3;
        public int blockSize = 2;

        public bool isGameOver { get; private set; }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PlayerMove(int x, int y)
        {
            var teamManager = TeamManager.Instance;
            var mapManager = Map.Map.Instance;
            if (!isGameOver && MapUtil.IsValid(x, y, blockSize, teamManager.activeTeam, Map.Map.Instance))
            {
                var depth = mapManager.GetDepth(x, y) + 1;
                SetBlock(x, y, depth, teamManager.activeTeam);
                if (depth >= goalDepth)
                {
                    isGameOver = true;
                }
                else
                {
                    teamManager.CycleActiveTeam();
                }
            }
        }

        public void PlayerMove(Vector2I pos)
        {
            PlayerMove(pos.x, pos.y);
        }

        private void SetBlock(int x, int y, int depth, Team team)
        {
            var prop = StageManager.Instance.AddBlock(x, y, depth, team);
            var tile = new Tile(depth, team, prop);
            var map = Map.Map.Instance;
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    map.SetTile(tile, x + i, y + j);
                }
            }
        }
    }
}