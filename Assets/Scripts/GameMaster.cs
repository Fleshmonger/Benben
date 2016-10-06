using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    private int teamIndex;
    private bool gameOver = false;

    public Team[] teams;
    public Map map;
    public StageMaster stageMaster;
    public RulesMaster rulesMaster;
    public int goalHeight = 3;

    private void SetBlock(int x, int y, int height, Team team)
    {
        GameObject prop = stageMaster.AddBlock(new Vector3(x, height, y), team);
        Tile tile = new Tile(height, team, prop);
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                map.SetTile(x + i, y + j, tile);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void PlayerMove(int x, int y)
    {
        if (!gameOver)
        {
            Tile[,] tiles = GetQuadTiles(x, y);
            Team team = teams[teamIndex];
            if (rulesMaster.IsValid(tiles, team))
            {
                int height = tiles[0, 0].height + 1;
                SetBlock(x, y, height, team);
                if (height >= goalHeight)
                {
                    Debug.Log(team.owner + " won the game!");
                    gameOver = true;
                }
                else
                {
                    teamIndex = (teamIndex + 1) % teams.Length;
                }
            }
        }
    }

    // Returns a 2x2 array of tiles within x, y to x + 1, y + 1.
    public Tile[,] GetQuadTiles(int x, int y)
    {
        Tile[,] tiles = new Tile[2, 2];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Tile tile = map.GetTile(x + i, y + j);
                if (tile == null)
                {
                    tile = new Tile();
                }
                tiles[i, j] = tile;
            }
        }
        return tiles;
    }
}
