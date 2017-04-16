using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameLogic
{
    //TODO Rename.
    public class GameMaster : Singleton<GameMaster>
    {
        public int goalHeight = 3;
        public int blockSize = 2;

        public bool isGameOver { get; private set; }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PlayerMove(int x, int y)
        {
            var teamManager = TeamManager.Instance;
            if (!isGameOver && MapUtil.IsValid(x, y, blockSize, teamManager.activeTeam, MapManager.Instance))
            {
                var tiles = MapManager.Instance.GetTiles(x, y, blockSize, blockSize);
                int height = tiles[0, 0].height + 1;
                SetBlock(x, y, height, teamManager.activeTeam);
                if (height >= goalHeight)
                {
                    isGameOver = true;
                }
                else
                {
                    teamManager.CycleActiveTeam();
                }
            }
        }

        private void SetBlock(int x, int y, int height, Team team)
        {
            var prop = StageManager.Instance.AddBlock(x, height, y, team);
            var tile = new Tile(height, team, prop);
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