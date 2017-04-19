using UnityEngine;
using Gameplay;
using Gameplay.Map;

namespace Interface
{
    public sealed class InputController : Singleton<InputController>
    {
        public float viewSpeed = 5;
        public CameraManager cameraManager;

        private bool marked;
        private Point2 markPos;

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
                move += Vector2.left + Vector2.down;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move += Vector2.right + Vector2.up;
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
                var mapManager = MapManager.Instance;
                var gridPos = stageManager.WorldToGrid(hit.point);
                var valid = MapUtil.IsValid(gridPos, gameMaster.blockSize, TeamManager.Instance.activeTeam, mapManager);
                if (marked && valid && markPos == gridPos)
                {
                    gameMaster.PlayerMove(gridPos);
                    stageManager.ClearMark();
                    marked = false;
                }
                else
                {
                    stageManager.SetMarkPos(gridPos.x, gridPos.y, mapManager.GetDepth(gridPos), valid);
                    marked = true;
                    markPos = gridPos;
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