using UnityEngine;
using System.Collections;

public class InputMaster : MonoBehaviour
{
    public GameMaster gameMaster;
    public StageMaster stageMaster;
    public CameraMaster cameraMaster;

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
            Vector3 scale = stageMaster.blockPrefab.transform.localScale / 2;
            int x = Mathf.RoundToInt(hit.point.x / scale.x), z = Mathf.RoundToInt(hit.point.z / scale.z);
            gameMaster.PlayerMove(x, z);
        }
    }
}