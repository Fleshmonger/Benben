using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public CameraManager cameraManager;
    public MapManager mapManager;
    public StageManager stageManager;
    public RulesManager rulesManager;
    public TeamManager teamManager;
    public GameController gameController;

    private bool lastMove, lastMoveValid;
    private int lastMoveX = -1, lastMoveZ = -1;

    public float viewSpeed = 5;

    private void Update()
    {
        ActionInput();
        MoveInput();
    }

    private void ActionInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!gameController.gameOver)
            {
                PlaceBlock();
            }
            else
            {
                gameController.RestartScene();
            }
        }
    }

    private void MoveInput()
    {
        Vector2 move = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector2.left + Vector2.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector2.right + Vector2.down;
        }
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector2.right + Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector2.left + Vector2.down;
        }
        move *= viewSpeed * Time.deltaTime;
        cameraManager.Move(move.x, move.y);
    }

    private void PlaceBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 gridPos = stageManager.WorldToGrid(hit.point.x, hit.point.y, hit.point.z);
            int x = Mathf.RoundToInt(gridPos.x), z = Mathf.RoundToInt(gridPos.z);
            if (lastMove && lastMoveValid && lastMoveX == x && lastMoveZ == z)
            {
                gameController.PlayerMove(x, z);
                stageManager.ClearFakeBlock();
                lastMove = false;
            }
            else
            {
                Tile tile = mapManager.GetTile(x, z);
                int y;
                if (tile != null)
                {
                    y = tile.height + 1;
                }
                else
                {
                    y = Tile.NULL_HEIGHT + 1;
                }

                lastMoveValid = rulesManager.IsValid(x, z, gameController.blockSize, teamManager.activeTeam);
                stageManager.SetFakeBlock(x, y, z, lastMoveValid);
                lastMove = true;
                lastMoveX = x;
                lastMoveZ = z;
            }
        }
    }
}