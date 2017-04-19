using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Gameplay.Map;

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
            var mapManager = MapManager.Instance;
            if (!isGameOver && MapUtil.IsValid(x, y, blockSize, teamManager.activeTeam, MapManager.Instance))
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

        public void PlayerMove(Point2 pos)
        {
            PlayerMove(pos.x, pos.y);
        }

        private void SetBlock(int x, int y, int depth, Team team)
        {
            var prop = StageManager.Instance.AddBlock(x, y, depth, team);
            var tile = new Tile(depth, team, prop);
            var map = MapManager.Instance;
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    map.SetTile(x + i, y + j, tile);
                }
            }
        }
    }
}