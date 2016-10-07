using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public RulesManager rulesManager;
    public StageManager stageManager;
    public TeamsManager teamsManager;
    public MapManager mapManager;

    public int goalHeight = 3;

    private bool _gameOver = false;
    public bool gameOver
    {
        get
        {
            return _gameOver;
        }
    }

    private void SetBlock(int x, int y, int height, Team team)
    {
        GameObject prop = stageManager.AddBlock(new Vector3(x, height, y), team);
        Tile tile = new Tile(height, team, prop);
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                mapManager.SetTile(x + i, y + j, tile);
            }
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerMove(int x, int y)
    {
        if (!gameOver && rulesManager.ValidPlayerMove(x, y, teamsManager.activeTeam))
        {
            Tile[,] tiles = mapManager.GetQuadTiles(x, y);
            int height = tiles[0, 0].height + 1;
            SetBlock(x, y, height, teamsManager.activeTeam);
            if (height >= goalHeight)
            {
                _gameOver = true;
            }
            else
            {
                teamsManager.CycleActiveTeam();
            }
        }
    }
}
