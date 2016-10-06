using UnityEngine;
using System.Collections;

public class InputMaster : MonoBehaviour
{
    private bool lastMove = false;
    private int lastMoveX = -1, lastMoveZ = -1;

    public float viewSpeed = 5;
    public Map map;
    public GameMaster gameMaster;
    public StageMaster stageMaster;
    public CameraMaster cameraMaster;

    private void Update()
    {
        ActionInput();
        MoveInput();
    }

    private void ActionInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!gameMaster.IsGameOver())
            {
                PlaceBlock();
            }
            else
            {
                gameMaster.Restart();
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
        cameraMaster.Move(move.x, move.y);
    }

    private void PlaceBlock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 gridPos = stageMaster.WorldToGrid(hit.point.x, hit.point.y, hit.point.z);
            int x = Mathf.RoundToInt(gridPos.x), z = Mathf.RoundToInt(gridPos.z);
            if (lastMove && lastMoveX == x && lastMoveZ == z)
            {
                gameMaster.PlayerMove(x, z);
                stageMaster.ClearFakeBlock();
                lastMove = false;
            }
            else
            {
                Tile tile = map.GetTile(x, z);
                int y;
                if (tile != null)
                {
                    y = tile.height + 1;
                }
                else
                {
                    y = Tile.NULL_HEIGHT + 1;
                }
                stageMaster.SetFakeBlock(x, y, z);
                lastMove = true;
                lastMoveX = x;
                lastMoveZ = z;
            }
        }
    }
}