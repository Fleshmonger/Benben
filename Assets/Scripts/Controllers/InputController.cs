using UnityEngine;
using GameLogic;

namespace Interface
{
    public sealed class InputController : Singleton<InputController>
    {
        public float viewSpeed = 5;
        public CameraManager cameraManager;

        private bool lastMove;
        private bool lastMoveValid;
        private int lastMoveX = -1, lastMoveZ = -1;

        private void ActionInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var gameMaster = GameMaster.Instance;
                if (!gameMaster.isGameOver)
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
            var move = Vector2.zero;
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
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var stageManager = StageManager.Instance;
                var gameMaster = GameMaster.Instance;
                var gridPos = stageManager.WorldToGrid(hit.point.x, hit.point.y, hit.point.z);
                var x = Mathf.RoundToInt(gridPos.x);
                var z = Mathf.RoundToInt(gridPos.z);
                if (lastMove && lastMoveValid && lastMoveX == x && lastMoveZ == z)
                {
                    gameMaster.PlayerMove(x, z);
                    stageManager.ClearFakeBlock();
                    lastMove = false;
                }
                else
                {
                    var tile = MapManager.Instance.GetTile(x, z);
                    var y = 1;
                    if (tile != null)
                    {
                        y += tile.height;
                    }

                    lastMoveValid = MapUtil.IsValid(x, z, gameMaster.blockSize, TeamManager.Instance.activeTeam, MapManager.Instance);
                    stageManager.SetFakeBlock(x, y, z, lastMoveValid);
                    lastMove = true;
                    lastMoveX = x;
                    lastMoveZ = z;
                }
            }
        }

        // Unity update callback.
        private void Update()
        {
            ActionInput();
            MoveInput();
        }
    }
}